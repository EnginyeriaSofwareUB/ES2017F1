using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurnModel
{

    private static List<Player> playerTeam1 = new List<Player>();
    private static List<Player> playerTeam2 = new List<Player>();
    private static bool endTurnOfPlayer;
    private static bool endTurnButton;
    private static bool beginStopped;
    private static int turnPlayer1 = 0;
    private static int turnPlayer2 = 0;

    public ChangeTurnModel() {}
  
    // Getters and Setters.
    public bool GetEndTurnButton() { return endTurnButton; }
    public void SetEndTurnButton(bool endTurnButtonController) { endTurnButton = endTurnButtonController; }

    public bool GetBeginStopped() { return beginStopped; }
    public void SetBeginStopped(bool beginStoppedController) { beginStopped = beginStoppedController; }

    public List<Player> GetTeam1() { return playerTeam1; }
    public List<Player> GetTeam2() { return playerTeam2; }

    /*
    public void SetPlayerTeam1(List<Player> playerTeam1) { foreach (Player player in playerTeam1) { this.playerTeam1.Add(player); } }
    public void SetPlayerTeam2(List<Player> playerTeam2) { foreach (Player player in playerTeam2) { this.playerTeam2.Add(player); } }
    */
    public void SetPlayerTeams(List<Player> playerControllerTeam1, List<Player> playerControllerTeam2)
    {
        foreach (Player player in playerControllerTeam1) { playerTeam1.Add(player); }
        foreach (Player player in playerControllerTeam2) { playerTeam2.Add(player); }
        
    }

    public bool GetEndTurnOfPlayer() { return endTurnOfPlayer; }
    public void SetEndTurnOfPlayer(bool endTurnOfPlayerController) { endTurnOfPlayer = endTurnOfPlayerController; }
    
    public int GetTurnPlayer1() { return turnPlayer1; }
    public int GetTurnPlayer2() { return turnPlayer2; }    
    /*
    public void SetTurnPlayer1(int turnPlayer1) { this.turnPlayer1 = turnPlayer1; }
    public void SetTurnPlayer2(int turnPlayer2) { this.turnPlayer2 = turnPlayer2; }
    */
    public void SetTurnPlayers(int turnControllerPlayer1, int turnControllerPlayer2)
    {
        turnPlayer1 = turnControllerPlayer1;
        turnPlayer2 = turnControllerPlayer2;
    }


    // Functions to activate and deactivate sloth's animations
    public void ActivateSloth(Player slothTeam)
    {
        slothTeam.GetComponent<Animator>().enabled = true;
        slothTeam.GetComponent<Player>().enabled = true;
        slothTeam.GetComponent<ShotScript>().enabled = true;
        slothTeam.GetComponent<ShotScript>().Active(true);

    }

    public void DeactivateSloth(Player slothTeam)
    {
        
        slothTeam.GetComponent<Animator>().enabled = false;
        slothTeam.GetComponent<Player>().enabled = false;
        slothTeam.GetComponent<ShotScript>().Active(false);
        slothTeam.GetComponent<ShotScript>().enabled = false;

    }

    // Method in order to have only one sloth active.
    public void DeactivateAllExceptOne(List<Player> slothTeamA, List<Player> slothTeamB)
    {
        foreach (Player player in slothTeamB)
        {
            player.GetComponent<Animator>().enabled = false;
            player.GetComponent<Player>().enabled = false;
            player.GetComponent<ShotScript>().Active(false); //turn off de canvas UI
            player.GetComponent<ShotScript>().enabled = false;
        }

        for (int i = 1; i < slothTeamA.Count; i++)
        {
            slothTeamA[i].GetComponent<Animator>().enabled = false;
            slothTeamA[i].GetComponent<Player>().enabled = false;
            slothTeamA[i].GetComponent<ShotScript>().Active(false);
            slothTeamA[i].GetComponent<ShotScript>().enabled = false;
        }

    }
    

}
