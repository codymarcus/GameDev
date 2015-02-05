using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public GameObject ammo;
	public string gameType;
	public float ammoSpawnTime = 10f;
	GameObject[] floors;
	GameObject[] players;
	GameObject floor;
	Vector3 spawnLoc;
	int[] scores = {0, 0, 0, 0};
	//Text scoreText;

//	public GameObject coin;
//	float moneySpawnTime = 5f;
	
	// Use this for initialization
	void Start () {
		//scoreText = (Text) GameObject.FindGameObjectWithTag ("Score");

		// Find and store all floors
		floors = GameObject.FindGameObjectsWithTag ("Floor");

		// Find and store all players
		players = GameObject.FindGameObjectsWithTag ("Player");

		// Player setup based on game mode
		foreach (GameObject player in players)
			// 1 life each in last man standing
			if (gameType == "Last Man Standing")
				player.GetComponent<PlayerController>().lives = 1;
	}
	
	// Update is called once per frame
	void Update () {

		//scoreText.text = "P1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2] + "\n" + "P4: " + scores[3];

//		moneySpawnTime -= Time.deltaTime;

//		if (moneySpawnTime <= 0)
//		{
//			Spawn (coin);
//			moneySpawnTime = 5f;
//		}

		// Ammo spawn timing
		ammoSpawnTime -= Time.deltaTime;

		if (ammoSpawnTime <= 0)
		{
			Spawn (ammo);
			ammoSpawnTime = 5f;
		}

	}

	// Function to spawn an object
	void Spawn (GameObject item)
	{
		int floorNumber = Random.Range(0, floors.Length - 1);
		floor = floors[floorNumber];
		spawnLoc = new Vector3 (floor.transform.position.x
		                        + Random.Range(-0.5f * floor.transform.lossyScale.x,0.5f * floor.transform.lossyScale.x),
		                        floor.transform.position.y + 1f, 0);
		Instantiate (item, spawnLoc, Quaternion.Euler(90, 30, 0));
	}

	// Function to add an amount to a player's score
	void addScore (int playerNumber, int amount)
	{
		scores [playerNumber - 1] += amount;
	}

	public void isLastMan(int playerNumber)
	{
		if (gameType == "Last Man Standing")
		{
			players = GameObject.FindGameObjectsWithTag ("Player");
			if (players.Length <= 2)
				foreach (GameObject player in players)
				{
					int playerNo = player.GetComponent<PlayerController> ().playerNumber;
					if (playerNo != playerNumber)
						Debug.Log ("Player " + players [0].GetComponent<PlayerController> ().playerNumber + " Wins!");
						// Last Player Alive Wins
				}
		}
	}
}
