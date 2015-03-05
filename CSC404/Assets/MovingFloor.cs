using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	public float velocity = 5f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().velocity = new Vector3 (velocity, 0, 0);

		if (transform.position.x >= 42)
			transform.position = new Vector3(-42, transform.position.y);
	}
}
