﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject spawn;
	public int playerNumber;
	int ammo = 5;

	// Use this for initialization
	void Start () {
	
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
			Destroy(other.gameObject);
			Death ();
		}
		if (other.gameObject.tag == "Ammo")
		{
			Destroy(other.gameObject);
			ammo += 5;
		}
	}

	void Death () {
		transform.position = spawn.transform.position;
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
