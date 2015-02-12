using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform target;
	public float speed;
	Vector3 newPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		newPos = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
		transform.position = new Vector3(newPos.x, transform.position.y, 0);
	}
}
