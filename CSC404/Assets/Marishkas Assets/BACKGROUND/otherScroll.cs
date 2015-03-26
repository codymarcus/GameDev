using UnityEngine;
using System.Collections;

public class otherScroll : MonoBehaviour
{
		public float speed = 0;
		public static otherScroll current;
		private Material[] materials;
		private Renderer rend;
	
		// Use this for initialization
		void Start ()
		{
				rend = GetComponent<Renderer> ();
				rend.enabled = true;
				current = this;
		}
	
		// Update is called once per frame
		void Update ()
		{
				Vector2 offset = new Vector2 (Time.time * speed, 0);
				rend.material.mainTextureOffset = offset;
		}
}