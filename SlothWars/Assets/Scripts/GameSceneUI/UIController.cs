using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIController : MonoBehaviour {
	public Image panelMain;
	public Image panelOpts;
	private bool isPause = false;

	//optionsPanel
	private Button resumeOpts;
	private Button exitOpts;


	// Use this for initialization
	void Awake () {
		panelMain = GameObject.Find ("MainUIPanel").GetComponent<Image>();
		panelOpts = GameObject.Find ("OptionPanel").GetComponent<Image>();

		//panelMain init Elements

		//panelOpts init Elements

		resumeOpts = GameObject.Find ("ResumeGame").GetComponent<Button>();
		exitOpts = GameObject.Find ("QuitButton").GetComponent<Button>();

		panelOpts.gameObject.SetActive (false);
		panelMain.gameObject.SetActive (true);

		resumeOpts.onClick.AddListener (delegate{resumeSelected();});
		exitOpts.onClick.AddListener (delegate {exitGame ();});
	}
	
	// Update is called once per frame

	void Update () {

		//pause time if they press escape keyboard 
		if( Input.GetKeyDown(KeyCode.Escape))
		{
			
			isPause = !isPause;
			if (isPause) {
				Time.timeScale = 0;
				panelOpts.gameObject.SetActive (true);
			} else {
				Time.timeScale = 1;
				panelOpts.gameObject.SetActive (false);
			}}
		
	}


	void resumeSelected(){
		isPause = false;
		panelOpts.gameObject.SetActive (false);
		Time.timeScale = 1;
	}

	void exitGame(){
		panelOpts.gameObject.SetActive (false);
		SceneManager.LoadScene("MainMenu");
	}

}
