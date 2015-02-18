using UnityEngine;
using System.Collections;

public class SpikeWalls : MonoBehaviour {
	public double timeToRespwan = 5;
	public GameObject spike;
	public GameObject PlatformOwner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeToRespwan -= Time.deltaTime;
		if (timeToRespwan <= 0) {
			GameObject b = new GameObject ();
			b = Instantiate (spike, this.transform.position, spike.transform.rotation) as GameObject;
			b.GetComponent<SpikeDeath>().platform = PlatformOwner;
			timeToRespwan = 10;
		}
	}
}
