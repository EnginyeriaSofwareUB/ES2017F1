using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	private GameController gameController;
	private UIController2 uiController;

	private bool gettingAbilityInfo;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
		uiController = GameObject.Find("Main Camera").GetComponent<UIController2>();
		gettingAbilityInfo = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT){
			if (Input.GetKey ("up") || Input.GetKey ("w")) {
				gameController.MoveSloth (0, 1);
			} else if (Input.GetKey ("down") || Input.GetKey ("s")) {
				gameController.MoveSloth (0, -1);
			} else if (Input.GetKey ("right") || Input.GetKey ("d")) {
				gameController.MoveSloth (1, 0);
			} else if (Input.GetKey ("left") || Input.GetKey ("a")) {
				gameController.MoveSloth (-1, 0);
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				uiController.SetActiveOptsPanel (true);
				gameController.PauseGame ();
			} else if (Input.GetMouseButtonUp (1) && gettingAbilityInfo) {
				gameController.CancelAbility ();
				gettingAbilityInfo = false;
			}
		} else{
			if(gameController.GetStatus() == GameController.GameControllerStatus.PAUSE && Input.GetKeyDown(KeyCode.Escape)){
				UnPause();
			}
		}
	}


	public void ChangeTurn(){
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.EndTurn();
			
		}
	}


	public void CastAbility1(){
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility1();
			gettingAbilityInfo = true;
		}
	}

	public void CastAbility2(){
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility2();
			gettingAbilityInfo = true;
		}
	}

	public void CastAbility3(){
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT){
			gameController.CastAbility3();
			gettingAbilityInfo = true;
		}
	}

	public void Surrender(){
		gameController.Surrender();
	}

	public void QuitGame(){
		gameController.QuitGame();
	}

	public void UnPause(){
		uiController.SetActiveOptsPanel(false);
		gameController.UnPauseGame();
	}
}
