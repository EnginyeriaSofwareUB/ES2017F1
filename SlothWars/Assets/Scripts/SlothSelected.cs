﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSelected : MonoBehaviour {
	private TextMesh text;



	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<TextMesh>();
		text.text = "Sloth Selected";
	}
		
	public void turnRight()
	{
		text.transform.eulerAngles = new Vector3(0, 0, 0);
		text.transform.localPosition = new Vector3(0, 3, -0.5f);
	}
	public void turnLeft()
	{
		text.transform.eulerAngles = new Vector3(0, 360, 0);
		text.transform.localPosition = new Vector3(0, 3, 0.5f);
	}

}
