using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {

	private Image panelMain;
	private Image panelVS;
	private Image panelOpts;

	private Button playB;
	private Button slothPB;
	private Button optionsB;
	private Button exitB;

	//play Panel
	private Button playIAB;
	private Button playFriendB;
	private Button exitPB;
	//optionsPanel

	//back button on options
	private Button backOButton;

	// Use this for initialization
	void Awake() {
		//panelinit
		panelMain = GameObject.Find ("mainMenuPanel").GetComponent<Image>();
		panelVS = GameObject.Find ("playVSPanel").GetComponent<Image>();
		panelOpts = GameObject.Find("OptionsPanel").GetComponent<Image>();

		//panelMain init elements
		playB = GameObject.Find ("playButton").GetComponent<Button>();
		optionsB = GameObject.Find ("optionsButton").GetComponent<Button>();
		slothPB = GameObject.Find ("spButton").GetComponent<Button>();
		exitB = GameObject.Find ("exitButton").GetComponent<Button>();

		//panelOpts init elements
		backOButton = GameObject.Find ("exitOButton").GetComponent<Button>();

		//panelPlay init elemtns

		playIAB = GameObject.Find ("iaButton").GetComponent<Button>();
		playFriendB = GameObject.Find ("friendButton").GetComponent<Button>();
		exitPB = GameObject.Find ("exitPButton").GetComponent<Button>();
	
		//inactive to show main menu
		panelVS.gameObject.SetActive (false);
		panelOpts.gameObject.SetActive (false);

		//add listeners to panelMain buttons

		playB.onClick.AddListener (delegate{playSelected();});
		optionsB.onClick.AddListener (delegate{showOptions();});
		//slothPB.onClick.AddListener (delegate{});
		exitB.onClick.AddListener (delegate{exitGame();});


		//add listeners to panelplay buttons
		playIAB.onClick.AddListener (delegate{playSoloSelected();});
		playFriendB.onClick.AddListener (delegate{playFriendSelected();});
		exitPB.onClick.AddListener (delegate{goBackPlay();});

		//add listeners to panelOpts buttons
		backOButton.onClick.AddListener(delegate{goBackOptions();}); 

	}

	// Update is called once per frame
	void Update () {
		
	}

	void playSelected(){
		panelMain.gameObject.SetActive (false);
		panelVS.gameObject.SetActive (true);
	}
	//this should go to new screen
	void playSoloSelected(){
		SceneManager.LoadScene ("TeamSelection");
	}
	//this should go 
	void playFriendSelected(){
		SceneManager.LoadScene ("TeamSelection");
	}
	void goBackPlay(){
		panelMain.gameObject.SetActive (true);
		panelVS.gameObject.SetActive (false);
	}
	void showOptions(){
		panelMain.gameObject.SetActive (false);
		panelOpts.gameObject.SetActive (true);
	}

	void goBackOptions(){
		panelMain.gameObject.SetActive (true);
		panelOpts.gameObject.SetActive (false);
}

    //Use this method to load any scene.
    public void loadScene(string scene){
        SceneManager.LoadScene(scene);

    }
	void exitGame(){
		Application.Quit();
	}
}