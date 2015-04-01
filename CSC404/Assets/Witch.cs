using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

	// Use this for initialization

	public float fireratio = 1.0f;
	 float initial_y;
	 float initial_z;
	 float amplitude = 1.0f;
	 float speed = 1.0f;
	 float amplitude2 = 4.0f;
	 float speed2 = 5.0f;
	 float movementSpeed = 3.0f;

	bool isHit = false;

	void Start () {
		initial_y = transform.position.y;
		//transform.rotation = Quaternion.Euler(-43.1f,180,0);
		initial_z = transform.rotation.z;
		Destroy(this.gameObject, 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
		{
			transform.Rotate(new Vector3 (0, 0, -15));
		}
		float new_z = initial_z + amplitude2 * Mathf.Sin (speed2 * Time.time);
		float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = new Vector3(transform.position.x + movementSpeed*Time.deltaTime, new_y, transform.position.z);
		transform.Rotate (new_z, 0, 0);
		//print (transform.rotation.eulerAngles);
		//transform.position = new Vector3 (transform.position.x + movementSpeed*Time.deltaTime, transform.position.y, transform.position.z);
		//Destroy(this.gameObject, 20.0f);
	}

	public void Hit () {
		isHit = true;
	}
	
}
