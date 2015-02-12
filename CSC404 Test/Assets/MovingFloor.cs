using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	Vector3 startingPos;
	bool isHit = false;
	float timeToMove = 10.0F;

	// Use this for initialization
	void Start () {
		startingPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
			timeToMove -= Time.deltaTime;
		if (timeToMove <= 0)
		{
			transform.position = startingPos;
			timeToMove = 5.0F;
		}
	}

	public void Hit () {
		isHit = true;
	}
}
