using UnityEngine;
using System.Collections;

public class SpikeWalls : MonoBehaviour {
	public double respawnTime = 5;
	public GameObject spike;
	GameObject s = new GameObject ();

	double timeToRespawn;
	bool spikesOut = true;

	// Use this for initialization
	void Start () {
		timeToRespawn = respawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespawn -= Time.deltaTime;
		if (timeToRespawn <= 0) {
			if (spikesOut == true)
			{
				Destroy (s, 0f);
				spikesOut = false;
			}
			else
			{
				s = Instantiate (spike, this.transform.position, this.transform.rotation) as GameObject;
				s.transform.parent = transform;
				spikesOut = true;
			}
			timeToRespawn = respawnTime;
		}
	}
}
