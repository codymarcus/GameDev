using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	Vector3 startingPos;
	bool isHit = false;
	public float velocity = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		velocity *= 0.999f;
		if (Mathf.Abs(velocity) <= 0.01f)
		{
			velocity = 0f;
			isHit = false;
		}
		if (isHit)
		{
			if (gameObject.tag == "RightFloor")
				rigidbody.velocity = new Vector3 (velocity, 0, 0);
			if (gameObject.tag == "DownFloor")
				rigidbody.velocity = new Vector3 (0, -velocity, 0);
			rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		}
	}

	public void Hit () {
		isHit = true;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Boundary")
		{
			velocity = -velocity;
		}
	}
}
