using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour {

	public GameObject owner;
	public int ownerNumber;
	GameObject[] players;
	GameObject[] floors;
	bool isHit = false;
	float hitTime = 0.5f;
	float timeLeft;

	int hatNumber = 1;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
			Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
		if (isHit)
			timeLeft -= Time.deltaTime;
		else
		{
			transform.rotation = new Quaternion(0,0,0,0);
//			for (int i=0; i<4; i++)
//				if (!owner.GetComponent<PlayerController>().HatPlaces()[i] && i+1 == hatNumber)
//					hatNumber--;
			transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + (hatNumber*1.2f));
		}
	}

	public void Hit() {
		timeLeft = hitTime;
		isHit = true;
		transform.parent = null;
		owner.GetComponent<PlayerController> ().LoseHat ();
		foreach (GameObject player in players)
			Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
	}

	public void NewOwner (GameObject newOwner, int newNumber) {
		if (isHit)
		{
			if (owner != newOwner || timeLeft <= 0)
			{

				Debug.Log(owner + "+" + newOwner);
				owner = newOwner;
				ownerNumber = newNumber;
				owner.GetComponent<PlayerController>().AddHat();
				hatNumber = owner.GetComponent<PlayerController>().NumHats();
				GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
				GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, 0);
				transform.rotation = new Quaternion(0,0,0,0);
				transform.position = new Vector3(owner.transform.position.x, owner.transform.position.y + (hatNumber*1.2f));
				transform.parent = owner.transform;
				isHit = false;

				foreach (GameObject player in players)
					Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
				//Physics.IgnoreCollision(GetComponent<Collider>(), owner.GetComponent<Collider>());

			}
		}
	}

	public bool IsHit() {
		return isHit;
	}
}
