using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int[] scores = {0, 0, 0, 0};

	Text scoreText;

	// Use this for initialization
	void Awake () {
		scoreText = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "P1: " + scores[0] + "\n" + "P2: " + scores[1] + "\n" + "P3: " + scores[2]
		+ "\n" + "P4: " + scores[3];
	}
}
