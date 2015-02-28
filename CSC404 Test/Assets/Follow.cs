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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
		{
			// Decelerate movement and eventually stop
			rigidbody.velocity = new Vector3 (0.97f * rigidbody.velocity.x, 0.97f * rigidbody.velocity.y, 0);
			if (Mathf.Abs(rigidbody.velocity.x) <= 0.01f)
				rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
			if (Mathf.Abs(rigidbody.velocity.y) <= 0.01f)
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, 0);

			if (rigidbody.velocity.x < 2 && rigidbody.velocity.y < 2)
				isHit = false;
		}
		else 
		{
			target = FindClosestPlayer ().transform;
			newPos = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
			transform.position = new Vector3(newPos.x, newPos.y, 0);
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
