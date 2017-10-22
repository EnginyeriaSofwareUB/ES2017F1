using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageInterfaceScript : MonoBehaviour {

    private Image slothImageSelected, image;

    private int slothSelected;
    private bool isSlothTurnA, isSlothTurnB;

    private GameObject managerTeam, previousSceneBehaviour;

	// Use this for initialization
	void Start () {
       
        image = GameObject.Find("Interface").GetComponentsInChildren<Image>()[1];
        managerTeam = GameObject.Find("managerTeam");
        previousSceneBehaviour = GameObject.Find("sceneBehaviour");
  
    }
	
	// Update is called once per frame
	void Update () {
        isSlothTurnA = managerTeam.GetComponent<ChangeTurn>().isSlothTurnA;
        if (!isSlothTurnA)
        {
            slothSelected = managerTeam.GetComponent<ChangeTurn>().slothTurnA;
            print("Soy el sloth " + slothSelected + " del equipo A");
            slothImageSelected = previousSceneBehaviour.GetComponent<TeamSelection>().team1SlothImages[slothSelected];

        }
        else
        {

            slothSelected = managerTeam.GetComponent<ChangeTurn>().slothTurnB;
            print("Soy el sloth " + slothSelected + " del equipo B");
            slothImageSelected = previousSceneBehaviour.GetComponent<TeamSelection>().team2SlothImages[slothSelected];

        }
    }

    public void ShowImage()
    {
        image.sprite = slothImageSelected.sprite;
    }
}
