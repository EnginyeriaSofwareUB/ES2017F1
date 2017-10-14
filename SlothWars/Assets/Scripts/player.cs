﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public Animator anim;
	public Rigidbody rbody;

	private float inputH;
	private float inputV;

	private bool move = false;
    Vector3 newPosition = new Vector3(0f, 0f, 0f);

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Movement_Interpretation ();
		Movement_Correction ();
	}

	void Movement_Interpretation()
	{
		Jump ();
		Left_Or_Right ();
	}

	// Method for checking spacebar press action to activate jump animation
	void Jump()
	{
		if (Input.GetKey (KeyCode.Space)) 
		{
			anim.SetBool ("jump", true);
		}
		else 
		{
			anim.SetBool ("jump", false);
		}
	}

	// Checking movement of the player using arrow keys or A and D keys
	void Left_Or_Right()
	{
        if (!move)
        {
            inputH = Input.GetAxis ("Horizontal");
            anim.SetFloat ("inputH", inputH);

            if (inputH > 0.1)
            {
                move = true;
                inputH = 1;
                newPosition = transform.position + new Vector3(1f, 0f, 0f);
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            if (inputH < -0.1)
            {
                move = true;
                inputH = -1;
                newPosition = transform.position + new Vector3(-1f, 0f, 0f);
                transform.eulerAngles = new Vector3(0, -90, 0);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position,newPosition) > Vector3.kEpsilon)
            {
                Debug.Log(newPosition);
                float step = 1 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            }
            else
            {
                inputH = 0;
                move = false;
            }
        }
	}

	// Method for limiting available movement space for the player 
	void Movement_Correction()
	{
		if (transform.position.x > 9.5) 
		{
            move = false;
			transform.position = new Vector3 (9.5f, transform.position.y, transform.position.z);
		}
		if (transform.position.x < -9.5) 
		{
            move = false;
			transform.position = new Vector3 (-9.5f, transform.position.y, transform.position.z);
		}
	}
}