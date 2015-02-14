using UnityEngine;
using System.Collections;

public class HeavyFloor : MonoBehaviour {

	public float dropSpeed = 3f;
	bool weighedDown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (weighedDown == true)
			rigidbody.velocity = Vector3.down * dropSpeed;
		else
			rigidbody.velocity = Vector3.zero;
	}

	public void WeighDown() {
		weighedDown = true;
	}

	public void NotWeighDown() {
		weighedDown = false;
	}

}
