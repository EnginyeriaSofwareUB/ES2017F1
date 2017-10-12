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
		if (Input.GetKey (KeyCode.Space)) 
		{
			anim.SetBool ("jump", true);
		} 
		else 
		{
			anim.SetBool ("jump", false);
		}

		inputH = Input.GetAxis ("Horizontal");

		anim.SetFloat ("inputH", inputH);

		float moveX = inputH * 50f * Time.deltaTime;

		if (inputH > 0) 
		{
			transform.eulerAngles = new Vector3(0, 90, 0);
			Debug.Log (transform.rotation);
			rbody.velocity = new Vector3 (moveX, 0f, 0f);
		}
		else if (inputH < 0)
		{
			transform.eulerAngles = new Vector3(0, -90, 0);
			Debug.Log (transform.rotation);
			rbody.velocity = new Vector3 (moveX, 0f, 0f);
		}
	}
}
