using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float velocity;
	public int owner;
	public Vector3 speed;
	public GameObject player;
	public Vector2 rotation = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
		speed = Vector3.up * velocity * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		//rigidbody.AddForce (Vector3(1, 0, 0));
		transform.Translate(Vector3.right * velocity * Time.deltaTime);
		rotation = new Vector2 (Input.GetAxis (owner + "RightH"), Input.GetAxis (owner + "RightV"));
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Floor") {
			Destroy (gameObject);
		}
	}
}
