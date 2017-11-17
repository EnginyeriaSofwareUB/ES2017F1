using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour {

    private static bool created = false;

	private const int maxTeamSloths = 4;
	private Text currentPageText;
	private Text numPlayer;

	private Button prevPage;
	private Button nextPage;

	private GameObject slot1;
	private GameObject slot2;
	private GameObject slot3;
	
	private Text slot1Type;
	private Button slot1Pic;
	private Text slot1Health;
	private Text slot1Attack;
	private Text slot1Defense;
	private Text slot1Action;

	private Text slot2Type;
	private Button slot2Pic;
	private Text slot2Health;
	private Text slot2Attack;
	private Text slot2Defense;
	private Text slot2Action;

	private Text slot3Type;
	private Button slot3Pic;
	private Text slot3Health;
	private Text slot3Attack;
	private Text slot3Defense;
	private Text slot3Action;

	private Image team1Slot1Pic;
	private Image team1Slot2Pic;
	private Image team1Slot3Pic;
	private Image team1Slot4Pic;

	private Image team2Slot1Pic;
	private Image team2Slot2Pic;
	private Image team2Slot3Pic;
	private Image team2Slot4Pic;

	JSONNode node;
	int lenJson;
	int slothPerPage;
	int currentPage;
	int lastPage;
	int lastPageActiveSlot;
	string lastPlayer;
     
	void Awake()
	{

        // Dynamic elements

        currentPageText = GameObject.Find("currentPage").GetComponent<Text>();
		numPlayer = GameObject.Find("playerValue").GetComponent<Text>();

		slot1 = GameObject.Find ("slot1");
		slot2 = GameObject.Find ("slot2");
		slot3 = GameObject.Find ("slot3");

		slot1Type = GameObject.Find("type1Value").GetComponent<Text>();
		slot1Health = GameObject.Find("health1Value").GetComponent<Text>();
		slot1Attack= GameObject.Find("attack1Value").GetComponent<Text>();
		slot1Defense = GameObject.Find("deffence1Value").GetComponent<Text>();
		slot1Action = GameObject.Find("action1Value").GetComponent<Text>();

		slot2Type = GameObject.Find("type2Value").GetComponent<Text>();
		slot2Health = GameObject.Find("health2Value").GetComponent<Text>();
		slot2Attack = GameObject.Find("attack2Value").GetComponent<Text>();
		slot2Defense = GameObject.Find("deffence2Value").GetComponent<Text>();
		slot2Action = GameObject.Find("action2Value").GetComponent<Text>();

		slot3Type = GameObject.Find("type3Value").GetComponent<Text>();
		slot3Health = GameObject.Find("health3Value").GetComponent<Text>();
		slot3Attack = GameObject.Find("attack3Value").GetComponent<Text>();
		slot3Defense = GameObject.Find("deffence3Value").GetComponent<Text>();
		slot3Action = GameObject.Find("action3Value").GetComponent<Text>();

		team1Slot1Pic = GameObject.Find("team1Slot1Pic").GetComponent<Image>();
		team1Slot2Pic = GameObject.Find("team1Slot2Pic").GetComponent<Image>();
		team1Slot3Pic = GameObject.Find("team1Slot3Pic").GetComponent<Image>();
		team1Slot4Pic = GameObject.Find("team1Slot4Pic").GetComponent<Image>();

		team2Slot1Pic = GameObject.Find("team2Slot1Pic").GetComponent<Image>();
		team2Slot2Pic = GameObject.Find("team2Slot2Pic").GetComponent<Image>();
		team2Slot3Pic = GameObject.Find("team2Slot3Pic").GetComponent<Image>();
		team2Slot4Pic = GameObject.Find("team2Slot4Pic").GetComponent<Image>();

		// Buttons

		slot1Pic = GameObject.Find("slot1Pic").GetComponent<Button>();
		slot2Pic = GameObject.Find("slot2Pic").GetComponent<Button>();
		slot3Pic = GameObject.Find("slot3Pic").GetComponent<Button>();

		prevPage = GameObject.Find("leftArrow").GetComponent<Button>();
		nextPage = GameObject.Find("rightArrow").GetComponent<Button>();

		// Listeners

		prevPage.onClick.AddListener(delegate { PrevPageClick(); });
		nextPage.onClick.AddListener(delegate { NextPageClick(); });
		slot1Pic.onClick.AddListener(delegate { SlothSelect("1"); });
		slot2Pic.onClick.AddListener(delegate { SlothSelect("2"); });
		slot3Pic.onClick.AddListener(delegate { SlothSelect("3"); });

		// Other

		// GameControl.control.ClearData();
		string s = ((TextAsset)Resources.Load("slothapedia")).text;
		node = JSON.Parse(s);
		lenJson = node.Count;
		// if lenJson==0 : System.Exit();
		slothPerPage = 3;
		int slothModule = (lenJson % slothPerPage);
	    currentPage = 0;
		// Number of aviable pages to select a sloth.
		lastPage = (slothModule == 0) ? (lenJson/slothPerPage)-1 : (int) lenJson/slothPerPage;
		// Number of the last active slot in the last page.
		lastPageActiveSlot = (slothModule == 0) ? 3 : slothModule;
		Debug.Log ("Last page active slot: "+lastPageActiveSlot);
		prevPage.gameObject.SetActive (false);
		currentPageText.text = "1/"+(lastPage+1);
		numPlayer.text = "1"; // This will later be selected randomly
		lastPlayer = "2"; // This will later be selected randomly

		Debug.Log ("Len of json: "+lenJson);
		Debug.Log ("Last json element: "+node [lenJson-1] ["type"]);
	}

	void Start () {
		UpdateSlots();
	}

	void SlothSelect(string slot){
        string type = GetSlothType(slot);
		if("1".Equals(numPlayer.text)){
			if(CompareSloths(StorePersistentVariables.Instance.slothTeam1, type)){
				Debug.Log ("Team 1 already has this sloth");
                // ScreenMessage.showMessage("Your team already has this sloth");
            }
            else{
                StorePersistentVariables.Instance.slothTeam1.Add(new Sloth(type));

                //slothTeam1.Add(new Sloth(type));
				Debug.Log (type+" sloth added to team 1");
				SetTeamSlotPic("1",slot,StorePersistentVariables.Instance.slothTeam1.Count);
			    numPlayer.text = "2";
            }
        }
        else{
			if(CompareSloths(StorePersistentVariables.Instance.slothTeam2, type)){
				Debug.Log ("Team 2 already has this sloth");
                // ScreenMessage.showMessage("Your team already has this sloth");
            }
            else{
                StorePersistentVariables.Instance.slothTeam2.Add(new Sloth(type));
				//slothTeam2.Add (new Sloth(type));
				Debug.Log (type+" sloth added to team 2");
				SetTeamSlotPic("2",slot,StorePersistentVariables.Instance.slothTeam2.Count);
				if(StorePersistentVariables.Instance.slothTeam2.Count == maxTeamSloths){
					Debug.Log ("TEAM SELECTION FINISHED!");
                    //ScreenMessage.showMessage("TEAM SELECTION FINISHED!");
                    //WaitAndLoadScene(3);
					SceneManager.LoadScene ("default_scene");
                }
                numPlayer.text = "1";
            }
        }
	}

	private bool CompareSloths (List<Sloth> list, string type){
		foreach (Sloth sloth in list) {
			if (sloth.GetTypeName().Equals (type)) {
				return true;
			}
		}
		return false;
		
	}
    
    // Adding the images in team1SlothImages and team2SlothImages
	void SetTeamSlotPic(string team, string slot, int teamSlot){
		if("1".Equals(team)){
			switch(teamSlot){
			case 1:
               
                team1Slot1Pic.sprite = GetSlothSprite(slot);
				break;
			case 2:
                
                team1Slot2Pic.sprite = GetSlothSprite(slot);
				break;
			case 3:
               
                team1Slot3Pic.sprite = GetSlothSprite(slot);
				break;
			case 4:
                
                team1Slot4Pic.sprite = GetSlothSprite(slot);
				break;
			default:
				// throw Exception
				break;
			}
		}
		else{
			switch(teamSlot){
			case 1:
                
                team2Slot1Pic.sprite = GetSlothSprite (slot);
				break;
			case 2:
                
                team2Slot2Pic.sprite = GetSlothSprite(slot);
				break;
			case 3:
                
                team2Slot3Pic.sprite = GetSlothSprite(slot);
				break;
			case 4:
                
                team2Slot4Pic.sprite = GetSlothSprite(slot);
				break;
			default:
				// throw Exception
				break;
			}
        }
	}

	Sprite GetSlothSprite(string slot){
		switch(slot){
		case "1":
			return slot1Pic.image.sprite;
		case "2":
			return slot2Pic.image.sprite;
		case "3":
			return slot3Pic.image.sprite;
		default:
			// THROW EXCEPTION!
			return null;
		}
	}

	string GetSlothType(string slot){
		switch(slot){
		case "1":
			return slot1Type.text;
		case "2":
			return slot2Type.text;
		case "3":
			return slot3Type.text;
		default:
			// THROW EXCEPTION! 
			return null;
		}
	}

	void PrevPageClick()
	{
		if(currentPage==lastPage){
			nextPage.gameObject.SetActive (true);
			slot2.SetActive (true);
			slot3.SetActive (true);
		}
		currentPage--;
		currentPageText.text = (currentPage+1) + "/" + (lastPage+1);
		if(currentPage==0){
			prevPage.gameObject.SetActive (false);
		}
		UpdateSlots();
	}

	void NextPageClick()
	{
		if(currentPage==0){
			prevPage.gameObject.SetActive (true);
		}
		currentPage++;
		currentPageText.text = (currentPage+1) + "/" + (lastPage+1);
		if(currentPage==lastPage){
			nextPage.gameObject.SetActive (false);
		}
		UpdateSlots();
	}

	void UpdateSlots()
	{
		int i = currentPage * 3;

		slot1Type.text = node[i]["type"];
		slot1Pic.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(node [i] ["photo"]); // ASI FUNCTIONA
		slot1Health.text = node[i]["hp"];
		slot1Attack.text = node[i]["att"];
		slot1Defense.text = node[i]["def"];
		slot1Action.text = node[i]["ap"];

		if(currentPage==lastPage && lastPageActiveSlot<2){
			slot2.SetActive (false);
		}
		else{
			slot2Type.text = node[i + 1]["type"];
			slot2Pic.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(node [i + 1] ["photo"]); // ASI NO :V
			slot2Health.text = node[i + 1]["hp"];
			slot2Attack.text = node[i + 1]["att"];
			slot2Defense.text = node[i + 1]["def"];
			slot2Action.text = node[i + 1]["ap"];
		}

		if(currentPage==lastPage && lastPageActiveSlot<3){
			slot3.SetActive (false);
		}
		else{
			slot3Type.text = node[i + 2]["type"];
			slot3Pic.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(node [i + 2] ["photo"]);
			slot3Health.text = node[i + 2]["hp"];
			slot3Attack.text = node[i + 2]["att"];
			slot3Defense.text = node[i + 2]["def"];
			slot3Action.text = node[i + 2]["ap"];
		}
	}

}