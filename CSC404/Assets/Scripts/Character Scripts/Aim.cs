using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	public int playerNumber;
	public GameObject[] barrel;
	public bool splitted = false;
	Vector2 rightStick = new Vector2(0, 0);
	float angularVelocity = 200f;
	float radialDeadZone = 0.25f;
	Vector3 direction;
	Quaternion currentRotation;
	ParticleEmitter muzzleFlash;


	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {

		// Get right stick direction
		rightStick = new Vector2 (Input.GetAxis (playerNumber + "RightH"), Input.GetAxis (playerNumber + "RightV"));
		direction = new Vector3 (rightStick.x, -1 * rightStick.y, 0);

		// If right stick is pushed in a direction...
		if (direction.magnitude > radialDeadZone)
		{
			// Aim in that direction
			currentRotation = Quaternion.LookRotation(Vector3.forward, direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime * angularVelocity);

		}

		if (splitted == true) {
			if (Input.GetButton (playerNumber + "Fire1")) {
				// Fire
				//Instantiate (muzzleFlash, transform.position, Quarternion.identity);
				Debug.Log (splitted);

				for (int i =0; i<3; i++) {
						barrel [i].GetComponent<Weapon> ().Fire ();
				}
			} else {
				for (int i =0; i<3; i++) {
						barrel [i].GetComponent<Weapon> ().StopFire ();
				}
			}
		}

		// Otherwise, do not fire 
		else {
			if (Input.GetButton (playerNumber + "Fire1")) {
				// Fire
				//Instantiate (muzzleFlash, transform.position, Quarternion.identity);
				//Debug.Log (splitted);

				barrel [0].GetComponent<Weapon> ().Fire ();
			} else {

				barrel [0].GetComponent<Weapon> ().StopFire ();
				barrel [1].GetComponent<Weapon> ().StopFire ();
				barrel [2].GetComponent<Weapon> ().StopFire ();
			}
		}
	}
}
