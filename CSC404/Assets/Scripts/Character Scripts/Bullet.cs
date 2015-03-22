using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;
	public GameManager manager;
	GameObject[] players;
	GameObject[] hats;
	GameObject[] bullets;
	GameObject[] heavyFloors;
	float destroyTime = 0.01F;
	bool isDestroy = false;

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
			if (hat.transform.parent.GetComponent<Hat> ().ownerNumber == owner &&
			    !hat.transform.parent.GetComponent<Hat> ().IsHit())
			{
				Physics.IgnoreCollision (GetComponent<Collider> (), hat.GetComponent<Collider> ());
				Physics.IgnoreCollision (GetComponent<Collider> (), hat.transform.parent.GetComponent<Collider> ());
			}

		// Set initial velocity
		GetComponent<Rigidbody> ().velocity = transform.up * velocity; // * Time.deltaTime;
		speed = GetComponent<Rigidbody> ().velocity;
		muzzleEffect ();
	}
	
	// Update is called once per frame
	void Update () {
		// If bullet moves through side of screen, teleport it to other side
		if (transform.position.x < -32.5f)
			transform.position = new Vector3 (32.5f, transform.position.y);
		if (transform.position.x > 32.5f)
			transform.position = new Vector3 (-32.5f, transform.position.y);

		// If isDestroy is true, wait a short amount of time then destroy the bullet
		// The wait is so that the bullet's physics can briefly affect the object it hits
		if (isDestroy == true)
		{
			destroyTime -= Time.deltaTime;
			if (destroyTime <= 0)
				Destroy (gameObject);
		}
	}

	void hitEffect()
	{
		ParticleSystem coinEffect = new ParticleSystem();
		coinEffect = Instantiate(Resources.Load("hitEffectGreen"), transform.position, transform.rotation) as ParticleSystem;
		Destroy(coinEffect, 0);
	}

	void muzzleEffect()
	{
		ParticleSystem coinEffect = new ParticleSystem();
		coinEffect = Instantiate(Resources.Load("muzzleEffectGreen"), transform.position, transform.rotation) as ParticleSystem;
		Destroy(coinEffect, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		// Upon touching a "crazy" floor, call its hit function and set isDestroy to true
		if (other.gameObject.tag == "Floor") {
			//other.GetComponent<CrazyFloor>().Hit();
			//isDestroy = true;
			Destroy(gameObject);
			hitEffect();
		}

		// Upon touching a heavy floor, destroy bullet
		if (other.gameObject.tag == "HeavyFloor") {
			Destroy(gameObject);
			hitEffect();
		}

		// Exploding Floor
		if (other.gameObject.tag == "ExplodingFloor") {
			other.GetComponent<ExplodingFloor>().Hit();
			Destroy(gameObject);
			hitEffect();
		}

		// Upon touching an enemy, call its hit function and set isDestroy to true
		if (other.gameObject.tag == "Enemy") {
			if (other.gameObject.GetComponent<Enemy>() != null)
				other.gameObject.GetComponent<Enemy>().Hit();
			else
				other.gameObject.GetComponent<Follow>().Hit();
			isDestroy = true;
			hitEffect();
		}

		if (other.gameObject.tag == "Hat") {
			if (other.GetComponent<Hat> ().ownerNumber != owner)
			{
				if (!other.GetComponent<Hat> ().owner.GetComponent<PlayerController>().IsShield()){
					other.GetComponent<Hat>().Hit();
					isDestroy = true;
				}
			}
		}
	}
}
