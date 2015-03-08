using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public int playerNumber;
	public GameObject bullet;
	public GameObject barrel;
	public GameObject player;
	float canFire = 0f;
	bool isFiring = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		// If isFiring and weapon cooldown is zero...
		if (isFiring == true && canFire <= 0) 
		{ 
			// Create a new bullet
			GameObject b = new GameObject();
			b = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation) as GameObject;
			b.GetComponent<Bullet>().owner = playerNumber;

			// Destroy the bullet after a few seconds
			Destroy( b , 3.5F);

			// Set cooldown
			canFire = .3f;
		}

		// If cooldown is less than zero, set it to zero
		if (canFire <= 0)
			canFire = 0;
		// Otherwise, reduce it
		else
			canFire -= Time.deltaTime;
	}

	// Called when firing
	public void Fire() {
		isFiring = true;
	}

	// Called when stopping firing
	public void StopFire() {
		isFiring = false;
	}
}
