using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : GameController {

	private static int checkAwakeTu;
	private GameObject panelTutorial;
	private GameObject panelTutorialText;

    void Awake(){
        if (checkAwakeTu == 0)
        {
            checkAwakeTu = 1;
			StorePersistentVariables.Instance.slothTeam1.Add(new Sloth("Wizard"));
			Debug.Log(new Sloth("Wizard").GetSprite());
			StorePersistentVariables.Instance.slothTeam2.Add(new Sloth("Wizard"));
            InitializePlayer();
            InitializeTerrain();
            PlacePlayers();
            InitializeTurnVariables();
            InitializeAbilityVariables();
            InitializeLogicVariables();
            InitializeUIVariables();
        }

    }


	// Use this for initialization
	void Start () {

		panelTutorial = GameObject.Find("TutorialPopUp");
		panelTutorialText = GameObject.Find("TutorialText");
		panelTutorialText.GetComponent<Text>().text = "Welcome to the tutorial!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void ChangePanelTutorialText(){

	}

	public void DeactivatePanelTutorial(){
		panelTutorial.SetActive(false);
	}
}
