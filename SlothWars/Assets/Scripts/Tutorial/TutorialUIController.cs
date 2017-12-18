using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class TutorialUIController : UIController2 {
	protected GameObject tutorialPopUp;
	protected GameObject tutorialText;

	void Start () {
		s = ((TextAsset)Resources.Load("slothability")).text;
		n = JSON.Parse(s);
		mainPanel = GameObject.Find("MainUIPanel");
		optsPanel = GameObject.Find("OptionPanel");
		abilitiesPanel = GameObject.Find("AbilitiesPanel");
		infoAbPanel = GameObject.Find ("abInfoPanel");
		mainMessage = GameObject.Find("MainMessage");
		tutorialPopUp = GameObject.Find("TutorialPopUp");
		tutorialText = GameObject.Find("TutorialText");

		SetActiveOptsPanel(false);
		SetActiveAbilitiesPanel(false);
		SetActiveInfoAbPanel (false);
		SetActiveMainMessage(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (animatingMainMessage){
			if(mainMessageDuration > 0f){
				mainMessageDuration -= Time.deltaTime;
			} else {
				SetActiveMainMessage(false);
				animatingMainMessage = false;
			}
		}
		
	}


	public void SetTutorialPopUpActive(bool b){
		tutorialPopUp.SetActive(b);
	}

	public void SetTutorialText(string t){
		tutorialText.GetComponent<Text>().text = t;
	}
}
