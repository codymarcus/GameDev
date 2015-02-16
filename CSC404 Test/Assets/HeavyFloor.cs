using UnityEngine;
using System.Collections;

public class HeavyFloor : MonoBehaviour {

	public float dropSpeed = 3f;
	public double respawnTime;
	public float returnSpeed;
	double timeToRespawn;
	bool weighedDown = false;
	Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (weighedDown == true)
			rigidbody.velocity = Vector3.down * dropSpeed;
		else
			rigidbody.velocity = Vector3.zero;

		if (timeToRespawn < -1)
			timeToRespawn = -1;
		else if (timeToRespawn <= 0)
		{
			Vector3 newPos = Vector3.MoveTowards(transform.position, startPos, returnSpeed * Time.deltaTime);
			transform.position = new Vector3(newPos.x, newPos.y, 0);
			
		}
		else
			timeToRespawn -= Time.deltaTime;
	}

	public void WeighDown() {
		weighedDown = true;
	}

	public void NotWeighDown() {
		weighedDown = false;
		timeToRespawn = respawnTime;
	}

}
