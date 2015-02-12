using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;
	float destroyTime = 0.0001F;
	bool isDestroy = false;

	// Use this for initialization
	void Start () {
		rigidbody.velocity = transform.up * velocity; // * Time.deltaTime;
		speed = rigidbody.velocity;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDestroy == true)
		{
			destroyTime -= Time.deltaTime;
			if (destroyTime <= 0)
				Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor") {
			other.gameObject.GetComponent<CrazyFloor>().Hit();
			isDestroy = true;
		}

		if (other.gameObject.tag == "RightFloor" || other.gameObject.tag == "DownFloor") {
			
			Destroy (gameObject);
			other.gameObject.GetComponent<MovingFloor>().Hit();
		}

		if (other.gameObject.tag == "Player") {
			
			Destroy (gameObject);
		}
	}
	
}
