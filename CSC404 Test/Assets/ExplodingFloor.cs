using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExplodingFloor : MonoBehaviour {

	public Explosion explosion;
	
	float waitTime = 2f;
	bool isHit = false;

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
	}

	void Explode() {

		Destroy (collider);
		GameObject e = Instantiate (explosion, transform.position , transform.rotation) as GameObject;
		Destroy (gameObject);
	}

	public void Hit() {
		isHit = true;
	}

	void OnGUI() {
		Debug.Log (waitTime);
		GUI.color = Color.black;
		GUI.Label(new Rect(screenPosition.x-10, screenPosition.y-5, 100, 100),(System.Math.Round(waitTime, 0)+""));
	}
}
