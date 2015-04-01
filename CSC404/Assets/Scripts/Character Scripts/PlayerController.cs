using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Slider PlayerScoreSlider;

	public GameObject spawn;
	public int playerNumber;
	public GameObject[] spawns;
	public GameManager manager;
	public int lives = 1000;
	public GameObject self;
	public GameObject MagnetEffect;
	public ParticleSystem shieldEffect;
	public ParticleSystem magnetParticleEffect;
	//public GameObject shieldEffect2;
	public GameObject gun;
	public bool pausepressed = false;

	public Texture2D GetHatImage;

	public AudioClip GetCoins;

	Animator anim;

	int numHats = 1;
	bool[] hatPlaces = {true, false, false, false};
	Hat[] hats;

	float timeInShield = 0;
	float timeInMagnet = 0;
	float SplittedTime = 0;

	bool isShield = true;
	bool isMagnet = true; // to turn off particle effects at the very beginning
	bool splittedEnabled = true;
	bool isJumping = false;
	bool canPuffInAir = false;

	GameObject[] players;

	float hatDrop = 1f;
	bool isHatDrop = false;

	Vector3 speed = new Vector3();
	public CharacterController controller;

	public KnightManager knightManager;


	float doubleJump = 3.0F;
	//bool canDJump = false;
	bool get_coin = false;
	int addedPoints;

	int ammo = 5;
	float floatScore = 0;
	bool inHill = false;
	bool isAlive = true;

	Vector3 screenPosition;

	GUIStyle livesFont;
	float fadeTime = 2f;
	Color color = Color.white;
	public Color playerColor;

	bool knightArea = false;

	// Use this for initialization
	void Start () {
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
		anim = GetComponent<Animator>();

		hats = GetComponentsInChildren<Hat>();
	}
	
	// Update is called once per frame
	void Update () {

		if (pausepressed == false) {
			if (Input.GetKeyDown ("z") || Input.GetButtonDown ("Fire4")) {
				pausepressed = true;
				//resumepressed = false;
			}
		} else {
			if (Input.GetKeyDown ("z") || Input.GetButtonDown ("Fire4")) {
				pausepressed = false;
				//resumepressed = true;
			}
		}

		if (transform.position.y < 6 && !knightArea)
		{
			knightArea = true;
			knightManager.addPlayer();
			Debug.Log(transform.position.y);
		}
		else if (transform.position.y >= 6 && knightArea)
		{
			knightArea = false;
			knightManager.losePlayer();
		}

		if (isHatDrop)
		{
			hatDrop -= Time.deltaTime;
			if (hatDrop <= 0)
				DropHats();
		}

		if (PlayerScoreSlider.value < GameManager.scores [playerNumber - 1])
			PlayerScoreSlider.value += (GameManager.scores [playerNumber - 1]-PlayerScoreSlider.value)/10f;

		if (timeInShield > 0)
			timeInShield -= Time.deltaTime;
		else
			if(isShield) turnOffShield();

		if (timeInMagnet > 0)
			timeInMagnet -= Time.deltaTime;
		else
			if(isMagnet)turnOffMagnet();

		if (SplittedTime > 0)
			SplittedTime -= Time.deltaTime;
		else
			splittedEnabled = false;

		if (splittedEnabled == true) {
			gun.GetComponent<Aim> ().splitted = true;		
		} else {
			gun.GetComponent<Aim> ().splitted = false;
		}
		if (fadeTime > 0)
			fadeTime -= Time.deltaTime;

		if (transform.position.z != 0)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;

		if (Input.GetAxis (playerNumber + "Horizontal") < -0.1 || Input.GetAxis (playerNumber + "Horizontal") > 0.1)
			anim.SetBool("IsWalking", true);
		else
			anim.SetBool("IsWalking", false);

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

		// If player falls through bottom of screen, teleport them to top
		if (transform.position.y <= -10)
			transform.position = new Vector3 (transform.position.x, 26);

		// If player moves through side of screen, teleport them to other side
		if (transform.position.x < -32.5f)
			transform.position = new Vector3 (32.5f, transform.position.y);
		if (transform.position.x > 32.5f)
			transform.position = new Vector3 (-32.5f, transform.position.y);

		if (GetComponent<CharacterController>().velocity.y > 0.1 || GetComponent<CharacterController>().velocity.y < -0.1)
			anim.SetBool("IsJumping", true);
		else
			anim.SetBool("IsJumping", false);

		if (Input.GetButton (playerNumber + "Fire1"))
			anim.SetTrigger("Attack");

		if(Input.GetButtonDown(playerNumber+"Jump")){
			if(isJumping){
				if(canPuffInAir){
					puffEffect();
					//doubleJumpEffect();
					canPuffInAir = false;
				}
			}else{
				puffEffect();
			}
		}
	}

	void OnGUI () {
		//color.a = 1;
		//GUI.color = color;

//		playerColor.a = 1;
//		GUI.color = playerColor;

//		if (inHill)
//		GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),(GameManager.scores [playerNumber - 1]+""));

		//playerColor.a = fadeTime;
		//GUI.color = playerColor;
		Color color = GUI.color;
		Color old = color;
		color.a = fadeTime;
		GUI.color = color;
		if (numHats < 1)
			//GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),("Get a Hat!"), livesFont);
			GetHatImage = (Texture2D)Resources.Load(SetGetHatPath(playerNumber));
			GUI.DrawTexture(new Rect(screenPosition.x-30, screenPosition.y-60, 60, 40), GetHatImage, ScaleMode.StretchToFill);
		//if (manager.gameType == "Deathmatch" || manager.gameType == "Team Deathmatch")
		//{
		//	if (lives > 1)

		//	else
		//		GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),(lives + " Life!"), livesFont);
		//}
		//else
		//	GUI.Label(new Rect(screenPosition.x-15, screenPosition.y-40, 100, 100),("Infinite Lives!"), livesFont);
	}

	string SetGetHatPath(int playerNum){
		string path = "ScorePoints/";
		if(playerNum == 1)
			path += "green/g_hat";
		if(playerNum == 2)
			path += "blue/b_hat";
		if(playerNum == 3)
			path += "red/r_hat";
		if(playerNum == 4)
			path += "yellow/y_hat";
		return path;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "HatTrigger")
		{
			if (other.GetComponentInParent<Hat>().IgnoreTrigger() == false)
				other.GetComponentInParent<Hat>().NewOwner(gameObject, playerNumber);
		}
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
			if (other.GetComponentInParent<Hat>().IgnoreTrigger() == false)
				other.GetComponentInParent<Hat>().NewOwner(gameObject, playerNumber);
		}

		if (other.gameObject.tag == "Money")
		{
			if (numHats > 0)
			{
				GetComponent<AudioSource>().PlayOneShot(GetCoins);
				addedPoints = (int) Mathf.Pow(2f, numHats-1);
				GameManager.AddScore(playerNumber, addedPoints);
			}
			else
				fadeTime = 2f;
		}

		if (other.gameObject.tag == "Magnet")
		{
			turnOnMagnet(5f);
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "Shield")
		{
			turnOnShield(5f);
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "SplittedBarrel")
		{

			SplittedTime = 5f;
			splittedEnabled = true;
			//Debug.Log (splittedEnabled);
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag == "Floor"){
			if(other.transform.position.y < transform.position.y){ // if dropping onto a floor
				puffEffect();
				isJumping = false;
			}
		}
	}

	void puffEffect()
	{
		ParticleSystem puff = new ParticleSystem();
		puff = Instantiate(Resources.Load("SmokePuff"), new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y, transform.position.z), Quaternion.Euler(270, 0, 0)) as ParticleSystem;
		//Destroy(puff, 0);
	}
	void doubleJumpEffect()
	{
		ParticleSystem puff = new ParticleSystem();
		puff = Instantiate(Resources.Load("doubleJump"), new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y, transform.position.z), Quaternion.Euler(270, 0, 0)) as ParticleSystem;
		//Destroy(puff, 0);
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Floor")
		{
			//canDJump = true;	
			isJumping = true;
			canPuffInAir = true;
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
					//Debug.Log("P" + playerNumber + " died! \n Lives: " + lives);
					int spawnNumber = Random.Range (0, spawns.Length);
					spawn = spawns [spawnNumber];
					//timeInShield = shieldTime;
					//turnOnShield();
					transform.position = spawn.transform.position;
				}
				else
				{
					//Debug.Log("P" + playerNumber + " eliminated!");
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
				//turnOnShield();
				transform.position = spawn.transform.position;
			}
		}
	}
	public void turnOnShield(float dur){
		isShield = true;
		timeInShield = dur;
		shieldEffect.Play();
	}
	public void turnOffShield(){
		isShield = false;
		shieldEffect.Stop();
	}
	public void turnOnMagnet(float dur){
		isMagnet = true;
		timeInMagnet = dur;
		magnetParticleEffect.Play();
		MagnetEffect.GetComponent<Collider>().enabled = true;
	}
	public void turnOffMagnet(){
		isMagnet = false;
		magnetParticleEffect.Stop();
		MagnetEffect.GetComponent<Collider>().enabled = false;
	}
	public bool IsShield() {
		return isShield;
	}

	public void LoseHat() {
		if (numHats > 0)
		{
			numHats--;
			hats = GetComponentsInChildren<Hat>();

			bool pass = false;

			for (int i=0; i < 4; i++)
				if (!hatPlaces[i])
				{
					hatPlaces[i-1] = false;
					//Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
					pass = true;
				}
			if (pass == false)
			{
				hatPlaces[3] = false;
				//Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
			}
			isHatDrop = true;
			hatDrop = 1f;
		}
	}

	void DropHats() {
		isHatDrop = false;
		hatDrop = 1f;
		for (int j=0; j<4; j++)
		{
			//Debug.Log(hatPlaces[j]);
			if (hatPlaces[j])
			{
				bool isEmpty = true;
				while (isEmpty == true)
				{
					foreach(Hat hat in hats)
					{
						if (hat.GetHatNumber() - 1 == j)
						{
							isEmpty = false;
						}
					}
					if (isEmpty == true)
					{
						foreach(Hat hat in hats)
						{
							if (hat.GetHatNumber() - 1 > j)
								hat.SetHatNumber(hat.GetHatNumber()-1);
						}
					}
					hats = GetComponentsInChildren<Hat>();
				}
			}
		}
	}

	public void AddHat() {
		numHats++;
		for (int i=0; i < 4; i++)
			if (!hatPlaces[i])
			{
				hatPlaces[i] = true;
				//Debug.Log(hatPlaces[0]+","+hatPlaces[1]+","+hatPlaces[2]+","+hatPlaces[3]);
				break;
			}
	}

	public int NumHats() {
		return numHats;
	}

	public bool[] HatPlaces() {
		return hatPlaces;
	}
	
}
