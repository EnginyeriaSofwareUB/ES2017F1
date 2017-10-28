using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class SlothapediaController : MonoBehaviour {
    GameObject canvas;
    public Button button;
    private float matrixWidth = 0.6f;
    private float slothWidth = 0.4f;

	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
        GameObject background = GameObject.Find("backgroundMatrix");
        int width = Screen.width;
        int height = Screen.height;

        background.GetComponent<RectTransform>().sizeDelta = new Vector2(width * matrixWidth, height);
        string s = ((TextAsset)Resources.Load("slothapedia")).text;
        JSONNode n = JSON.Parse(s);
        int numberSloths = n.Count;
        int i = 0;
        int currentSloth;
        for (; i < numberSloths / 4; i++){
            for(int j = 0; j <4; j++){
                Button b = Instantiate(button);
                currentSloth = (4 * i + j);
                b.name = "button" + n[currentSloth.ToString()]["type"];
                b.GetComponent<Image>().sprite = Resources.Load<Sprite>(n[currentSloth.ToString()]["photo"]);
                //Meter el button en el canvas
                b.transform.parent = canvas.transform;
                RectTransform rectTransform = b.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 3, width * matrixWidth * 0.25f * 0.2f * 3);
                rectTransform.position = new Vector2(width * matrixWidth * 0.25f * 0.5f + j * width * matrixWidth * 0.25f, height -(width * matrixWidth * 0.25f * 0.5f + i * width * matrixWidth * 0.25f));

            }
        }

        for(int j = 0; j < numberSloths % 4; j++){
            Button b = Instantiate(button);
            currentSloth = (4 * i + j);
            b.name = "button" + n[currentSloth.ToString()]["type"];
            b.GetComponent<Image>().sprite = Resources.Load<Sprite>(n[currentSloth.ToString()]["photo"]);
            //Meter el button en el canvas
            b.transform.parent = canvas.transform;
            RectTransform rectTransform = b.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width * matrixWidth * 0.25f * 0.2f * 3, width * matrixWidth * 0.25f * 0.2f * 3);
            rectTransform.position = new Vector2(width * matrixWidth * 0.25f * 0.5f + j * width * matrixWidth * 0.25f, height - (width * matrixWidth * 0.25f * 0.5f + i * width * matrixWidth * 0.25f));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
