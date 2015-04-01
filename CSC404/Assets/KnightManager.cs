using UnityEngine;
using System.Collections;

public class KnightManager : MonoBehaviour {

	public GameObject knights;
	public double spawnTime = 4f;
	double timeLeft;
	int numPlayers = 0;
	bool runTime = false;
	bool leftSide = true;

	// Use this for initialization
	void Start () {
		timeLeft = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (runTime)
		{
			timeLeft-=Time.deltaTime;
			//Debug.Log (timeLeft);
			if (timeLeft <= 0)
			{
				GameObject k;
				if (leftSide)
				{
					k = Instantiate(knights, new Vector3(transform.position.x-transform.localScale.x/2, transform.position.y),
			     	    	       new Quaternion (0, 0, 0, 0)) as GameObject;
					leftSide = false;
				}
				else
				{
					k = Instantiate(knights, new Vector3(transform.position.x+transform.localScale.x/2, transform.position.y),
					                new Quaternion (0, 180, 0, 0)) as GameObject;
					leftSide = true;
				}
				Destroy ( k , 4f);
				timeLeft = spawnTime;
			}
		}
	}

	public void addPlayer () {
		numPlayers++;
		if (numPlayers > 2)
			runTime = true;
	}

	public void losePlayer () {
		numPlayers--;
		if (numPlayers < 3)
		{
			runTime = false;
			timeLeft = spawnTime;
		}
	}
}
