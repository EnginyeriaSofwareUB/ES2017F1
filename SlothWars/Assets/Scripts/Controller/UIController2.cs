using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


	public void DisplaySlothAbilities(Sloth sloth){
		SetActiveAbilitiesPanel(true);
		GameObject.Find("buttonAbility1").GetComponent<Image>().sprite = Resources.Load<Sprite>("Spellicons/"+sloth.GetIdAb1());
        GameObject.Find("buttonAbility2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Spellicons/"+sloth.GetIdAb2());
        GameObject.Find("buttonAbility3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Spellicons/"+sloth.GetIdAb3());
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

	public void SetActiveAb1(bool b){
		((Button)GameObject.Find("buttonAbility1").GetComponent<Button>()).interactable = b;
	}

	public void SetActiveAb2(bool b){
		((Button)GameObject.Find("buttonAbility2").GetComponent<Button>()).interactable = b;
	}

	public void SetActiveAb3(bool b){
		((Button)GameObject.Find("buttonAbility3").GetComponent<Button>()).interactable = b;
	}

	
}
