using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject spawn;
	public int playerNumber;
	public GameObject[] spawns;
	public GameManager manager;
	public int lives = 1000;
	public GameObject self;
	public GameObject player;

	Vector3 speed = new Vector3();
	public CharacterController controller;


	float doubleJump = 3.0F;
	bool canDJump = false;

	int ammo = 5;
	float floatScore = 0;
	bool isAlive = true;

	Vector3 screenPosition;

	// Use this for initialization
	void Start () {
		spawns = GameObject.FindGameObjectsWithTag ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.z != 0)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		GameManager.scores [playerNumber - 1] = (int) floatScore;
		if (speed != null)
		{
			transform.Translate (speed);
			speed = new Vector3 (speed.x * 0.95F, speed.y * 0.95F, 0);
		}

//		if (Input.GetButton (playerNumber + "Jump") && canDJump)
//			controller.Move (new Vector3 (0, doubleJump, 0) * Time.deltaTime);
	}

	void OnGUI () {
		GUI.Label(new Rect(screenPosition.x-10, screenPosition.y-5, 100, 100),("P" + playerNumber));
	}

	void OnTriggerEnter(Collider other)
	{
		// If player touches a death object then die
		if (other.gameObject.tag == "Death")
		{
			Death();
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Floor")
			canDJump = true;
		{
			speed = new Vector3 (speed.x, 0, speed.z);
		}

		if (other.gameObject.tag == "HeavyFloor")
		{
			other.gameObject.GetComponentInParent<HeavyFloor>().NotWeighDown();
			other.gameObject.GetComponentInParent<CrazyFloor>().Hit();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Hill")
		{
			floatScore += Time.deltaTime;
			Debug.Log("Player" + playerNumber + ": " + GameManager.scores[playerNumber-1]);
		}

		if (other.gameObject.tag == "HeavyFloor")
		{
			other.gameObject.GetComponentInParent<HeavyFloor>().WeighDown();
		}
	}

	public void Update_color(int color)
	{
		if (color == 1) {
			self.renderer.material.color = Color.red;
		}
		if (color == 2) {
			self.renderer.material.color = Color.green;
		}
	}

	void Death () {
		if (isAlive == true)
		{
			lives--;
			if (lives > 0)
			{
				Debug.Log("P" + playerNumber + " died! \n Lives: " + lives);
				int spawnNumber = Random.Range (0, spawns.Length);
				spawn = spawns [spawnNumber];
				transform.position = spawn.transform.position;
			}
			else
			{
				Debug.Log("P" + playerNumber + " eliminated!");
				Destroy(this.gameObject);
				manager.Dead(playerNumber);
				isAlive = false;
			}
		}
		else
		{
			int spawnNumber = Random.Range (0, spawns.Length);
			spawn = spawns [spawnNumber];
			transform.position = spawn.transform.position;
		}
	}

	public bool UseAmmo() {
		if (ammo > 0)
		{
			ammo--;
			return true;
		}
		else
			return false;
	}
	
}
