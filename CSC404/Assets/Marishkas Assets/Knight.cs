using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour {

	public float velocity;
	public Vector3 speed;
	GameObject[] players;
	GameObject[] hats;
	GameObject[] bullets;

	// Use this for initialization
	void Start () {
		// Ignore collisions with players
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
			Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ());
		
		bullets = GameObject.FindGameObjectsWithTag ("Bullet");
		foreach (GameObject bullet in bullets)
			Physics.IgnoreCollision (GetComponent<Collider> (), bullet.GetComponent<Collider> ());

		hats = GameObject.FindGameObjectsWithTag ("HatTrigger");
		foreach (GameObject hat in hats)
			if (hat.transform.parent.GetComponent<Hat> ().IsHit())
		{
			Physics.IgnoreCollision (GetComponent<Collider> (), hat.GetComponent<Collider> ());
			Physics.IgnoreCollision (GetComponent<Collider> (), hat.transform.parent.GetComponent<Collider> ());
		}
		
		// Set initial velocity
		GetComponent<Rigidbody> ().velocity = transform.right * velocity; // * Time.deltaTime;
		speed = GetComponent<Rigidbody> ().velocity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Hat") {
			if (!other.GetComponent<Hat> ().owner.GetComponent<PlayerController>().IsShield()){
				other.GetComponent<Hat>().Hit();
				Physics.IgnoreCollision (GetComponent<Collider> (), other.GetComponent<Collider> ());
//				Physics.IgnoreCollision (GetComponent<Collider> (), other.transform.parent.GetComponent<Collider> ());
			}
		}
	}
}
