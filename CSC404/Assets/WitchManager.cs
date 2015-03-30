using UnityEngine;
using System.Collections;

public class WitchManager : MonoBehaviour {

	// Use this for initialization

	public float spawnratio = 20.0f;

	public GameObject witch;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject w;
		if (spawnratio < 0) {
			w = Instantiate (witch, new Vector3 (transform.position.x, transform.position.y),
                new Quaternion (0, 180, 0, 0)) as GameObject;
			spawnratio = 20.0f;
		} else {
			spawnratio -= Time.deltaTime;
		}
	}
}
