using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	float lifeSpan = 0.3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (0.9f, 0.9f, 0.9f);
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0)
			Destroy (gameObject);
	}
}
