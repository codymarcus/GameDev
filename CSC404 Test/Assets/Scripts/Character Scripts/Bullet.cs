using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;
	public GameManager manager;
	GameObject[] players;
	float destroyTime = 0.01F;
	bool isDestroy = false;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
		{
			Physics.IgnoreCollision(collider, player.collider);
		}
		rigidbody.velocity = transform.up * velocity; // * Time.deltaTime;
		speed = rigidbody.velocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestroy == true)
		{
			destroyTime -= Time.deltaTime;
			if (destroyTime <= 0)
				Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor") {
			other.gameObject.GetComponent<CrazyFloor>().Hit();
			isDestroy = true;
		}

		if (other.gameObject.tag == "HeavyFloor") {
			Destroy(gameObject);
		}

	}
	
	void onControllerColliderHit (ControllerColliderHit hit)
	{
		rigidbody.detectCollisions = false;
	}
	
}
