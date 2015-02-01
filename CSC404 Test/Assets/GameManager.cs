using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject coin;
	public GameObject[] floors;
	GameObject floor;
	float moneySpawnTime = 5f;
	Vector3 moneySpawnLoc;

	// Use this for initialization
	void Start () {
		floors = GameObject.FindGameObjectsWithTag ("Floor");
	}
	
	// Update is called once per frame
	void Update () {

		moneySpawnTime -= Time.deltaTime;

		if (moneySpawnTime <= 0)
		{
			int floorNumber = Random.Range(0, floors.Length - 1);
			floor = floors[floorNumber];
			moneySpawnLoc = new Vector3 (floor.transform.position.x
			                             + Random.Range(-0.5f * floor.transform.lossyScale.x,0.5f * floor.transform.lossyScale.x),
			                             floor.transform.position.y + 1f, 0);
			Instantiate (coin, moneySpawnLoc, Quaternion.Euler(90, 30, 0));
			moneySpawnTime = 5f;
		}
	}
}
