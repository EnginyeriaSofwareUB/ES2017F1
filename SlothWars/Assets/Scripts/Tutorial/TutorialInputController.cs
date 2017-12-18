using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputController : InputController {

	void Start () {
		gameController = GameObject.Find("Main Camera").GetComponent<TutorialController>();
		uiController = GameObject.Find("Main Camera").GetComponent<TutorialUIController>();
		gettingAbilityInfo = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameController.GetStatus() == GameController.GameControllerStatus.WAITING_FOR_INPUT ){
			if ( (Input.GetKey ("up") || Input.GetKey ("w")) && (  ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.KILL_DUMMY2 || ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.TEACHING_MOVEMENT2) ) {
				gameController.MoveSloth (0, 1);
			} else if ((Input.GetKey ("down") || Input.GetKey ("s")) && (((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.KILL_DUMMY2 || ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.TEACHING_MOVEMENT2) ) {
				gameController.MoveSloth (0, -1);
			} else if ((Input.GetKey ("right") || Input.GetKey ("d")) && (((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.KILL_DUMMY2 || ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.TEACHING_MOVEMENT2) ) {
				gameController.MoveSloth (1, 0);
			} else if ((Input.GetKey ("left") || Input.GetKey ("a")) && (((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.KILL_DUMMY2 || ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.TEACHING_MOVEMENT2) ) {
				gameController.MoveSloth (-1, 0);
			} else if (Input.GetKeyDown (KeyCode.Escape) && ((TutorialController)gameController).GetTutorialStatus() == TutorialController.TutorialControllerStatus.WAITING_ESC) {
				uiController.SetActiveOptsPanel (true);
				uiController.SetActiveMainMessage(false);
				((TutorialController)gameController).NotifyEsc();
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

	public void UnPause(){
		uiController.SetActiveOptsPanel(false);
		gameController.UnPauseGame();
		((TutorialController)gameController).NotifyOptionsChecked();
	}


	public void GotIt(){
		((TutorialController)gameController).GotIt();
	}

}
