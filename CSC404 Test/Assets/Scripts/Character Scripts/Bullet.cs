using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 start;
	public float live_time;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * velocity * Time.deltaTime);
		live_time += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor") {
			Destroy (gameObject);
		}
	}
}
