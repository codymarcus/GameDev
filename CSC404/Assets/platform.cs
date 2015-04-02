using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour {
	
	public float initial_y;
	float amplitude = 0.75f;
	float speed = 0.5f;
	
	// Use this for initialization
	void Start () {
		//speed = Random.value + 0.5;
		if (Random.value > 0.5) {
			amplitude = -1*amplitude;
		} else {
			//amplitude = 0.75f;
		}
		speed += Random.value;

		initial_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position.y = initial_y + amplitude*Mathf.Sin(speed*Time.time);
		float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
	}
}
