using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class SlothapediaController : MonoBehaviour {
    GameObject canvas;
    public Button button;
    //Size of panels
    private float matrixWidth = 0.6f;
    private float slothWidth = 0.4f;
    private GameObject panelSlothInfo;
    private GameObject panelAbilityInfo;
    private GameObject slothModel;
    private GameObject rawImage;
    private Quaternion quaternion;
	public AudioSource slothapediaSound;
    //Slothapedia json and slothability json
    private JSONNode n;
    private JSONNode m;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        GameObject background = GameObject.Find("backgroundMatrix");
        slothModel = GameObject.Find("slothModel");
        rawImage = GameObject.Find("RawImage");
        quaternion = Quaternion.Euler(0, 5f, 0);
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

				name = n [currentSloth.ToString ()] ["photo"];
				name = name.Insert (0, "f");
				Debug.Log (name);
				b.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
                //Properly place the button
                b.transform.parent = canvas.transform;
                RectTransform rectTransform = b.GetComponent<RectTransform>();
                //This magic numbers are in order to put the proportions correctly
                rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 4.5f, width * matrixWidth * 0.25f * 0.2f * 4.5f);
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
			name = n [currentSloth.ToString ()] ["photo"];
			name = name.Insert (0, "f");
			Debug.Log (name);
			b.GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
            //b.GetComponent<Image>().sprite = Resources.Load<Sprite>(n[currentSloth.ToString()]["photo"]);
            //Meter el button en el canvas
            b.transform.parent = canvas.transform;
            RectTransform rectTransform = b.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 4.5f, width * matrixWidth * 0.25f * 0.2f * 4.5f);
            rectTransform.position = new Vector2(width * matrixWidth * 0.25f * 0.5f + j * width * matrixWidth * 0.25f, height - (width * matrixWidth * 0.25f * 0.5f + i * width * matrixWidth * 0.25f));
        }

        //Place the slothResume panel where we can find the model of the sloth, its stats and its abilities
        panelSlothInfo= GameObject.Find("SlothResume");
        panelSlothInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(width * slothWidth, height*0.6f);
        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(height*0.6f*0.66f, height*0.6f*0.66f);
        //stats
        GameObject panelStatsInfo = GameObject.Find("StatsResume");
        panelStatsInfo.GetComponent<RectTransform>().sizeDelta = new Vector2((width * slothWidth)*0.5f, height*0.6f*0.33f);
        //abilities:
        GameObject panelAbilityIcon = GameObject.Find("AbilityIconResume");
        panelAbilityIcon.GetComponent<RectTransform>().sizeDelta = new Vector2((width * slothWidth)*0.5f, height*0.6f*0.33f);
        //Place the abilityResume panel where we will see the description of the selected ability:
        panelAbilityInfo = GameObject.Find("AbilityResume");

        panelAbilityInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(width * slothWidth, height*0.4f);



        //this panels must start being enabled
        panelSlothInfo.SetActive(true);
        panelAbilityInfo.SetActive(true);
		GameObject.Find("Ability1").GetComponent<Button>().gameObject.SetActive(false);
		GameObject.Find("Ability2").GetComponent<Button>().gameObject.SetActive(false);
		GameObject.Find("Ability3").GetComponent<Button>().gameObject.SetActive(false);

    
    }

    public void showSlothInfo(int currentSloth){
        //if passes Sprint 4 review delete this lines below
		//When we press a button of a sloth, first we activate the panel sloth info
        //panelSlothInfo.SetActive(true);
        //panelAbilityInfo.SetActive(false);

		//reactivate all inactive buttons
		Button [] abilityButtons;
		abilityButtons = GameObject.Find("AbilityIconResume").GetComponentsInChildren<Button>(true);
		foreach (Button abButton in abilityButtons) {
			abButton.gameObject.SetActive (true);
		}
		//clean ability description
		GameObject.Find("AbilityDescription").GetComponent<Text>().text = "";

		//Place the correct values on the stats icon
        GameObject.Find("health1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["hp"].ToString();
        GameObject.Find("attack1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["att"].ToString();
        GameObject.Find("deffence1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["def"].ToString();
        GameObject.Find("action1Value").GetComponent<Text>().text = n[currentSloth.ToString()]["ap"].ToString();
        Destroy(slothModel);
        GameObject kk = Resources.Load<GameObject>("ModelosDefinitivos/ModelosSlothapedia/"+n[currentSloth.ToString()]["model"]);
        slothModel = Instantiate(kk);
        slothModel.transform.position = new Vector3(-100f, -100.023f, 0.0489f);
        
        //Add via software the delegate. Note that this is also mandatory since this buttons are created on time execution too.
        ((Button)GameObject.Find("Ability1").GetComponent<Button>()).onClick.AddListener(delegate{
                showAbilityInfo(n[currentSloth.ToString()]["idAb1"]);     
            });
		//changingImage


		//Debug.Log("hola");
		//Debug.Log(newSprite);
		///Image theImage = 

		name = n[currentSloth.ToString()]["idAb1"];
		name = name.Insert (0, "Spellicons/");
		GameObject.Find("Ability1").GetComponent<Image>().sprite = Resources.Load<Sprite>(name);

		//theImage.sprite = newSprite;​


		((Button)GameObject.Find("Ability2").GetComponent<Button>()).onClick.AddListener(delegate{
            showAbilityInfo(n[currentSloth.ToString()]["idAb2"]);     
        });

		name = n[currentSloth.ToString()]["idAb2"];
		name = name.Insert (0, "Spellicons/");
		GameObject.Find("Ability2").GetComponent<Image>().sprite = Resources.Load<Sprite>(name);

        ((Button)GameObject.Find("Ability3").GetComponent<Button>()).onClick.AddListener(delegate{
            showAbilityInfo(n[currentSloth.ToString()]["idAb3"]);     
        });

		name = n[currentSloth.ToString()]["idAb3"];
		name = name.Insert (0, "Spellicons/");
		GameObject.Find("Ability3").GetComponent<Image>().sprite = Resources.Load<Sprite>(name);
	
    }

    public void showAbilityInfo(string abilityID){
        //Activate the panel and show the description.
        //panelAbilityInfo.SetActive(true);
        GameObject.Find("AbilityDescription").GetComponent<Text>().text = m[abilityID]["desc"];
    }

	public void BackToMenu(){
		SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
        if(slothModel != null){
		    slothModel.transform.Rotate(quaternion.eulerAngles*Time.deltaTime);
        }
	}
}
