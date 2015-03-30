using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

	// Use this for initialization

	public float fireratio = 1.0f;

	public float movementSpeed = 0.1f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fireratio < 0) {
			GameObject b = new GameObject ();
			b = Instantiate (Resources.Load ("bulletGreen2"), transform.position, new Quaternion (180, 0, 0, 0)) as GameObject;
			fireratio = 1.0f;
		} else {
			fireratio -= Time.deltaTime;
		}
		transform.position = new Vector3 (transform.position.x + movementSpeed*Time.deltaTime, transform.position.y, transform.position.z);
		Destroy(this.gameObject, 20.0f);
	}
}
