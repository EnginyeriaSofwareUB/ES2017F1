﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class SlothapediaController : MonoBehaviour {
    GameObject canvas;
    public Button button;
    //Size of panels
    private float matrixWidth = 0.6f;
    private float slothWidth = 0.4f;
    private GameObject panelSlothInfo;
    private GameObject panelAbilityInfo;
    //Slothapedia json and slothability json
    private JSONNode n;
    private JSONNode m;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        GameObject background = GameObject.Find("backgroundMatrix");
        // Sizes in which we will apply the factor conversion
        int width = Screen.width;
        int height = Screen.height;

        background.GetComponent<RectTransform>().sizeDelta = new Vector2(width * matrixWidth, height);
        string s = ((TextAsset)Resources.Load("slothapedia")).text;
        string t = ((TextAsset)Resources.Load("slothability")).text;
        n = JSON.Parse(s);
        m = JSON.Parse(t);


        int numberSloths = n.Count;
        int i = 0;
        int currentSloth;
        //The first sloths can be put with the same for
        for (; i < numberSloths / 4; i++){
            for(int j = 0; j <4; j++){
                //Instiantate a button and add manually the delegate.
                //Since the button is created on execution time, this is mandatory.
                Button b = Instantiate(button);
                currentSloth = (4 * i + j);
                int a = currentSloth;
                b.name = "button" + n[currentSloth.ToString()]["type"];
                b.onClick.AddListener(delegate{
                    showSlothInfo(a);
                    });
                b.GetComponent<Image>().sprite = Resources.Load<Sprite>(n[currentSloth.ToString()]["photo"]);
                //Properly place the button
                b.transform.parent = canvas.transform;
                RectTransform rectTransform = b.GetComponent<RectTransform>();
                //This magic numbers are in order to put the proportions correctly
                rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 3, width * matrixWidth * 0.25f * 0.2f * 3);
                rectTransform.position = new Vector2(width * matrixWidth * 0.25f * 0.5f + j * width * matrixWidth * 0.25f, height -(width * matrixWidth * 0.25f * 0.5f + i * width * matrixWidth * 0.25f));

            }
        }

        //The last sloths may be a little more difficult to put.
        //This for resolves that.
        for(int j = 0; j < numberSloths % 4; j++){
            //Same as the other for. I could do a function for this, but whatever.
            Button b = Instantiate(button);
            currentSloth = (4 * i + j);
            int a = currentSloth;
            b.name = "button" + n[currentSloth.ToString()]["type"];
            b.onClick.AddListener(delegate{
                showSlothInfo(a);     
            });
            b.GetComponent<Image>().sprite = Resources.Load<Sprite>(n[currentSloth.ToString()]["photo"]);
            //Meter el button en el canvas
            b.transform.parent = canvas.transform;
            RectTransform rectTransform = b.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 3, width * matrixWidth * 0.25f * 0.2f * 3);
            rectTransform.position = new Vector2(width * matrixWidth * 0.25f * 0.5f + j * width * matrixWidth * 0.25f, height - (width * matrixWidth * 0.25f * 0.5f + i * width * matrixWidth * 0.25f));
        }

        //Place the slothResume panel where we can find the model of the sloth, its stats and its abilities
        panelSlothInfo= GameObject.Find("SlothResume");
        panelSlothInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(width * slothWidth, height*0.6f);
        //stats
        GameObject panelStatsInfo = GameObject.Find("StatsResume");
        panelStatsInfo.GetComponent<RectTransform>().sizeDelta = new Vector2((width * slothWidth)*0.5f, height*0.6f*0.33f);
        //abilities:
        GameObject panelAbilityIcon = GameObject.Find("AbilityIconResume");
        panelAbilityIcon.GetComponent<RectTransform>().sizeDelta = new Vector2((width * slothWidth)*0.5f, height*0.6f*0.33f);
        //Place the abilityResume panel where we will see the description of the selected ability:
        panelAbilityInfo = GameObject.Find("AbilityResume");
        panelAbilityInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(width * slothWidth, height*0.4f);

        //this panels must start being disabled
        panelSlothInfo.SetActive(false);
        panelAbilityInfo.SetActive(false);
    
    }

    public void showSlothInfo(int currentSloth){
        //When we press a button of a sloth, first we activate the panel sloth info
        panelSlothInfo.SetActive(true);
        panelAbilityInfo.SetActive(false);
        //Place the correct values on the stats icon
        GameObject.Find("health1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["hp"].ToString();
        GameObject.Find("attack1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["att"].ToString();
        GameObject.Find("deffence1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["def"].ToString();
        GameObject.Find("action1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["ap"].ToString();
        //Add via software the delegate. Note that this is also mandatory since this buttons are created on time execution too.
        ((Button)GameObject.Find("Ability1").GetComponent<Button>()).onClick.AddListener(delegate{
                showAbilityInfo(n[currentSloth.ToString()]["idAb1"]);     
            });
        ((Button)GameObject.Find("Ability2").GetComponent<Button>()).onClick.AddListener(delegate{
            showAbilityInfo(n[currentSloth.ToString()]["idAb2"]);     
        });
        ((Button)GameObject.Find("Ability3").GetComponent<Button>()).onClick.AddListener(delegate{
            showAbilityInfo(n[currentSloth.ToString()]["idAb3"]);     
        });

    }

    public void showAbilityInfo(string abilityID){
        //Activate the panel and show the description.
        panelAbilityInfo.SetActive(true);
        GameObject.Find("AbilityDescription").GetComponent<Text>().text = m[abilityID]["desc"];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}