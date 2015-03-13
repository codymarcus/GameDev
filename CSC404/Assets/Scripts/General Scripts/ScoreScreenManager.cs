using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {

	public static int[] matchScores = {0, 0, 0, 0};
	public float waitTime;
	public static int[] rank = {0,0,0,0};

	// Use this for initialization
	void Start () {
		matchScores = GameManager.scores;
	}
	
	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime <= 0){
			Application.LoadLevel("Scene0");
		}
	}
}
