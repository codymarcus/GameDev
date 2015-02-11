using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;

	// Use this for initialization
	void Start () {
		speed = Vector3.up * velocity * Time.deltaTime;
		speed = transform.rotation * speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * velocity * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor") {
			Destroy (gameObject);
		}
	}
	
}
