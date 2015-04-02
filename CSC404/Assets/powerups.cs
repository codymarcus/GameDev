using UnityEngine;
using System.Collections;

public class powerups : MonoBehaviour {

	public float initial_y;
	public float amplitude = 0.4f;
	public float speed = 3.5f;
	public AudioClip soundEffect;

	// Use this for initialization
	void Start () {
		initial_y = transform.position.y;
		powerupEffect();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position.y = initial_y + amplitude*Mathf.Sin(speed*Time.time);
		float new_y = initial_y + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
	}

	void powerupEffect()
	{
		ParticleSystem coinEffect = new ParticleSystem();
		//GetComponent<AudioSource>().PlayOneShot(soundEffect);
		coinEffect = Instantiate(Resources.Load("powerupEffect"), transform.position, Quaternion.Euler(0, 0, 0)) as ParticleSystem;
		//Destroy(coinEffect, 0);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<AudioSource>().PlayOneShot(soundEffect);
			//print (soundEffect);
			powerupEffect();
		}
	}
}
