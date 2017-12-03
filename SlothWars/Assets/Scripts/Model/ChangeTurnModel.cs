using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurnModel
{    //SINGLETON
    private static ChangeTurnModel instance;

    public static ChangeTurnModel Instance
    {
        get
        {
            if (instance == null)
            {
                //TODO: FIX THIS TO MAKE IT SINGLETON
                instance = new ChangeTurnModel();
            }
            return instance;
        }
    }
    ///////*****///////
    private static Button endTurnButton;
    private static List<GameObject> teamSloths1;
    private static List<GameObject> teamSloths2;
    private static bool endTurnOfPlayer;
    private static bool endTurnButtonStatus;
    private static bool beginStopped;
    private static int turnPlayer1;
    private static int turnPlayer2;
    private static Text turnLabel;
    private static Sloth currentSloth;
    private static int id, currentTurn;
    private static int apCurrentSloth;

    protected ChangeTurnModel() {  }
  
    public void SetEndTurnButton(Button endTurnButtonController) { endTurnButton = endTurnButtonController; }
    public Button GetEndTurnButton() { return endTurnButton; }
    // Getters and Setters.
    public bool GetEndTurnButtonStatus() { return endTurnButtonStatus; }
    public void SetEndTurnButtonStatus(bool endTurnButtonController) { endTurnButtonStatus = endTurnButtonController; }

    public bool GetBeginStopped() { return beginStopped; }
    public void SetBeginStopped(bool beginStoppedController) { beginStopped = beginStoppedController;}

    public List<GameObject> GetTeam1() { Debug.Log("HOLA"); return teamSloths1; }
    public List<GameObject> GetTeam2() { return teamSloths2; }

    public void SetTeams(List<GameObject> playerControllerTeam1, List<GameObject> playerControllerTeam2)
    {
        teamSloths1 = new List<GameObject>();
        teamSloths2 = new List<GameObject>();
        foreach (GameObject player in playerControllerTeam1) { teamSloths1.Add(player); }
        foreach (GameObject player in playerControllerTeam2) { teamSloths2.Add(player); }
        
    }

    public void SetCurrentTurn(int idCont, int currentTurnCont) { id = idCont; currentTurn = currentTurnCont; }
    public int GetId() { return id; }
    public int GetCurrentTurn() { return currentTurn;}

    public bool GetEndTurnOfPlayer() { return endTurnOfPlayer; }
    public void SetEndTurnOfPlayer(bool endTurnOfPlayerController) { endTurnOfPlayer = endTurnOfPlayerController; }
    
    public int GetTurnPlayer1() { return turnPlayer1; }
    public int GetTurnPlayer2() { return turnPlayer2; }    

    public Sloth GetCurrentSloth() { return currentSloth; }
    public void SetCurrentSloth(Sloth sloth) { currentSloth = sloth;}

    public int GetApCurrentSloth() { return apCurrentSloth; }
    public void SetApCurrentSloth(int ap ) { apCurrentSloth = ap;}
    public void DecrementApCurrentSloth(int ap){ apCurrentSloth -= ap;}


    public void SetTurnPlayers(int turnControllerPlayer1, int turnControllerPlayer2)
    {
        turnPlayer1 = turnControllerPlayer1;
        turnPlayer2 = turnControllerPlayer2;
    }

    public void SetTurnPlayer1(int turnControllerPlayer1)
    {
        turnPlayer1 = turnControllerPlayer1;
    }

    public void SetTurnPlayer2(int turnControllerPlayer2)
    {
        turnPlayer2 = turnControllerPlayer2;
    }
    // Functions to activate and deactivate sloth's animations
    public void ActivateSloth(GameObject slothTeam)
    {
        
        slothTeam.GetComponent<Animator>().enabled = true;
        slothTeam.GetComponent<AnimPlayer>().enabled = true;
        slothTeam.GetComponent<ShotScript>().enabled = true;
        slothTeam.GetComponent<SlothSelected>().enabled = true;
        slothTeam.GetComponent<SlothSelected>().Active(true);
        

    }

    public void DeactivateSloth(GameObject slothTeam)
    {
        slothTeam.GetComponent<Animator>().enabled = false;
        slothTeam.GetComponent<AnimPlayer>().enabled = false;
        slothTeam.GetComponent<ShotScript>().enabled = false;
        slothTeam.GetComponent<SlothSelected>().enabled = false;
        slothTeam.GetComponent<SlothSelected>().Active(false);
        
    }
   

        // Method in order to have only one sloth active.        
    public void DeactivateAllExceptOne(List<GameObject> slothTeamA, List<GameObject> slothTeamB)
    {
        foreach (GameObject player in slothTeamB)
        {
            player.GetComponent<Animator>().enabled = false;
            player.GetComponent<AnimPlayer>().enabled = false;
            player.GetComponent<ShotScript>().enabled = false;
            player.GetComponent<SlothSelected>().Active(false);
            player.GetComponent<SlothSelected>().enabled = false;
        }

        for (int i = 1; i < slothTeamA.Count; i++)
        {
            slothTeamA[i].GetComponent<Animator>().enabled = false;
            slothTeamA[i].GetComponent<AnimPlayer>().enabled = false;
            slothTeamA[i].GetComponent<ShotScript>().enabled = false;
            slothTeamA[i].GetComponent<SlothSelected>().Active(false);
            slothTeamA[i].GetComponent<SlothSelected>().enabled = false;

        }
        
    }

    public void SetText(Text label){
        turnLabel = label;

    }

    public Text GetText(){
        return turnLabel;
    }

}
