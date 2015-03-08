using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	// Use this for initialization
	Vector3 screenPosition;

	Color color = Color.white;

	float fadeTime = 0f;
	float waitTime = 2f;
	bool isHit = false;
	int addedPoints;
	GUIStyle livesFont;

	void Start () {
		livesFont = new GUIStyle();
		livesFont.fontSize = 15;
		livesFont.fontStyle = FontStyle.Bold;
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
	}

	void OnGUI () {
		color.a = fadeTime;
		GUI.color = color;
		GUI.Label(new Rect(screenPosition.x, screenPosition.y-40, 100, 100),("+" + addedPoints), livesFont);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			addedPoints = (int) Mathf.Pow(2f, other.gameObject.GetComponent<PlayerController>().NumHats()-1);
			fadeTime = 2f;
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
			isHit = true;
		}
	}



}
