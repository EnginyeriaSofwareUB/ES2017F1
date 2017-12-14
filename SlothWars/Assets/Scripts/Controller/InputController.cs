using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	private GameController2 gameController;
	private UIController2 uiController;


	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Main Camera").GetComponent<GameController2>();
		uiController = GameObject.Find("Main Camera").GetComponent<UIController2>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
