using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurnModel
{
    private Button endTurnButton;
    private List<GameObject> teamSloths1 = new List<GameObject>();
    private List<GameObject> teamSloths2 = new List<GameObject>();
    private bool endTurnOfPlayer;
    private bool endTurnButtonStatus;
    private bool beginStopped;
    private int turnPlayer1 = 0;
    private int turnPlayer2 = 0;

    public ChangeTurnModel() {}
  
    public void SetEndTurnButton(Button endTurnButtonController) { endTurnButton = endTurnButtonController; }
    public Button GetEndTurnButton() { return endTurnButton; }
    // Getters and Setters.
    public bool GetEndTurnButtonStatus() { return endTurnButtonStatus; }
    public void SetEndTurnButtonStatus(bool endTurnButtonController) { endTurnButtonStatus = endTurnButtonController; }

    public bool GetBeginStopped() { return beginStopped; }
    public void SetBeginStopped(bool beginStoppedController) { beginStopped = beginStoppedController; }

    public List<GameObject> GetTeam1() { return teamSloths1; }
    public List<GameObject> GetTeam2() { return teamSloths2; }

    public void SetTeams(List<GameObject> playerControllerTeam1, List<GameObject> playerControllerTeam2)
    {
        foreach (GameObject player in playerControllerTeam1) { teamSloths1.Add(player); }
        foreach (GameObject player in playerControllerTeam2) { teamSloths2.Add(player); }
        
    }

    public bool GetEndTurnOfPlayer() { return endTurnOfPlayer; }
    public void SetEndTurnOfPlayer(bool endTurnOfPlayerController) { endTurnOfPlayer = endTurnOfPlayerController; }
    
    public int GetTurnPlayer1() { return turnPlayer1; }
    public int GetTurnPlayer2() { return turnPlayer2; }    

    public void SetTurnPlayers(int turnControllerPlayer1, int turnControllerPlayer2)
    {
        turnPlayer1 = turnControllerPlayer1;
        turnPlayer2 = turnControllerPlayer2;
    }


    // Functions to activate and deactivate sloth's animations
    public void ActivateSloth(GameObject slothTeam)
    {
        
        slothTeam.GetComponent<Animator>().enabled = true;
        slothTeam.GetComponent<AnimPlayer>().enabled = true;
        slothTeam.GetComponent<ShotScript>().enabled = true;
        slothTeam.GetComponent<ShotScript>().Active(true);
        

    }

    public void DeactivateSloth(GameObject slothTeam)
    {

        slothTeam.GetComponent<Animator>().enabled = false;
        slothTeam.GetComponent<AnimPlayer>().enabled = false;
        slothTeam.GetComponent<ShotScript>().Active(false);
        slothTeam.GetComponent<ShotScript>().enabled = false;
        
    }
   

        // Method in order to have only one sloth active.        
        public void DeactivateAllExceptOne(List<GameObject> slothTeamA, List<GameObject> slothTeamB)
        {
        
            foreach (GameObject player in slothTeamB)
            {
                player.GetComponent<Animator>().enabled = false;
                player.GetComponent<AnimPlayer>().enabled = false;
                player.GetComponent<ShotScript>().Active(false); //turn off de canvas UI
                player.GetComponent<ShotScript>().enabled = false;
            }

            for (int i = 1; i < slothTeamA.Count; i++)
            {
                slothTeamA[i].GetComponent<Animator>().enabled = false;
                slothTeamA[i].GetComponent<AnimPlayer>().enabled = false;
                slothTeamA[i].GetComponent<ShotScript>().Active(false);
                slothTeamA[i].GetComponent<ShotScript>().enabled = false;
            }
        
        }

}
