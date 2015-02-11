using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	public int playerNumber;
	Vector2 rightStick = new Vector2(0, 0);
	float angularVelocity = 30f;
	float radialDeadZone = 0.25f;
	public Vector3 direction;
	Quaternion currentRotation;
	//public int RotateSpeed = 300;

	// Use this for initialization
	void Start () {
		rightStick = new Vector2 (Input.GetAxis (playerNumber + "RightH"), Input.GetAxis (playerNumber + "RightV"));
		direction = new Vector3 (rightStick.x, -1 * rightStick.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate (-(Vector3.forward * RotateSpeed * Time.deltaTime * Input.GetAxis(playerNumber + "RightH")));
		rightStick = new Vector2 (Input.GetAxis (playerNumber + "RightH"), Input.GetAxis (playerNumber + "RightV"));
		direction = new Vector3 (rightStick.x, -1 * rightStick.y, 0);
		if (direction.magnitude > radialDeadZone)
		{
			currentRotation = Quaternion.LookRotation(Vector3.forward, direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime * angularVelocity);
		}

	}
}
