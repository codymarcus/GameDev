using UnityEngine;
using System.Collections;

public class ExplodingFloor : MonoBehaviour {

	public Explosion explosion;

	Vector3 pos;
	Quaternion angle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Explode() {
		pos = transform.position;
		angle = angle;

		Destroy (collider);
		GameObject e = Instantiate (explosion, transform.position , transform.rotation) as GameObject;
		Destroy (gameObject);
	}
}
