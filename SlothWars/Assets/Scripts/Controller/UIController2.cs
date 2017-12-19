using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;


public class UIController2 : MonoBehaviour {
	public GameObject mainPanel, optsPanel, abilitiesPanel, infoAbPanel, mainMessage;
	protected string[] abilitiesInfo = new string[3];
	protected string s;
	protected JSONNode n;
	protected float mainMessageDuration;
	protected bool animatingMainMessage = false;
	// Use this for initialization
	
	void Start () {
		s = ((TextAsset)Resources.Load("slothability")).text;
		n = JSON.Parse(s);
		mainPanel = GameObject.Find("MainUIPanel");
		optsPanel = GameObject.Find("OptionPanel");
		abilitiesPanel = GameObject.Find("AbilitiesPanel");
		infoAbPanel = GameObject.Find ("abInfoPanel");
		mainMessage = GameObject.Find("MainMessage");

		SetActiveOptsPanel(false);
		SetActiveAbilitiesPanel(false);
		SetActiveInfoAbPanel (false);
		SetActiveMainMessage(true);
        mainMessage.GetComponent<Text>().text = "";
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

		abilitiesInfo [0] = n[sloth.GetIdAb1 ()] ["desc"];
        GameObject.Find("buttonAbility2").GetComponent<Image>().sprite = Resources.Load<Sprite>("Spellicons/"+sloth.GetIdAb2());
		abilitiesInfo [1] = n[sloth.GetIdAb2 ()] ["desc"];
        GameObject.Find("buttonAbility3").GetComponent<Image>().sprite = Resources.Load<Sprite>("Spellicons/"+sloth.GetIdAb3());
		abilitiesInfo [2] = n[sloth.GetIdAb3 ()] ["desc"];
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

	public void SetActiveInfoAbPanel(bool b){
		infoAbPanel.SetActive(b);
	}

	public Image getInfoAbPanel(){
		return infoAbPanel.GetComponent<Image>();
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
		Debug.Log(b);
		mainMessage.SetActive(b);
	}

	public void ChangeMainMessage(string t){
		mainMessage.GetComponent<Text>().text = t;
	}

	public void NotifyNotEnoughAp(){
		SetActiveMainMessage(true);
		mainMessage.GetComponent<Text>().text = "You don't have enough action points left";
		mainMessageDuration = 1f;
		animatingMainMessage = true;
	}

    public void DisplaySlothStats(Sloth sloth,int currentAp)
    {
        //GameObject.Find("lifeSlothText").GetComponent<Text>().text = ((int)sloth.GetHp()).ToString();
       // GameObject.Find("attackSlothText").GetComponent<Text>().text = sloth.GetAttack().ToString();
       // GameObject.Find("shieldSlothText").GetComponent<Text>().text = sloth.GetDefense().ToString();
        GameObject.Find("actionSlothText").GetComponent<Text>().text = currentAp.ToString();
        GameObject.Find("slothImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(sloth.GetSprite());
    }


	public void UpdateTurnPlayerInfo(bool p){
		Text turnText = GameObject.Find ("TurnText").GetComponent<Text> ();
		if (p){
			turnText.color = new Color(0.0f/255.0f, 97.0f/255.0f, 255.0f/255.0f);
			turnText.text = "Blue Turn";
		} else {
			turnText.color = new Color(255.0f/255.0f, 0.0f/255.0f, 0.0f/255.0f);
			turnText.text = "Red Turn";
		}
	}

	public string getAbilityInfo(string nameAbButton){
		string abText;
		switch(nameAbButton){
			case "buttonAbility1":
				abText = abilitiesInfo [0];
				break;
			case "buttonAbility2":
			abText =  abilitiesInfo [1];
				break;
			case "buttonAbility3":
			abText =  abilitiesInfo [2];
				break;
			default:
			abText =  "";
				break;

		}
		return abText;
	}

		

	
}
