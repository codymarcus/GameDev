using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float dropRange;
	public float speed = 4;

	float curSpeed;
	Vector3 startPos;
	Vector3 endPos;
	bool isHit;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = new Vector3 (startPos.x, startPos.y - dropRange);
		curSpeed = -speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
		{
			rigidbody.velocity = new Vector3 (0.97f * rigidbody.velocity.x, 0.97f * rigidbody.velocity.y, 0);
			if (Mathf.Abs(rigidbody.velocity.x) <= 0.01f)
				rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
			if (Mathf.Abs(rigidbody.velocity.y) <= 0.01f)
				rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, 0);
			
			rigidbody.angularVelocity = new Vector3 (0, 0, .7f * rigidbody.angularVelocity.z);
			if (Mathf.Abs(rigidbody.angularVelocity.z) <= 0.01f)
				rigidbody.angularVelocity = new Vector3 (0, rigidbody.angularVelocity.y, 0);

			if (rigidbody.velocity.x == 0 && rigidbody.velocity.y == 0 && rigidbody.angularVelocity.z == 0)
			{
				isHit = false;
				startPos = new Vector3 (transform.position.x, transform.position.y + (dropRange/2));
				endPos = new Vector3 (startPos.x, startPos.y - dropRange);
			}
		}
		else
		{
			if (transform.position.y >= startPos.y)
				curSpeed = -speed;
			else if (transform.position.y <= endPos.y)
				curSpeed = speed;
			rigidbody.velocity = new Vector3 (0, curSpeed, 0);
		}
	}

	public void Hit() {
		isHit = true;
	}
}
