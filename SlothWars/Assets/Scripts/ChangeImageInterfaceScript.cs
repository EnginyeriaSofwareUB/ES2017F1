using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageInterfaceScript : MonoBehaviour {

    private Image slothImageSelected, image;

    private int slothSelected = 0;
    private bool isSlothTurnA, firstImageBeginGame;

    private GameObject managerTeam, previousSceneBehaviour;

	// Use this for initialization
    // Getting the image script from the scene (if there are more images, we have to change the number 1)
    // Getting the gameObject sceneBehaviour from previous scene
	void Start () {
       
		image = GameObject.Find ("slothImage").GetComponent<Image> ();
        managerTeam = GameObject.Find("managerTeam");
        previousSceneBehaviour = GameObject.Find("sceneBehaviour");

        // To show the image of the first sloth when the game starts.
        if (managerTeam.GetComponent<ChangeTurn>().beginStopped)
        {
            slothImageSelected = previousSceneBehaviour.GetComponent<TeamSelection>().team1SlothImages[slothSelected];
            ShowImage();
        }
  
    }
	
	// Update is called once per frame. 
    // Getting the images from the scene before in order to have it ordered by players' preselection.
	void Update () {
		// ----> THIS SHOULD NOT BE DONE IN UPDATE <---- 
		// Change the images only when it is needed, not always.
        isSlothTurnA = managerTeam.GetComponent<ChangeTurn>().isSlothTurnA;
        if (!isSlothTurnA)
        {
            slothSelected = managerTeam.GetComponent<ChangeTurn>().slothTurnA;
            slothImageSelected = previousSceneBehaviour.GetComponent<TeamSelection>().team1SlothImages[slothSelected];

        }
        else
        {
            slothSelected = managerTeam.GetComponent<ChangeTurn>().slothTurnB;
            slothImageSelected = previousSceneBehaviour.GetComponent<TeamSelection>().team2SlothImages[slothSelected];

        }
    }

    // Method to show the image in the scene. Called by end turn Button in order to show the image corresponding to the sloth's turn.
    public void ShowImage()
    {
        image.sprite = slothImageSelected.sprite;
    }
}
