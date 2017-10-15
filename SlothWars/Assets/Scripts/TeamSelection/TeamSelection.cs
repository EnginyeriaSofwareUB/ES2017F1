using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour {

    private Text numPlayer;

    private Button prevPage;
    private Button nextPage;

    private Text slot1Type;
    private Image slot1Pic;
    private Text slot1Health;
    private Text slot1Attack;
    private Text slot1Defense;
    private Text slot1Action;

    private Text slot2Type;
    private Image slot2Pic;
    private Text slot2Health;
    private Text slot2Attack;
    private Text slot2Defense;
    private Text slot2Action;

    private Text slot3Type;
    private Image slot3Pic;
    private Text slot3Health;
    private Text slot3Attack;
    private Text slot3Defense;
    private Text slot3Action;

    private Image teamSlot1Pic;
    private Image teamSlot2Pic;
    private Image teamSlot3Pic;
    private Image teamSlot4Pic;

    JSONNode node;
    int lenJson;
    int currentPage;


    private void Awake()
    {
        // Dynamic values

        numPlayer = GameObject.Find("playerValue").GetComponent<Text>();

        slot1Type = GameObject.Find("type1Value").GetComponent<Text>();
        slot1Pic = GameObject.Find("sloth1Pic").GetComponent<Image>();
        slot1Health = GameObject.Find("health1Value").GetComponent<Text>();
        slot1Attack= GameObject.Find("attack1Value").GetComponent<Text>();
        slot1Defense = GameObject.Find("deffence1Value").GetComponent<Text>();
        slot1Action = GameObject.Find("action1Value").GetComponent<Text>();

        slot2Type = GameObject.Find("type2Value").GetComponent<Text>();
        slot2Pic = GameObject.Find("sloth2Pic").GetComponent<Image>();
        slot2Health = GameObject.Find("health2Value").GetComponent<Text>();
        slot2Attack = GameObject.Find("attack2Value").GetComponent<Text>();
        slot2Defense = GameObject.Find("deffence2Value").GetComponent<Text>();
        slot2Action = GameObject.Find("action2Value").GetComponent<Text>();

        slot3Type = GameObject.Find("type3Value").GetComponent<Text>();
        slot3Pic = GameObject.Find("sloth3Pic").GetComponent<Image>();
        slot3Health = GameObject.Find("health3Value").GetComponent<Text>();
        slot3Attack = GameObject.Find("attack3Value").GetComponent<Text>();
        slot3Defense = GameObject.Find("deffence3Value").GetComponent<Text>();
        slot3Action = GameObject.Find("action3Value").GetComponent<Text>();

        // Buttons

        prevPage = GameObject.Find("leftArrow").GetComponent<Button>();
        nextPage = GameObject.Find("rightArrow").GetComponent<Button>();

        // Listeners

        prevPage.onClick.AddListener(delegate { PrevPageClick(); });
        nextPage.onClick.AddListener(delegate { NextPageClick(); });

        // Other

        string s = ((TextAsset)Resources.Load("slothapedia")).text;
        node = JSON.Parse(s);
        lenJson = node.Count;
        currentPage = 0;
        numPlayer.text = "1";
    }

    // Use this for initialization
    void Start () {
        UpdateSlots();
    }

    void PrevPageClick()
    {
        //currentPage--;
        UpdateSlots();
    }

    void NextPageClick()
    {
        //currentPage++;
        UpdateSlots();
    }

    void UpdateSlots()
    {
        int i = currentPage * 3;

        slot1Type.text = node[i]["type"];
        slot1Pic.sprite = Resources.Load(node[i]["photo"]) as Sprite;
        slot1Health.text = node[i]["hp"];
        slot1Attack.text = node[i]["att"];
        slot1Defense.text = node[i]["def"];
        slot1Action.text = node[i]["ap"];

        // if this slot is used:
        slot2Type.text = node[i + 1]["type"];
        slot2Pic.sprite = Resources.Load(node[i + 1]["photo"]) as Sprite;
        slot2Health.text = node[i + 1]["hp"];
        slot2Attack.text = node[i + 1]["att"];
        slot2Defense.text = node[i + 1]["def"];
        slot2Action.text = node[i + 1]["ap"];
        // else: set slot inactive

        // if this slot is used:
        slot3Type.text = node[i + 2]["type"];
        slot2Pic.sprite = Resources.Load(node[i + 2]["photo"]) as Sprite;
        slot3Health.text = node[i + 2]["hp"];
        slot3Attack.text = node[i + 2]["att"];
        slot3Defense.text = node[i + 2]["def"];
        slot3Action.text = node[i + 2]["ap"];
        // else: set slot inactive
    }
}
