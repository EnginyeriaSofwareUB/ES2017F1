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
		if(gameController.GetStatus() == GameController2.GameControllerStatus.WAITING_FOR_INPUT){
			if(Input.GetKey("up") || Input.GetKey("w")){
				gameController.MoveSloth(0, 1);
			} else if (Input.GetKey("down") || Input.GetKey("s")){
				gameController.MoveSloth(0, -1);
			} else if (Input.GetKey("right") || Input.GetKey("d")){
				gameController.MoveSloth(1, 0);
			} else if (Input.GetKey("left") || Input.GetKey("a")){
				gameController.MoveSloth(-1, 0);
			} else if (Input.GetKeyDown(KeyCode.Escape)){
				uiController.SetActiveOptsPanel(true);
				gameController.PauseGame();
			}
		} else{
			if(gameController.GetStatus() == GameController2.GameControllerStatus.PAUSE && Input.GetKeyDown(KeyCode.Escape)){
				UnPause();
			}
		}
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

	public void UnPause(){
		uiController.SetActiveOptsPanel(false);
		gameController.UnPauseGame();
	}
}
