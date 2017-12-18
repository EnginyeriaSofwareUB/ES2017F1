using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
	private Vector3 target;
	private bool isWorld;
	private GameObject arrow;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find("RedArrow");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getTarget(){
		return target;
	}

	public void setTarget(Vector3 p){
		target = p;
	}

	public void setIsWorld(bool b){
		isWorld = b;
	}

	public void setArrowActive(bool b){
		arrow.SetActive(b);
	}
}
