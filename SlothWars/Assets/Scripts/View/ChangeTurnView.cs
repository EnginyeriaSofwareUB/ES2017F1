using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeTurnView: MonoBehaviour {

    private ChangeTurnModel changeTurnModel;
    private bool endTurnOfPlayer,beginStopped;
    private int playerTurn1, playerTurn2;
    private List<Player> playersTeam1, playersTeam2;

    private void Awake()
    {
        changeTurnModel = new ChangeTurnModel();

    }
    private void Start()
    {
        playersTeam1 = new List<Player>();
        playersTeam2 = new List<Player>();

    }
    private void Update()
    {
        if (playersTeam1.Count == 0) {
            UpdateSlothTeams();
        }

        //Getting the information from Model.
        playerTurn1 = changeTurnModel.GetTurnPlayer1();
        playerTurn2 = changeTurnModel.GetTurnPlayer2();

        beginStopped = changeTurnModel.GetBeginStopped();
        endTurnOfPlayer = changeTurnModel.GetEndTurnOfPlayer();

        FinishTurn();
    }

    //Method to update the teams.
    private void UpdateSlothTeams()
    {
        foreach(Player player in changeTurnModel.GetTeam1())
        {
            playersTeam1.Add(player);
        }
        foreach(Player player in changeTurnModel.GetTeam2())
        {
            playersTeam2.Add(player);
        }
    }

    // Function in order to change turns.
    public void FinishTurn()
    {

        // If he has ended, he will press the button, changing the variable to true.
        if (beginStopped)
        {
            changeTurnModel.DeactivateAllExceptOne(playersTeam1, playersTeam2);
        }
        if (endTurnOfPlayer)
        {
            changeTurnModel.DeactivateSloth(playersTeam2[playerTurn2]);
            changeTurnModel.ActivateSloth(playersTeam1[playerTurn1]);
        }
        else
        {
            changeTurnModel.DeactivateSloth(playersTeam1[playerTurn1]);
            changeTurnModel.ActivateSloth(playersTeam2[playerTurn2]);

        }
    }
}
