using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public int playerNumber;
	//public GameObject bullet;
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
			switch (playerNumber)
			{
			case 1:
				b = Instantiate(Resources.Load("bulletGreen2"), barrel.transform.position, barrel.transform.rotation) as GameObject;
				break;
			case 2:
				b = Instantiate(Resources.Load("bulletBlue2"), barrel.transform.position, barrel.transform.rotation) as GameObject;
				break;
			case 3:
				b = Instantiate(Resources.Load("bulletRed2"), barrel.transform.position, barrel.transform.rotation) as GameObject;
				break;
			case 4:
				b = Instantiate(Resources.Load("bulletYellow2"), barrel.transform.position, barrel.transform.rotation) as GameObject;
				break;
			}

			b.GetComponent<Bullet>().owner = playerNumber;

			// Destroy the bullet after a few seconds
			Destroy( b , 1.5F);

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
