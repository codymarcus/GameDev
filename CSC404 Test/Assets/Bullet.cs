using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float newX = transform.position.x + velocity * Time.deltaTime;
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor")
		{
			Destroy (gameObject);
		}
	}
}
