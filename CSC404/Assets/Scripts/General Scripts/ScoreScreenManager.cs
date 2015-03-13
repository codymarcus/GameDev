using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {

	public static int[] matchScores = {0, 0, 0, 0};
	public float waitTime;
	string displayText;
	

	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if (waitTime <= 0){
			//Debug.Log(OnClickEvent.matchType);
			/*
			if (OnClickEvent.matchType == null)
			{
				Application.LoadLevel ("RoundScreen");
			}
			else
			{	
				Application.LoadLevel ("GameMenu");
			}
			*/
			Application.LoadLevel("Scene0");
		}
	}
}
