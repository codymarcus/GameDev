using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	
	public float speed;
	Vector3 newPos;
	GameObject[] players;
	Transform target;
	GameObject closestPlayer;
	float closestDistance;
	bool isHit = false;
	bool offscreen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		offscreen = transform.position.x > 32.5f || transform.position.x < -32.5f || transform.position.y > 26 || transform.position.y < -10;
		if (isHit == false) {
			target = FindClosestPlayer ().transform;
			newPos = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
			transform.position = new Vector3(newPos.x, newPos.y, 0);
			if (transform.position.x > 40.0f)
				transform.position = new Vector3 (35.0f, transform.position.y);
			if (transform.position.x < -40.0f)
				transform.position = new Vector3 (-35.0f, transform.position.y);
			if (transform.position.y > 36.0f)
				transform.position = new Vector3 (transform.position.x, 31.0f);
			if (transform.position.y < -20.0f)
				transform.position = new Vector3 (transform.position.x, -15.0f);
		}

		if (isHit || offscreen)
		{
			// Decelerate movement and eventually stop
			GetComponent<Rigidbody>().velocity = new Vector3 (0.97f * GetComponent<Rigidbody>().velocity.x, 0.97f * GetComponent<Rigidbody>().velocity.y, 0);
			if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) <= 0.01f)
				GetComponent<Rigidbody>().velocity = new Vector3 (0, GetComponent<Rigidbody>().velocity.y, 0);
			if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) <= 0.01f)
				GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, 0, 0);

			if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) <= 2.0f && Mathf.Abs(GetComponent<Rigidbody>().velocity.x) <= 2.0f) 
				isHit = false;
		}
	}

	GameObject FindClosestPlayer() {
		closestDistance = 0;
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
		{
			float distance = (player.transform.position - transform.position).sqrMagnitude;
			if (closestDistance == 0 || distance < closestDistance)
			{

				closestPlayer = player;
				closestDistance = distance;
			}
		}
		return closestPlayer;
	}

	// Called if enemy is hit
	public void Hit() {
		isHit = true;
	}
}
