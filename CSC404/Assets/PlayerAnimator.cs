using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {
	private Animator animator;
	private float speed;
	private bool jumping;
	[SerializeField]
	private int pNum;
	private Vector3 face;
	private string lastDir = "right";
	private bool canLeft = true;
	private bool canRight = false;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		face = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		speed = Input.GetAxis(pNum+"Horizontal");
		if(Input.GetButtonDown(pNum+"Jump")){
			jumping = true;
		}
		if(speed<0){
			lastDir = "right";

		}
		if(speed>0){
			lastDir = "left";
		}
		if(lastDir == "left" && canRight){
			face = new Vector3(0,180,0);
			this.transform.Rotate(face);
			canRight = false;
			canLeft = true;
		}
		if(lastDir == "right" && canLeft){
			face = new Vector3(0,-180,0);
			this.transform.Rotate(face);
			canLeft = false;
			canRight = true;
		}

	
		if(jumping){
			animator.SetBool("jumping", true);
			jumping = false;
		}if(animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Jump")){
			animator.SetBool("jumping", false);
		}

		if(speed!=0f){
			Debug.Log("Test");
			animator.SetBool("running", true);
		}
		if(speed==0f){
			Debug.Log("Test");
			animator.SetBool("running", false);
		}
		Debug.Log(speed);
	}

}
