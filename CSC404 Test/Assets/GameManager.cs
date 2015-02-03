using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject coin;
	public GameObject ammo;
	public GameObject[] floors;
	GameObject floor;
	float ammoSpawnTime = 10f;
	float moneySpawnTime = 5f;
	Vector3 spawnLoc;
	//Text scoreText;
	int[] scores = {0, 0, 0, 0};
	
	// Use this for initialization
	void Start () {
		//scoreText = (Text) GameObject.FindGameObjectWithTag ("Score");
		floors = GameObject.FindGameObjectsWithTag ("Floor");
	}
	
	// Update is called once per frame
	void Update () {

		//scoreText.text = "P1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2] + "\n" + "P4: " + scores[3];

		moneySpawnTime -= Time.deltaTime;
		ammoSpawnTime -= Time.deltaTime;

		if (moneySpawnTime <= 0)
		{
			Spawn (coin);
			moneySpawnTime = 5f;
		}

		if (ammoSpawnTime <= 0)
		{
			Spawn (ammo);
			ammoSpawnTime = 10f;
		}

	}

	void Spawn (GameObject item)
	{
		int floorNumber = Random.Range(0, floors.Length - 1);
		floor = floors[floorNumber];
		spawnLoc = new Vector3 (floor.transform.position.x
		                        + Random.Range(-0.5f * floor.transform.lossyScale.x,0.5f * floor.transform.lossyScale.x),
		                        floor.transform.position.y + 1f, 0);
		Instantiate (item, spawnLoc, Quaternion.Euler(90, 30, 0));
	}

	void addScore (int playerNumber, int amount)
	{
		scores [playerNumber - 1] += amount;
	}
}
