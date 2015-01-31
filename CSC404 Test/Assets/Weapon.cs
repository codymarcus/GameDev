using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("RightH");
		float y = Input.GetAxis("RightV");
		transform.Rotate(Time.deltaTime, 0, 0);
	}
}
