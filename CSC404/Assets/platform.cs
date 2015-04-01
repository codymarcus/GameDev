using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour {
	
	public float initial_y;
	public float amplitude = 0.0f;
	public float speed = 0.0f;
	
	// Use this for initialization
	void Start () {
		initial_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position.y = initial_y + amplitude*Mathf.Sin(speed*Time.time);
		float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
	}
}
