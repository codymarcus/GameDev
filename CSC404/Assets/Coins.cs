using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	// Use this for initialization
	Vector3 screenPosition;

	float fadeTime = 0f;
	float waitTime = 2f;
	float rotationsPerMinute = 25.0f;
	bool isHit = false;
	int addedPoints;
	GUIStyle livesFont;
	public Texture2D ScoreImage;
	public AudioClip collectSound;

	AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();
		//ScoreImage = (Texture2D)Resources.Load("ScorePoints/red/r_100");
	}
	
	// Update is called once per frame
	void Update () {
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;

		if (fadeTime > 0)
			fadeTime -= Time.deltaTime;
		if (isHit)
			waitTime -= Time.deltaTime;
		if (waitTime <= 0)
			Destroy(this.gameObject);

		transform.Rotate(6.0f*rotationsPerMinute*Time.deltaTime,0,0);
	}

	void OnGUI () {
		Color color = GUI.color;
		Color old = color;
		color.a = fadeTime;
		GUI.color = color;
		if (ScoreImage != null)
			GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y-40, 60, 30), ScoreImage, ScaleMode.StretchToFill);
		GUI.color = old;
	}

	void coinEffect()
	{
		ParticleSystem coinEffect = new ParticleSystem();
		coinEffect = Instantiate(Resources.Load("CoinEffect"), transform.position, Quaternion.Euler(0, 0, 0)) as ParticleSystem;
		Destroy(coinEffect, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			if (other.gameObject.GetComponent<PlayerController> ().NumHats () > 0) {
					source.PlayOneShot(collectSound, 1f);
					coinEffect();
					int playerNum = other.gameObject.GetComponent<PlayerController>().playerNumber;
					int playerHat = other.gameObject.GetComponent<PlayerController>().NumHats();
					SetImage(playerNum, playerHat);
					GameManager.numCoins--;
					addedPoints = (int)Mathf.Pow (2f, other.gameObject.GetComponent<PlayerController> ().NumHats () - 1);
					fadeTime = 2f;
					gameObject.GetComponent<Renderer> ().enabled = false;
					Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer> ();
					foreach (Renderer r in renderers) {
							r.enabled = false;
					}
					gameObject.GetComponent<Collider> ().enabled = false;
					isHit = true;
			}
		}
	}

	void SetImage (int playerNum, int playerHat){
		string path = "ScorePoints/";
		if(playerNum == 1)
			path += "green/g_";
		if(playerNum == 2)
			path += "blue/b_";
		if(playerNum == 3)
			path += "red/r_";
		if(playerNum == 4)
			path += "yellow/y_";
		if(playerHat == 1)
			path += "100";
		if(playerHat == 2)
			path += "200";
		if(playerHat == 3)
			path += "400";
		if(playerHat == 4)
			path += "800";
		ScoreImage = (Texture2D)Resources.Load(path);
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Magneting"){
			float step = 5.0f * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, other.gameObject.transform.position,step);
		}
	}


}
