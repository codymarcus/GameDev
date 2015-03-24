using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScore : MonoBehaviour {

	public int currentplace = 0;
	public Text score;

	// Use this for initialization
	void Start () {
		Debug.Log (Showplace.scores);
		score.text = Showplace.scores[currentplace].ToString();
	}
}
