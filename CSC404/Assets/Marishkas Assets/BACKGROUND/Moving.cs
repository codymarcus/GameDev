using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public Transform movingSun;
	public Transform position1;
	public Transform position2;
	public Vector3 newPosition;
	public string currentState;
	public float smooth;
	public float resetTime;
	// Use this for initialization
	void Start () {
		ChangeTarget ();
	
	}
	
	// Update is called once per frame
	void fixedUpdate () {

		movingSun.position = Vector3.Lerp (movingSun.position, newPosition, smooth = Time.deltaTime);
	
	}

	void ChangeTarget () {
		if(currentState == "Moving To Position 1") {
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		}
		else if(currentState == "Moving To Position 2") {
			currentState = "Moving to Position 1";
			newPosition = position1.position;
		}
		else if(currentState == "") {
			currentState = "Moving to Position 2";
			newPosition = position2.position;
		}

		Invoke ("ChangeTarget", resetTime);
	}
}
