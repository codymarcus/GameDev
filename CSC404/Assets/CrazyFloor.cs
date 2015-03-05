﻿using UnityEngine;
using System.Collections;

public class CrazyFloor : MonoBehaviour {

	public double respawnTime;
	public int returnSpeed = 7;
	public double timeLimit = 2f;


	double timeToRespawn = 5f;
	double timeSoFar;
	bool isHit = false;

	// Starting position and angle
	Vector3 startPos;
	Quaternion startAngle;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startAngle = transform.rotation;
		timeSoFar = timeLimit;
	}
	
	// Update is called once per frame
	void Update () {

		// Determine if floor is hit using starting position and angle
		if (transform.position == startPos && transform.rotation == startAngle)
		{
			isHit = false;
			timeToRespawn = 5;
		}
		else
			isHit = true;

		if (timeSoFar <= 0)
		{
			transform.position = startPos;
			transform.rotation = startAngle;
			rigidbody.velocity = new Vector3 (0, 0);
			rigidbody.angularVelocity = new Vector3 (0, 0, 0);
			timeSoFar = timeLimit;
		}

		// If floor is hit
		if (isHit)
		{
			timeToRespawn -= Time.deltaTime;
			// If respawn time has elapsed, move towards starting position and angle
			if (timeToRespawn <= 0)
			{
				Vector3 newPos = Vector3.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);
				transform.position = new Vector3(newPos.x, newPos.y, 0);
				Quaternion newAngle = Quaternion.RotateTowards(transform.rotation, startAngle, returnSpeed * 6 * Time.deltaTime);
				transform.rotation = new Quaternion(newAngle.x, newAngle.y, newAngle.z, newAngle.w);
				timeSoFar -= Time.deltaTime;
			}
			// Otherwise, decelerate the floor's velocity and angular velocity
			else
			{
				rigidbody.velocity = new Vector3 (0.95f * rigidbody.velocity.x, 0.95f * rigidbody.velocity.y, 0);
				if (Mathf.Abs(rigidbody.velocity.x) <= 0.01f)
					rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
				if (Mathf.Abs(rigidbody.velocity.y) <= 0.01f)
					rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, 0);

				rigidbody.angularVelocity = new Vector3 (0, 0, .7f * rigidbody.angularVelocity.z);
				if (Mathf.Abs(rigidbody.angularVelocity.z) <= 0.01f)
					rigidbody.angularVelocity = new Vector3 (0, rigidbody.angularVelocity.y, 0);
			}
		}
	}

	// Called when floor is hit
	public void Hit() {
		isHit = true;
		timeToRespawn = respawnTime;
	}
}