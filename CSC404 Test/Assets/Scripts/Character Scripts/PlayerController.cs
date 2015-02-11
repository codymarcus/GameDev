using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject spawn;
	public int playerNumber;
	public GameObject[] spawns;
	public GameManager manager;
	public int lives;
	public GameObject self;

	private Vector3 moveDirection = Vector3.zero;

	int ammo = 5;
	float floatScore = 0;
	bool isAlive = true;

	float[] list;
	int i = 0;

	Vector3 screenPosition;

	// Use this for initialization
	void Start () {
		spawns = GameObject.FindGameObjectsWithTag ("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		GameManager.scores [playerNumber - 1] = (int) floatScore;
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

		// If player touches bullet that isn't theirs then die and destroy bullet
		if (other.gameObject.tag == "Bullet")
		{
			//int owner = other.gameObject.GetComponent<Bullet>().owner;
			//if (owner != playerNumber)
			//{
			//	Destroy (other.gameObject);
			//	Death();
			//}
			moveDirection = (other.gameObject.transform.position - other.gameObject.GetComponent<Bullet>().start)/other.gameObject.GetComponent<Bullet>().live_time;
			if (moveDirection == new Vector3(0,0,0)){

			}
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= 20;

			CharacterController controller = GetComponent<CharacterController>();
			controller.Move(moveDirection * Time.deltaTime);
			Destroy(other.gameObject);
		}

		// If player touches ammo, destroy it and add 5 ammo
		if (other.gameObject.tag == "Ammo")
		{
			Destroy(other.gameObject);
			ammo += 5;
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

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Hill")
		{
			floatScore += Time.deltaTime;
			Debug.Log("Player" + playerNumber + ": " + GameManager.scores[playerNumber-1]);
		}
	}

	void Death () {
		if (MatchManager.gameType != "King of the Hill" && isAlive == true)
		{
			lives--;
			if (lives > 0)
			{
				int spawnNumber = Random.Range (0, spawns.Length);
				spawn = spawns [spawnNumber];
				transform.position = spawn.transform.position;
			}
			else
			{
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
