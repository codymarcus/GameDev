using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

	// Use this for initialization

	public float fireratio = 1.0f;
	 float initial_y;
	 float initial_z;
	 float amplitude = 1.0f;
	 float speed = 1.0f;
	 float amplitude2 = 2.0f;
	 float speed2 = 5.0f;
	 float movementSpeed = 3.0f;
	 float recoveryTime = 5f;
	 float timeToRecover;
	 public AudioClip groan;

	bool isHit = false;

	void Start () {
		initial_y = transform.position.y;
		//transform.rotation = Quaternion.Euler(-43,180,0);
		initial_z = transform.rotation.z;
		Destroy(this.gameObject, 20.0f);
		timeToRecover = recoveryTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit && timeToRecover > 0)
		{
			transform.Rotate(new Vector3 (0, 0, -15));
			timeToRecover -= Time.deltaTime;
		}
		else
		{
			if (fireratio < 0) {
				GameObject a = new GameObject ();
				a = Instantiate (Resources.Load ("BulletWitch"), transform.position, Quaternion.Euler(180, 90, 0)) as GameObject;
				GameObject b = new GameObject ();
				b = Instantiate (Resources.Load ("BulletWitch"), transform.position, Quaternion.Euler(225, 90, 0)) as GameObject;
				GameObject c = new GameObject ();
				c = Instantiate (Resources.Load ("BulletWitch"), transform.position, Quaternion.Euler(135, 90, 0)) as GameObject;
				fireratio = 1.0f;
			} else
				fireratio -= Time.deltaTime;
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
		timeToRecover = recoveryTime;
		GetComponent<AudioSource>().PlayOneShot(groan);
		isHit = true;
	}
	
}
