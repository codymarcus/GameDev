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

	int hats = 1;
	bool[] hatPlaces = {true, false, false, false};

	float timeInShield;

	bool isShield = true;

	GameObject[] players;

	Vector3 speed = new Vector3();
	public CharacterController controller;


	float doubleJump = 3.0F;
	bool canDJump = false;

	int addedPoints;

	int ammo = 5;
	float floatScore = 0;
	bool inHill = false;
	bool isAlive = true;

	Vector3 screenPosition;

	GUIStyle livesFont;
	float fadeTime = 2f;
	Color color = Color.white;
	Color playerColor;

	// Use this for initialization
	void Start () {
		//timeInShield = shieldTime;

		spawns = GameObject.FindGameObjectsWithTag ("Spawn");

		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
			if (player != gameObject)
				Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());

		livesFont = new GUIStyle();
		livesFont.fontSize = 15;
		livesFont.fontStyle = FontStyle.Bold;
		playerColor = self.GetComponent<Renderer>().material.color;
		livesFont.normal.textColor = playerColor;

	}
	
	// Update is called once per frame
	void Update () {
		if (timeInShield > 0)
			timeInShield -= Time.deltaTime;
		else
			isShield = false;

		if (transform.position.z != 0)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		//GameManager.scores [playerNumber - 1] = (int) floatScore;
		/*
		if (speed != null)
		{
			transform.Translate (speed);
			speed = new Vector3 (speed.x * 0.95F, speed.y * 0.95F, 0);
		}


		if (GetComponent<CharacterController>().velocity.x < 0)
			self.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		else if (GetComponent<CharacterController>().velocity.x > 0)
			self.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
		*/

		if (fadeTime > 0)
			fadeTime -= Time.deltaTime;

		// If player falls through bottom of screen, teleport them to top
		if (transform.position.y <= -10)
			transform.position = new Vector3 (transform.position.x, 26);

		// If player moves through side of screen, teleport them to other side
		if (transform.position.x < -32.5f)
			transform.position = new Vector3 (32.5f, transform.position.y);
		if (transform.position.x > 32.5f)
			transform.position = new Vector3 (-32.5f, transform.position.y);
	}

	void OnGUI () {
		color.a = 1;
		GUI.color = color;
		GUI.Label(new Rect(screenPosition.x-10, screenPosition.y-5, 100, 100),("P" + playerNumber));

//		playerColor.a = 1;
//		GUI.color = playerColor;

//		if (inHill)
//		GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),(GameManager.scores [playerNumber - 1]+""));

		playerColor.a = fadeTime;
		GUI.color = playerColor;
		//if (manager.gameType == "Deathmatch" || manager.gameType == "Team Deathmatch")
		//{
		//	if (lives > 1)
				GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),("+" + addedPoints), livesFont);
		//	else
		//		GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),(lives + " Life!"), livesFont);
		//}
		//else
		//	GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),("Infinite Lives!"), livesFont);
	}

	void OnTriggerEnter(Collider other)
	{
		// If player touches a death object then die
		if (other.gameObject.tag == "Death" || other.gameObject.tag == "Enemy")
		{
			Death();
		}

		if (other.gameObject.tag == "Hill")
		{
			inHill = true;
		}

		if (other.gameObject.tag == "HatTrigger")
		{
			other.GetComponentInParent<Hat>().NewOwner(gameObject, playerNumber);
		}

		if (other.gameObject.tag == "Money")
		{
			if (hats > 0)
			{
				Destroy(other.gameObject);
				fadeTime = 2f;
				addedPoints = (int) Mathf.Pow(2f, hats-1);
				GameManager.AddScore(playerNumber, addedPoints);
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Floor")
			canDJump = true;
		{
			speed = new Vector3 (speed.x, 0, speed.z);
		}

		if (other.gameObject.tag == "HeavyFloorTrigger")
		{
			other.gameObject.GetComponentInParent<HeavyFloor>().NotWeighDown();
		}

		if (other.gameObject.tag == "Hill")
		{
			inHill = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Hill")
		{
			floatScore += Time.deltaTime;
		}

		if (other.gameObject.tag == "HeavyFloorTrigger")
		{
			other.gameObject.GetComponentInParent<HeavyFloor>().WeighDown();
		}
	}

	public void Update_color(int color)
	{
		if (color == 1) {
			self.GetComponent<Renderer>().material.color = Color.red;
		}
		if (color == 2) {
			self.GetComponent<Renderer>().material.color = Color.green;
		}
	}

	void Death () {
		if (!isShield)
		{
			if (isAlive == true && manager.gameType != "King of the Hill")
			{
				lives--;

				if (lives > 0)
				{
					fadeTime = 2f;
					Debug.Log("P" + playerNumber + " died! \n Lives: " + lives);
					int spawnNumber = Random.Range (0, spawns.Length);
					spawn = spawns [spawnNumber];
					//timeInShield = shieldTime;
					isShield = true;
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
				//timeInShield = shieldTime;
				isShield = true;
				transform.position = spawn.transform.position;
			}
		}
	}

	public void LoseHat() {
		if (hats > 0)
		{
			hats--;
			for (int i=0; i < 4; i++)
				if (!hatPlaces[i])
				{
					hatPlaces[i-1] = false;
					Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
					return;
				}
			hatPlaces[3] = false;
			Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
		}
	}

	public void AddHat() {
		hats++;
		for (int i=0; i < 4; i++)
			if (!hatPlaces[i])
			{
				hatPlaces[i] = true;
				Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
				break;
			}
	}

	public int NumHats() {
		return hats;
	}

	public bool[] HatPlaces() {
		return hatPlaces;
	}
}
