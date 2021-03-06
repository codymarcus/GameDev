﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Vertical flying range and speed
	public float dropRange;
	public float speed;

	float curSpeed;
	bool isHit;

	// Top and bottom ends of flying range
	Vector3 startPos;
	Vector3 endPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = new Vector3 (startPos.x, startPos.y - dropRange);
		curSpeed = -speed;
	}
	
	// Update is called once per frame
	void Update () {
		// If enemy is hit
		if (isHit)
		{
			// Decelerate movement and eventually stop
			GetComponent<Rigidbody>().velocity = new Vector3 (0.97f * GetComponent<Rigidbody>().velocity.x, 0.97f * GetComponent<Rigidbody>().velocity.y, 0);
			if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) <= 0.01f)
				GetComponent<Rigidbody>().velocity = new Vector3 (0, GetComponent<Rigidbody>().velocity.y, 0);
			if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) <= 0.01f)
				GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, 0, 0);

			// Decelerate spinning and eventually stop
			GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, .7f * GetComponent<Rigidbody>().angularVelocity.z);
			if (Mathf.Abs(GetComponent<Rigidbody>().angularVelocity.z) <= 0.01f)
				GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, GetComponent<Rigidbody>().angularVelocity.y, 0);

			// If stopped, set new movement range
			if (GetComponent<Rigidbody>().velocity.x == 0 && GetComponent<Rigidbody>().velocity.y == 0 && GetComponent<Rigidbody>().angularVelocity.z == 0)
			{
				isHit = false;
				startPos = new Vector3 (transform.position.x, transform.position.y + (dropRange/2));
				endPos = new Vector3 (startPos.x, startPos.y - dropRange);
			}
		}
		// If enemy is not hit
		else
		{
			// Check position and set vertical direction accordingly
			if (transform.position.y >= startPos.y)
				curSpeed = -speed;
			else if (transform.position.y <= endPos.y)
				curSpeed = speed;
			GetComponent<Rigidbody>().velocity = new Vector3 (0, curSpeed, 0);
		}
		// If player falls through bottom of screen, teleport them to top
		if (transform.position.y <= -10)
			transform.position = new Vector3 (transform.position.x, 26);
		
		// If player moves through side of screen, teleport them to other side
		if (transform.position.x < -32.5f)
			transform.position = new Vector3 (32.5f, transform.position.y);
		if (transform.position.x > 32.5f)
			transform.position = new Vector3 (-32.5f, transform.position.y);
	}

	// Called if enemy is hit
	public void Hit() {
		isHit = true;
	}
}
