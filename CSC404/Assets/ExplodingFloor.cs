using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExplodingFloor : MonoBehaviour {

	public Explosion explosion;
	
	float waitTime = 5f;
	float respawnTime = 10f;
	bool isHit = false;
	bool isExplode = false;

	Vector3 screenPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;

		if (isHit)
			waitTime -= Time.deltaTime;
		if (waitTime <= 0)
			Explode();
		if (isExplode)
			respawnTime -= Time.deltaTime;
		if (respawnTime <= 0) {
			gameObject.renderer.enabled = true;
			gameObject.collider.enabled = true;
			isExplode = false;
			respawnTime = 10f;
			isHit = false;
		}
	}

	void Explode() {
		gameObject.collider.enabled = false;
		GameObject e = Instantiate (explosion, transform.position , transform.rotation) as GameObject;
		waitTime = 5f;
		isHit = false;
		//Destroy (gameObject);
		gameObject.renderer.enabled = false;
		isExplode = true;
	}

	public void Hit() {
		isHit = true;
	}

	void OnGUI() {
		GUI.color = Color.black;
		if (gameObject.renderer.enabled)
			GUI.Label(new Rect(screenPosition.x-10, screenPosition.y-5, 100, 100),(System.Math.Round(waitTime, 0)+""));
	}
}
