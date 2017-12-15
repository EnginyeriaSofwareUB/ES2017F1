using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController2 : MonoBehaviour {
	public GameObject mainPanel, optsPanel, abilitiesPanel, mainMessage;
	private float mainMessageDuration;
	private bool animatingMainMessage = false;
	// Use this for initialization
	
	void Start () {
		mainPanel = GameObject.Find("MainUIPanel");
		optsPanel = GameObject.Find("OptionPanel");
		abilitiesPanel = GameObject.Find("AbilitiesPanel");
		mainMessage = GameObject.Find("MainMessage");

		SetActiveOptsPanel(false);
		SetActiveAbilitiesPanel(false);
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

	public void SetActiveGameButtons(bool b){
		SetActiveAb1(b);
		SetActiveAb2(b);
		SetActiveAb3(b);
		SetActiveTurn(b);
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

	public void SetActiveTurn(bool b){
		((Button)GameObject.Find("EndTurnButton").GetComponent<Button>()).interactable = b;
	}

	public void SetActiveMainMessage(bool b){
		mainMessage.SetActive(b);
	}

	public void NotifyNotEnoughAp(){
		SetActiveMainMessage(true);
		mainMessage.GetComponent<Text>().text = "You don't have enough action points left";
		mainMessageDuration = 1f;
		animatingMainMessage = true;
	}
	public void DisplaySlothStats(Sloth sloth){
		GameObject.Find("lifeSlothText").GetComponent<Text>().text = ((int)sloth.GetMaxHp()).ToString();
		GameObject.Find("attackSlothText").GetComponent<Text>().text = sloth.GetAttack().ToString();
		GameObject.Find("shieldSlothText").GetComponent<Text>().text = sloth.GetDefense().ToString();
		GameObject.Find("actionSlothText").GetComponent<Text>().text = sloth.GetAp().ToString();
		GameObject.Find("slothImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(sloth.GetSprite());
	}

	public void UpdateTurnPlayerInfo(bool p){
		if (p){
			GameObject.Find("TurnText").GetComponent<Text>().text = "Blue";
		} else {
			GameObject.Find("TurnText").GetComponent<Text>().text = "Red";
		}
	}

	
}
