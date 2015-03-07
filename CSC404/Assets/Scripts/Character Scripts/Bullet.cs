using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;
	public GameManager manager;
	GameObject[] players;
	GameObject[] heavyFloors;
	float destroyTime = 0.01F;
	bool isDestroy = false;

	// Use this for initialization
	void Start () {
		// Ignore collisions with players

		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
			Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());

		// Ignore collisions with heavy floors
		heavyFloors = GameObject.FindGameObjectsWithTag ("HeavyFloor");
		foreach (GameObject heavyFloor in heavyFloors)
			Physics.IgnoreCollision(GetComponent<Collider>(), heavyFloor.GetComponent<Collider>());

		// Set initial velocity
		GetComponent<Rigidbody>().velocity = transform.up * velocity; // * Time.deltaTime;
		speed = GetComponent<Rigidbody>().velocity;
	}
	
	// Update is called once per frame
	void Update () {
		// If isDestroy is true, wait a short amount of time then destroy the bullet
		// The wait is so that the bullet's physics can briefly affect the object it hits
		if (isDestroy == true)
		{
			destroyTime -= Time.deltaTime;
			if (destroyTime <= 0)
				Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Upon touching a "crazy" floor, call its hit function and set isDestroy to true
	
		if (other.gameObject.tag == "Floor") {
			other.gameObject.GetComponent<CrazyFloor>().Hit();
			isDestroy = true;
		}
		//if (other.gameObject.tag == "Player") {
		//	isDestroy = true;
			//other.gameObject.GetComponent<PlayerController>().Death();
		//}
		if (other.gameObject.tag == "deathwall") {
			isDestroy = true;
		}
		// Upon touching a heavy floor, destroy bullet
		if (other.gameObject.tag == "HeavyFloor") {
			Destroy(gameObject);
		}

		// Exploding Floor
		if (other.gameObject.tag == "ExplodingFloor") {
			other.GetComponent<ExplodingFloor>().Hit();
			Destroy(gameObject);
		}

		// Upon touching an enemy, call its hit function and set isDestroy to true
		if (other.gameObject.tag == "Enemy") {
			if (other.gameObject.GetComponent<Enemy>() != null)
				other.gameObject.GetComponent<Enemy>().Hit();
			else
				other.gameObject.GetComponent<Follow>().Hit();
			isDestroy = true;
		}

		if (other.gameObject.tag == "Hat") {
			isDestroy = true;
		}
	}
}
