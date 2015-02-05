using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject spawn;
	public int playerNumber;
	public GameObject[] spawns;
	public GameManager manager;
	public int lives;
	int ammo = 5;
	
	// Use this for initialization
	void Start () {
		spawns = GameObject.FindGameObjectsWithTag ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Death")
		{
			Death();
		}
		if (other.gameObject.tag == "Money")
		{
			Destroy(other.gameObject);

		}
		if (other.gameObject.tag == "Bullet")
		{
			int owner = other.gameObject.GetComponent<Bullet>().owner;
			if (owner != playerNumber)
			{
				Destroy (other.gameObject);
				Death();
			}
		}
		if (other.gameObject.tag == "Ammo")
		{
			Destroy(other.gameObject);
			ammo += 5;
		}
	}

	void Death () {
		lives--;
		if (lives > 0)
		{
			int spawnNumber = Random.Range (0, spawns.Length);
			spawn = spawns [spawnNumber];
			transform.position = spawn.transform.position;
		}
		else
		{
			Destroy(this.gameObject);
			manager.Dead(playerNumber);
		}
	}

	public bool UseAmmo() {
		if (ammo > 0)
		{
			ammo--;
			return true;
		}
		else
			return false;
	}
}
