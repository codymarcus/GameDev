using UnityEngine;
using System.Collections;

public class daynight : MonoBehaviour {

	float RotationSpeed = 6.0f; //6
	//float initial_y;
	//float amplitude = 10.0f;
	//float speed = 0.0333f;
	public GameObject sun;
	public GameObject moon;

	// Use this for initialization
	void Start () {
		//initial_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate (0, 0, 1);
		transform.Rotate (Vector3.forward * (RotationSpeed * Time.deltaTime));
		sun.transform.Rotate (Vector3.back * (RotationSpeed * Time.deltaTime));
		moon.transform.Rotate (Vector3.back * (RotationSpeed * Time.deltaTime));
		//float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		//transform.position = new Vector3(transform.position.x, new_y, transform.position.z);

	}
}
