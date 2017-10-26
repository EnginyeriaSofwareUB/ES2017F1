using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurn : MonoBehaviour
{
    // Getting the endTurn Button in the scene.
    private GameObject endTurnButton;

    // Getting the sloths in the scene.
    public List<GameObject> slothTeamA;
    public List<GameObject> slothTeamB;
    public bool isSlothTurnA;

    
    //GameObject with the script where the sloths are created
    GameObject managerTeam;

    public int slothTurnA, slothTurnB;

    // Initially, the friendly sloth has not ended his turn.
    public bool endTurnOfSloth = false;
    public bool beginStopped = true;

    private void Start()
    {
        endTurnButton = GameObject.Find("EndTurnButton");
        isSlothTurnA = false;
        // In order to know the different turns.
        slothTurnA = 0;
        slothTurnB = 0;

        slothTeamA = new List<GameObject>();
        slothTeamB = new List<GameObject>();

        managerTeam = GameObject.Find("managerTeam");
    }

    // Getting the sloths in the scene. 
    private void Update()
    {
        
        slothTeamA = managerTeam.GetComponent<CreateSloth>().teamA;
        slothTeamB = managerTeam.GetComponent<CreateSloth>().teamB;

        // if a sloth is walking, the player cannot end the turn (Disable the end turn button)
        if (slothTeamA[slothTurnA].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk") || slothTeamB[slothTurnB].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            endTurnButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            endTurnButton.GetComponent<Button>().interactable = true;
        }

        // if a sloth is shooting, the player cannot end the turn (Disable the end turn button) 
        if ((slothTeamA[slothTurnA].GetComponent<ShotScript>().GetShotLoad() || slothTeamB[slothTurnB].GetComponent<ShotScript>().GetShotLoad()))
        {
            endTurnButton.GetComponent<Button>().interactable = false;
        } else
        {
            endTurnButton.GetComponent<Button>().interactable = true;
        }

        // To have only one sloth moving in the beginning of the game.
        if (beginStopped)
        {
            DeactivateAllExceptOne(slothTeamA, slothTeamB);
            beginStopped = false;
            isSlothTurnA = true;
        }

    }

    // Method in order to have only one sloth active.
    public void DeactivateAllExceptOne(List<GameObject> slothTeamA, List<GameObject> slothTeamB)
    {
        foreach (GameObject sloth in slothTeamB)
        {
            sloth.GetComponent<Animator>().enabled = false;
            sloth.GetComponent<Player>().enabled = false;
            sloth.GetComponent<ShotScript>().Active(false); //turn off de canvas UI
            sloth.GetComponent<ShotScript>().enabled = false;
        }

        for (int i = 1; i<slothTeamA.Count; i++)
        {
            slothTeamA[i].GetComponent<Animator>().enabled = false;
            slothTeamA[i].GetComponent<Player>().enabled = false;
            slothTeamA[i].GetComponent<ShotScript>().Active(false);
            slothTeamA[i].GetComponent<ShotScript>().enabled = false;
        }
    
    }

    // Functions to activate and deactivate sloth's animations
    private void ActivateSloth(GameObject slothTeam)
    {
        slothTeam.GetComponent<Animator>().enabled = true;
        slothTeam.GetComponent<Player>().enabled = true;
        slothTeam.GetComponent<ShotScript>().enabled = true;
        slothTeam.GetComponent<ShotScript>().Active(true);

    }

    private void DeactivateSloth(GameObject slothTeam)
    {
        
        slothTeam.GetComponent<Animator>().enabled = false;
        slothTeam.GetComponent<Player>().enabled = false;
        slothTeam.GetComponent<ShotScript>().Active(false);
        slothTeam.GetComponent<ShotScript>().enabled = false;

    }

    // Function in order to change turns.
    public void FinishTurn()
    {
        // If he has ended, he will press the button, changing the variable to true.
        if (endTurnOfSloth)
        {
            
            endTurnOfSloth = false;
            DeactivateSloth(slothTeamB[slothTurnB]); 
            ActivateSloth(slothTeamA[slothTurnA]);
            isSlothTurnA = true;
        
            slothTurnB += 1;
            if(slothTurnB == slothTeamB.Count)
            {
                slothTurnB = 0;
            }
            
            /* If the variable is already true, it means that it is the turn for the enemy sloth.
            If we arrive here it will mean that the enemy sloth has finished his turn, and 
            it will change the variable to false.*/
        }
        else
        {
            endTurnOfSloth = true;
     
            DeactivateSloth(slothTeamA[slothTurnA]);
            ActivateSloth(slothTeamB[slothTurnB]);
            isSlothTurnA = false;
           
            slothTurnA += 1;
            if (slothTurnA == slothTeamA.Count)
            {
                slothTurnA = 0;
            }

        }
    }

}
