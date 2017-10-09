using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	private Image panelMain;
	private Image panelVS;
	private Button playB;

	// Use this for initialization
	void Awake() {
		panelMain = GameObject.Find ("mainMenuPanel").GetComponent<Image>();
		panelVS = GameObject.Find ("playVSPanel").GetComponent<Image>();
		playB = GameObject.Find ("playButton").GetComponent<Button>();
		panelVS.gameObject.SetActive (false);
		playB.onClick.AddListener (delegate{playSelected();});

	}

	// Update is called once per frame
	void Update () {
		
	}

	void playSelected(){
		panelMain.gameObject.SetActive (false);
		panelVS.gameObject.SetActive (true);
	}
}
