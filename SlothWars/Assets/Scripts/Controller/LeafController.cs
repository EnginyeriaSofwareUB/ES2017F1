using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition =  new Vector3(0f, 1+ 0.5f*Mathf.Cos(Time.time), 0f);
	}
}
