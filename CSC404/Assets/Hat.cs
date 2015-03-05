using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour {

	GameObject[] floors;

	// Use this for initialization
	void Start () {
		floors = GameObject.FindGameObjectsWithTag ("Floor");
		foreach (GameObject floor in floors)
			Physics.IgnoreCollision(GetComponent<Collider>(), floor.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
