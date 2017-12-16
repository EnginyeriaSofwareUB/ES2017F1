using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
	private Vector3 speedVector, position;
	private float sizeBlock = 1.0f;
	private float speed = 1f;
	private bool moving;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(moving){
			if( (speedVector*Time.deltaTime).magnitude < (position - transform.parent.position).magnitude ){
				transform.parent.position = transform.parent.position + speedVector*Time.deltaTime;
			} else {
				moving = false;
				transform.parent.position = position;
				GameObject.Find("Main Camera").GetComponent<GameController2>().NotifyActionEnded();
			}
		}
	}

	public void MoveTo(int x, int y){
		moving = true;
		position = transform.parent.position + new Vector3(x * sizeBlock, y * sizeBlock, 0f);
		speedVector =  (position - transform.parent.position)/speed;
		speedVector.z = 0;
	}

	public bool IsMoving(){
		return moving;
	}
}
