using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	public int playerNumber;
	public GameObject[] barrel;
	public bool splitted = false;
	public GameObject playerController;
	Transform player;
	Vector2 rightStick = new Vector2(0, 0);
	float angularVelocity = 200f;
	float radialDeadZone = 0.25f;
	Vector3 direction;
	Quaternion currentRotation;
	ParticleEmitter muzzleFlash;
	bool turnLeft = false;
	bool turnRight = false;

	// Use this for initialization
	void Start () {
		player = playerController.transform.Find("Player");
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

		if (Input.GetAxis (playerNumber + "RightH") < -0.1)
		{
			turnLeft = true;
			turnRight = false;
			//player.rotation = new Quaternion(0, 90, 0, 0);
//			foreach (Transform child in playerController.transform)
//				if (child.tag == "Hat")
//					child.rotation = new Quaternion(0, 270, 0, 0);
			//Quaternion wanted = new Quaternion(1, 180, 1, 1);
			//player.rotation = Quaternion.Lerp(player.rotation, wanted, Time.time * 0.001f);
		}
		if (Input.GetAxis (playerNumber + "RightH") > 0.1)
		{
			turnRight = true;
			turnLeft = false;
			//player.rotation = new Quaternion(0, 0, 0, 0);
//			foreach (Transform child in playerController.transform)
//				if (child.tag == "Hat")
//					child.rotation = new Quaternion(0, 90, 0, 0);
		}

		if (turnLeft){
			print (player.transform.eulerAngles.y);
			if(player.transform.eulerAngles.y < 180){
				player.Rotate (0, 10, 0);
			}else{
				turnLeft = false;
			}
		}
		if (turnRight){
			print (player.transform.eulerAngles.y);
			if(player.transform.eulerAngles.y > 0){
				player.Rotate (0, -10, 0);
			}else{
				turnRight = false;
			}
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
				barrel [1].GetComponent<Weapon> ().StopFire ();
				barrel [2].GetComponent<Weapon> ().StopFire ();
			} else {

				barrel [0].GetComponent<Weapon> ().StopFire ();
				barrel [1].GetComponent<Weapon> ().StopFire ();
				barrel [2].GetComponent<Weapon> ().StopFire ();
			}
		}
	}
}
