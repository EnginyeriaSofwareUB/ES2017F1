using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputController : InputController {

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
			} else if (Input.GetMouseButtonUp (1) && gettingAbilityInfo && gameController.GetCurrentSloth().GetComponent<ShotScript>().GetShotLoad()) {
				gameController.CancelAbility ();
				gettingAbilityInfo = false;
			}
		} else{
			if(gameController.GetStatus() == GameController.GameControllerStatus.PAUSE && Input.GetKeyDown(KeyCode.Escape)){
				UnPause();
			}
		}
	}


	public void GotIt(){
		((TutorialController)gameController).GotIt();
	}
}
