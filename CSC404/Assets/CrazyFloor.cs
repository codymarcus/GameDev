using UnityEngine;
using System.Collections;

public class CrazyFloor : MonoBehaviour {

	public double respawnTime;
	public int returnSpeed;
	public double timeLimit;
	public GameObject platform;
	GameObject[] players;
	
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

		players = GameObject.FindGameObjectsWithTag ("Player");


		foreach (GameObject player in players)
		if (player.GetComponent<PlayerController>().moved && gameObject.tag == "SpawnFloor") {
				Physics.IgnoreCollision (GetComponent<Collider> (), player.GetComponent<Collider> (), true);
			}
		//player.transform.position.y < transform.position.y && 
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
			GetComponent<Rigidbody>().velocity = new Vector3 (0, 0);
			GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, 0);
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
				GetComponent<Rigidbody>().velocity = new Vector3 (0.95f * GetComponent<Rigidbody>().velocity.x, 0.95f * GetComponent<Rigidbody>().velocity.y, 0);
				if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) <= 0.01f)
					GetComponent<Rigidbody>().velocity = new Vector3 (0, GetComponent<Rigidbody>().velocity.y, 0);
				if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) <= 0.01f)
					GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, 0, 0);

				GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, .7f * GetComponent<Rigidbody>().angularVelocity.z);
				if (Mathf.Abs(GetComponent<Rigidbody>().angularVelocity.z) <= 0.01f)
					GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, GetComponent<Rigidbody>().angularVelocity.y, 0);
			}
		}
	}

	// Called when floor is hit
	public void Hit() {
		isHit = true;
		timeToRespawn = respawnTime;
	}
}
