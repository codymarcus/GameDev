using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

	// Use this for initialization

	public float fireratio = 1.0f;
	public float initial_y;
	public float amplitude = 1.0f;
	public float speed = 3.0f;
	public float movementSpeed = 3.0f;

	bool isHit = false;

	void Start () {
		initial_y = transform.position.y;
		Destroy(this.gameObject, 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
		{
			transform.Rotate(new Vector3 (0, 0, -15));
		}
		else
		{
			if (fireratio < 0) {
				GameObject a = new GameObject ();
				a = Instantiate (Resources.Load ("bulletWitch"), transform.position, Quaternion.Euler(180, 90, 0)) as GameObject;
				GameObject b = new GameObject ();
				b = Instantiate (Resources.Load ("bulletWitch"), transform.position, Quaternion.Euler(225, 90, 0)) as GameObject;
				GameObject c = new GameObject ();
				c = Instantiate (Resources.Load ("bulletWitch"), transform.position, Quaternion.Euler(135, 90, 0)) as GameObject;
				fireratio = 1.0f;
			} else {
				fireratio -= Time.deltaTime;
			}
		}
		float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = new Vector3(transform.position.x + movementSpeed*Time.deltaTime, new_y, transform.position.z);

		//transform.position = new Vector3 (transform.position.x + movementSpeed*Time.deltaTime, transform.position.y, transform.position.z);
		//Destroy(this.gameObject, 20.0f);
	}

	public void Hit () {
		isHit = true;
	}
	
}
