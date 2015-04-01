using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public string gameType;

	public GameObject floor;
	public float ammoSpawnTime = 10f;
	public GameObject hill;
	public float hillChangeTime = 10f;
	public int winScore;
	public GameObject[] CoinsCollcetion;
	public GameObject[] PowersCollection;
	public Image background;

	GameObject myHill;
	float floorSpawnTime = 0.5f;
	GameObject[] floors;
	public GameObject[] players;
	public GameObject spawnFloor;
	Vector3 spawnLoc;
	Vector3 coinSpawnLoc;
	GameObject coinSpawnObject;
	float coinSpawnTime;

	GameObject[] playersList;

	List<int> winners = new List<int> ();

	// Teams for team gametypes
	ArrayList team1 = new ArrayList();
	ArrayList team2 = new ArrayList();

	int[] team1Array;
	int[] team2Array;

	int pause_player = -1;

	public static int[] scores = {0, 0, 0, 0};
	public static int[] teamScores = {0, 0};

	Text scoreText;
	Text timerText;

	float curAmmoTime;
	float curHillTime;
	float curFloorTime;
	float curMoneyTime;
	float curMagnetTime;

	public static int numCoins;
	
	public GameObject Magnet;
	public float moneySpawnTime = 5f;
	public float MagnetSpawnTime = 5f;
	//float timeRemain = 12f;

	public static bool paused = false;

	// Use this for initialization
	void Start () {

		scoreText = GameObject.FindGameObjectWithTag ("Scores").GetComponent<Text>();

		timerText = GameObject.FindGameObjectWithTag ("Timer").GetComponent<Text>();

		curMoneyTime = moneySpawnTime;
		curAmmoTime = ammoSpawnTime;
		curHillTime = hillChangeTime;
		curFloorTime = floorSpawnTime;
		curMagnetTime = MagnetSpawnTime;

		// Set GameType to MatchManager GameType
		if (MatchManager.gameType != null)
			gameType = MatchManager.gameType;

		// Find and store all floors
		floors = GameObject.FindGameObjectsWithTag ("Floor");

		// Find and store all players
		players = GameObject.FindGameObjectsWithTag ("Player");

		// Setup Hill if King of the Hill
		if (gameType == "King of the Hill")
		{
			int floorNumber = Random.Range(0, floors.Length);
			floor = floors[floorNumber];
			spawnLoc = new Vector3 (floor.transform.position.x
			                        + Random.Range(-0.5f * floor.transform.lossyScale.x,0.5f * floor.transform.lossyScale.x),
			                        floor.transform.position.y + 1.75f, 0);
			myHill = Instantiate (hill, spawnLoc, Quaternion.Euler(0, 0, 0)) as GameObject;
		//	myHill.transform.parent = floor.transform;
		}

		// Player setup based on game mode
		foreach (GameObject player in players)
		{
			if (gameType == "Deathmatch" || gameType == "Team Deathmatch")
				player.GetComponent<PlayerController>().lives = 3;
		}

		// Team setup if team-based game
		if (MatchManager.teamDynamic != null && MatchManager.teamDynamic != "FFA")
		{
			// Team setup
			foreach (int playerNum in MatchManager.team1){
				foreach (GameObject player in players){
					if(player.GetComponent<PlayerController>().playerNumber == playerNum){
						player.GetComponent<PlayerController>().Update_color(1);
						team1.Add(playerNum);
					}
				}
			}
			foreach (int playerNum in MatchManager.team2){
				foreach (GameObject player in players){
					if(player.GetComponent<PlayerController>().playerNumber == playerNum){
						player.GetComponent<PlayerController>().Update_color(2);
						team2.Add(playerNum);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Screen.width != 1280) {
			Screen.SetResolution(1280, 720, true);
			// set to true if you want a fullscreen game
		}

		if (pause_player == -1) {
			for (int i=0; i<4; i++) {
				if (players [i].GetComponent<PlayerController> ().pausepressed == true) {
					Time.timeScale = 0;
					pause_player = i;
					paused = true;
					background.color = new Color(background.color[0], background.color[1], background.color[2], 0.5f);
					//Debug.Log (1);
					break;
				}
			}
		} else {
			if (players [pause_player].GetComponent<PlayerController> ().pausepressed == false) {
				Time.timeScale = 1;
				pause_player = -1;
				paused = false;
				background.color = new Color(background.color[0], background.color[1], background.color[2], 0);
			} else if (Input.GetKeyDown ("s") || Input.GetButtonDown ("Fire2")) {
				Time.timeScale = 1;
				pause_player = -1;
				paused = false;
				background.color = new Color(background.color[0], background.color[1], background.color[2], 0);
				Application.LoadLevel("Scene0");
				for (int i=0;i<4;i++) {
					scores[i] = 0;
				}
				numCoins = 0;
				//resumepressed = false;
			} else if (Input.GetKeyDown ("d") || Input.GetButtonDown ("Fire3")) {
				Time.timeScale = 1;
				pause_player = -1;
				paused = false;
				background.color = new Color(background.color[0], background.color[1], background.color[2], 0);
				for (int i=0;i<4;i++) {
					scores[i] = 0;
				}
				numCoins = 0;
				Application.LoadLevel("StartMenu");
			}
		}
		//timeRemain -= Time.deltaTime;
		//scoreText.text = "First to 100!\nP1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2] + "\n" + "P4: " + scores[3];
		//scoreText.text = "First to 30!\nP1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2] + "\n" + "P4: " + scores[3];

		//timerText.text = "Time Remaining:" + (int)timeRemain;
		int numPowers = GameObject.FindGameObjectsWithTag("Magnet").Length + 
						GameObject.FindGameObjectsWithTag("Shield").Length + 
						GameObject.FindGameObjectsWithTag("SplittedBarrel").Length;
		numCoins = GameObject.FindGameObjectsWithTag("Money").Length;
		scoreText.text = "First to 30!";
		curFloorTime -= Time.deltaTime;

		if (numPowers < 3){
			curMagnetTime -= Time.deltaTime;
		}

		if (numCoins < 10){
			curMoneyTime -= Time.deltaTime;
		}
		if (coinSpawnTime > 0) {
			coinSpawnTime -= Time.deltaTime;
		}else{
			if(coinSpawnObject){
				GameObject s = Instantiate (coinSpawnObject, coinSpawnLoc, Quaternion.Euler (0, 0, 0)) as GameObject;
				coinSpawnObject = null;
			}
		}

		for (int i=0; i<4; i++)
		{
			if (scores[i] >= winScore)
			//winners.Add(i+1);
				Application.LoadLevel ("ScoresScreen");
		}

		/*
		if (timeRemain <= 0)
		{
			Application.LoadLevel("ScoresScreen");
		}
		*/

		if (curMoneyTime <= 0)
		{
			if (numCoins < 10)
			{
				Spawn(CoinsCollcetion[Random.Range (0, 3)], -36, 8, -1, 24);
				curMoneyTime = moneySpawnTime;
			}
		}

		if (curMagnetTime <= 0) 
		{
			if (numPowers < 3){
				Spawn(PowersCollection[Random.Range (0, 3)], -27, 27, -6, 20);
				curMagnetTime = MagnetSpawnTime;
			}
		}

//
//		if (curFloorTime <= 0)
//		{
//			Spawn (spawnFloor);
//			curFloorTime = floorSpawnTime;
//		}

		/*
		// Move the Hill if GameType is King of the Hill
		if (gameType == "King of the Hill")
		{
			// Hill change timing
			curHillTime -= Time.deltaTime;
		
			if (curHillTime <= 0)
			{
				MoveHill ();
				curHillTime = hillChangeTime;
			}


			if (MatchManager.teamDynamic == "FFA")
			{
				for (int i = 0; i < 4; i++)
					if (scores[i] >= winScore)
					{
						winners.Add(i+1);
						RoundOver(winners, 1, 1);
					}
			}
			else
			{
				team1Array = team1.ToArray(typeof(int)) as int[];
				team2Array = team2.ToArray(typeof(int)) as int[];
				teamScores[0] = scores[team1Array[0]-1] + scores[team1Array[1]-1];
				teamScores[1] = scores[team2Array[0]-1] + scores[team2Array[1]-1];
				if (teamScores[0] >= winScore)
				{
					winners.AddRange(team1Array);
					RoundOver(winners, 2, 1);
				}
				else if (teamScores[1] >= winScore)
				{
					winners.AddRange(team2Array);
					RoundOver(winners, 2, 1);
				}
			}


		}*/

	}	

	// Function to spawn an object
	void Spawn (GameObject item, int max_x, int min_x, int max_y, int min_y)
	{
		spawnLoc = new Vector3 (Random.Range (min_x, max_x), Random.Range (min_y, max_y), 0);
		while (Physics.CheckSphere(spawnLoc, 1)) {
			spawnLoc = new Vector3 (Random.Range (min_x, max_x), Random.Range (min_y, max_y), 0);
		}
		if(Time.time > 4.0f){
			if (item.name == "Square Coins" || item.name == "Wave Coins" || item.name == "Cross Coins") {
				coinSpawnEffect(spawnLoc);
				spawnCoins(1.0f, item, spawnLoc);
			}else{
				GameObject s = Instantiate (item, spawnLoc, Quaternion.Euler (0, 0, 0)) as GameObject;
			}
		}
	}
	void spawnCoins(float delay, GameObject item, Vector3 spawnLoc){
		coinSpawnLoc = spawnLoc;
		coinSpawnObject = item;
		coinSpawnTime = delay;
	}

	// Function to move Hill
	void MoveHill ()
	{
		int floorNumber = Random.Range(0, floors.Length);
		floor = floors[floorNumber];
		spawnLoc = new Vector3 (floor.transform.position.x
		                        + Random.Range(-0.5f * floor.transform.lossyScale.x,0.5f * floor.transform.lossyScale.x),
		                        floor.transform.position.y + 1.75f, 0);
		myHill.transform.position = spawnLoc;
	//	myHill.transform.rotation = floor.transform.rotation;
	//	myHill.transform.parent = floor.transform;
	}

	// Function to add an amount to a player's score
	public static void AddScore (int playerNumber, int amount)
	{
		scores [playerNumber - 1] += amount;
	}

	public void Dead (int playerNumber)
	{
		if (gameType == "Deathmatch")
		{
			playersList = GameObject.FindGameObjectsWithTag("Player");
			if (playersList.Length < 3)
			{
				winners.Add(playersList[0].GetComponent<PlayerController>().playerNumber);
				RoundOver (winners, 1, 1);
			}
		}
		else if (gameType == "Team Deathmatch")
		{
			if (team1.Contains(playerNumber))
				team1.Remove(playerNumber);
			if (team2.Contains(playerNumber))
				team2.Remove(playerNumber);
			if (team1.Count == 0)
			{
				foreach (int member in MatchManager.team2)
					winners.Add(member);
				RoundOver(winners, 2, 1);
			}
			else if (team2.Count == 0)
			{
				foreach (int member in MatchManager.team1)
					winners.Add(member);
				RoundOver(winners, 2, 1);
			}
		}
	}

	public static void coinSpawnEffect(Vector3 spawnLoc)
	{
		ParticleSystem coinEffect = new ParticleSystem();
		coinEffect = Instantiate(Resources.Load("CoinSpawnEffect"), spawnLoc, Quaternion.Euler(0, 0, 0)) as ParticleSystem;
	}

	public static void ResetScores(){
		for (int i = 0; i < 4; i++)
			scores[i] = 0;
	}

	public void RoundOver (List<int> winnerNumbers, int numWinners, int score)
	{
		//for (int i = 0; i < numWinners; i++)
			//ScoreScreenManager.matchScores[winnerNumbers[i] - 1]+=score;
		MatchManager.roundNumber ++;
		Application.LoadLevel ("ScoresScreen");
	}
}
