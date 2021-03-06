﻿using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour {

	public GameObject owner;
	public int ownerNumber;
	GameObject[] players;
	GameObject[] hats;
	GameObject[] floors;
	bool isHit = false;
	float hitTime = 0.5f;
	float timeLeft;
	int hatNumber = 1;
	bool isDropping = false;
	bool ignoreTrigger = false;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
		{
			Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
		}

		hats = GameObject.FindGameObjectsWithTag ("Hat");
		foreach (GameObject hat in hats)
			Physics.IgnoreCollision(GetComponent<Collider>(), hat.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0)
			{
				ignoreTrigger = false;
				//foreach (GameObject player in players)
				//s	Physics.IgnoreCollision(GetComponentInChildren<Collider>(), player.GetComponent<Collider>(), false);
			}
		}
		else
		{
//			transform.rotation = new Quaternion(0,0,0,0);
			transform.right = -owner.transform.Find("Player").forward;
//			for (int i=0; i<4; i++)
//				if (!owner.GetComponent<PlayerController>().HatPlaces()[i] && i+1 == hatNumber)
//					hatNumber--;
			if (!isDropping)
				transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + (hatNumber*1.2f));
			else
			{
				if (transform.position.y > owner.transform.position.y + hatNumber*1.2f)
					transform.position = new Vector3(owner.transform.position.x, transform.position.y - 0.0001f);
				else
					isDropping = false;
			}
		}

		// If hat falls through bottom of screen, teleport it to top
		if (transform.position.y <= -10)
			transform.position = new Vector3 (transform.position.x, 26);
		
		// If hat moves through side of screen, teleport it to other side
		if (transform.position.x < -32.5f)
			transform.position = new Vector3 (32.5f, transform.position.y);
		if (transform.position.x > 32.5f)
			transform.position = new Vector3 (-32.5f, transform.position.y);

	}

	public void Hit() {
		ignoreTrigger = true;
		timeLeft = hitTime;
		isHit = true;
		transform.parent = null;
		owner.GetComponent<PlayerController> ().LoseHat ();
		foreach (GameObject player in players) {
			if (player.activeSelf)
				Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> ());
		}
	}

	public void NewOwner (GameObject newOwner, int newNumber) {
		if (isHit)
		{
			if (owner != newOwner || timeLeft <= 0)
			{
				isHit = false;
				Debug.Log(owner + "+" + newOwner);
				owner = newOwner;
				ownerNumber = newNumber;
				owner.GetComponent<PlayerController>().AddHat();
				hatNumber = owner.GetComponent<PlayerController>().NumHats();
				GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
				GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, 0);
				transform.rotation = new Quaternion(0,0,0,0);
				transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + (hatNumber*1.2f));
				transform.parent = owner.transform;

				foreach (GameObject player in players){
					if (player.activeSelf)
						Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
				}
			}
		}
	}

	public bool IsHit() {
		return isHit;
	}

	public int GetHatNumber() {
		return hatNumber;
	}

	public void SetHatNumber(int num) {
		hatNumber = num;
		isDropping = true;
	}

	public bool IgnoreTrigger(){
		return ignoreTrigger;
	}
}
