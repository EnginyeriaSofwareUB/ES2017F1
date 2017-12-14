using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController2 : MonoBehaviour {
	public GameObject mainPanel, optsPanel, abilitiesPanel;
	// Use this for initialization
	
	void Start () {
		mainPanel = GameObject.Find("MainUIPanel");
		optsPanel = GameObject.Find("OptionPanel");
		abilitiesPanel = GameObject.Find("AbilitiesPanel");

		SetActiveOptsPanel(false);
		SetActiveAbilitiesPanel(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SetActiveMainPanel(bool b){
		mainPanel.SetActive(b);
	}

	public void SetActiveOptsPanel(bool b){
		optsPanel.SetActive(b);
	}

	public void SetActiveAbilitiesPanel(bool b){
		abilitiesPanel.SetActive(b);
	}
}
