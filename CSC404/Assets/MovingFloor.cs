using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	public float xVelocity = 5f;
	public float yVelocity = 5f;
	public float xRange;
	public float yRange;
	float startX;
	float startY;
	bool xOut = false;
	bool yOut = false;
	float thisXVelocity;
	float thisYVelocity;

	// Use this for initialization
	void Start () {
		startX = transform.position.x;
		startY = transform.position.y;
		thisXVelocity = xVelocity/50;
		thisYVelocity = yVelocity/50;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(transform.position.x + thisXVelocity, transform.position.y + thisYVelocity);

		if (!xOut && (transform.position.x > startX + xRange || transform.position.x < startX))
		{
			thisXVelocity = -thisXVelocity;
			xOut = true;
		}
		else
			xOut = false;

		if (!yOut && (transform.position.y > startY + yRange || transform.position.y < startY))
		{
			thisYVelocity = -thisYVelocity;
			yOut = true;
		}
		else
			yOut = false;
	}
}
