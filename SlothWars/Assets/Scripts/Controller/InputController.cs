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


	public void ChangeTurn(){
		if(gameController.GetStatus() == GameController2.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.EndTurn();
		}
	}


	public void CastAbility1(){
		if(gameController.GetStatus() == GameController2.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility1();
		}
	}

	public void CastAbility2(){
		if(gameController.GetStatus() == GameController2.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility2();
		}
	}

	public void CastAbility3(){
		if(gameController.GetStatus() == GameController2.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility3();
		}
	}
}
