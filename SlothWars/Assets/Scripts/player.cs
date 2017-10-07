using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Animator anim;
	public Rigidbody rbody;

	private float inputH;
	private float inputV;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("1")) 
		{
			anim.Play ("walk",-1,0f);
		}

		if (Input.GetKey (KeyCode.Space)) 
		{
			anim.SetBool ("jump", true);
		} 
		else 
		{
			anim.SetBool ("jump", false);
		}

		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");

		anim.SetFloat ("inputH", inputH);
		anim.SetFloat ("inputV", inputV);

		float moveX = inputH * 20f * Time.deltaTime;
		float moveZ = inputV * 50f * Time.deltaTime;

		/*if (moveZ <= 0f) 
		{
			moveX = 0f;
		}*/

		rbody.velocity = new Vector3 (moveX, 0f, moveZ);
		//rbody.AddForce(new Vector3(moveX, 0f, moveZ), ForceMode.VelocityChange);
	}
}
