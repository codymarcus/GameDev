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

	GameObject myHill;
	float floorSpawnTime = 0.5f;
	GameObject[] floors;
	public GameObject[] players;
	public GameObject spawnFloor;
	Vector3 spawnLoc;

	GameObject[] playersList;

	List<int> winners = new List<int> ();

	// Teams for team gametypes
	ArrayList team1 = new ArrayList();
	ArrayList team2 = new ArrayList();

	int[] team1Array;
	int[] team2Array;

	public static int[] scores = {0, 0, 0, 0};
	public static int[] teamScores = {0, 0};

	//Text scoreText;

	float curAmmoTime;
	float curHillTime;
	float curFloorTime;

//	public GameObject coin;
//	float moneySpawnTime = 5f;
	
	// Use this for initialization
	void Start () {

//		scoreText = (Text) GameObject.FindGameObjectWithTag ("Score");

		team1Array = team1.ToArray(typeof(int)) as int[];
		team2Array = team1.ToArray(typeof(int)) as int[];

		curAmmoTime = ammoSpawnTime;
		curHillTime = hillChangeTime;
		curFloorTime = floorSpawnTime;

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

		//scoreText.text = "P1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2] + "\n" + "P4: " + scores[3];

		curFloorTime -= Time.deltaTime;
//
//		if (curFloorTime <= 0)
//		{
//			Spawn (spawnFloor);
//			curFloorTime = floorSpawnTime;
//		}

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

			Debug.Log(teamScores[0] + "-" + teamScores[1]);

			teamScores[0] = scores[team1Array[0]-1] + scores[team1Array[1]-1];
			teamScores[1] = scores[team2Array[0]-1] + scores[team2Array[1]-1];

			if (MatchManager.teamDynamic == "FFA")
				for (int i = 0; i < 4; i++)
					if (scores[i] >= winScore)
					{
						winners.Add(i+1);
						RoundOver(winners, 1, 1);
					}
			else
			{
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

		}

	}

	// Function to spawn an object
	void Spawn (GameObject item)
	{
		spawnLoc = new Vector3 (Random.Range (-38, 18), Random.Range (-15, 17), 0);
		GameObject s = Instantiate (item, spawnLoc, Quaternion.Euler(0, 0, 0)) as GameObject;
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
	}

	// Function to add an amount to a player's score
	void addScore (int playerNumber, int amount)
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
//		else if (gameType == "WANTED")
//		{
//			if (team1.Contains(playerNumber))
//				team1.Remove(playerNumber);
//			if (team2.Contains(playerNumber))
//				team2.Remove(playerNumber);
//			if (team1.Count == 0)
//			{
//				foreach (int member in MatchManager.team2)
//					winners.Add(member);
//				RoundOver(winners, 3, 1);
//			}
//			else if (team2.Count == 0)
//			{
//				foreach (int member in MatchManager.team1)
//					winners.Add(member);
//				RoundOver(winners, 1, 1);
//			}
//		}
	}

	public void RoundOver (List<int> winnerNumbers, int numWinners, int score)
	{
		for (int i = 0; i < numWinners; i++)
			ScoreScreenManager.matchScores[winnerNumbers[i] - 1]+=score;
		Application.LoadLevel (1);
	}
}
