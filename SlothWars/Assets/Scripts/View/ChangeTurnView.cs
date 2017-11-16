﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTurnView: MonoBehaviour {

    private ChangeTurnModel changeTurnModel;
    private bool endTurnOfPlayer,beginStopped;
    private int playerTurn1, playerTurn2;
    private List<GameObject> playersTeam1, playersTeam2;
    private Button endTurnButton;

    private void Start()
    {
        changeTurnModel = new ChangeTurnModel();
        endTurnButton = changeTurnModel.GetEndTurnButton();
        playersTeam1 = new List<GameObject>();
        playersTeam2 = new List<GameObject>();
        
    }
    private void Update()
    {
        if (playersTeam1.Count == 0) {
            UpdateSlothTeams();
            FinishTurn();
        } 
        FixedBugs();
    }
     
    //Method to update the teams.
    private void UpdateSlothTeams()
    {
        foreach(GameObject player in changeTurnModel.GetTeam1())
        {
            playersTeam1.Add(player);
        }
        foreach(GameObject player in changeTurnModel.GetTeam2())
        {
            playersTeam2.Add(player);
        }
    }

    // Function in order to change turns.
    // TODO: MIRAR CON ADRIA LOS METODOS PARA ACTIVAR Y DESACTIVAR MOVIMIENTO
    public void FinishTurn()
    {
        playerTurn1 = changeTurnModel.GetTurnPlayer1();
        playerTurn2 = changeTurnModel.GetTurnPlayer2();

        beginStopped = changeTurnModel.GetBeginStopped();
        endTurnOfPlayer = changeTurnModel.GetEndTurnOfPlayer();
        print(endTurnOfPlayer);
        // If he has ended, he will press the button, changing the variable to true.
        if (beginStopped)
        {
            print("Turno Equipo Azul " + playerTurn1);
            changeTurnModel.DeactivateAllExceptOne(playersTeam1, playersTeam2);
            return;
        }

        if (endTurnOfPlayer)
        {
            print("Turno Equipo Azul " + playerTurn1);
            changeTurnModel.DeactivateSloth(playersTeam2[playerTurn2]);
            changeTurnModel.ActivateSloth(playersTeam1[playerTurn1]);
        }
        else
        {
            print("Turno Equipo Rojo " + playerTurn2);
            changeTurnModel.DeactivateSloth(playersTeam1[playerTurn1]);
            changeTurnModel.ActivateSloth(playersTeam2[playerTurn2]);

        }
    }

    private void FixedBugs()
    {
        // if a sloth is walking, the user cannot end the turn (Disable the end turn button)
        
        if (changeTurnModel.GetTeam1()[playerTurn1].GetComponent<AnimPlayer>().GetMove() || changeTurnModel.GetTeam2()[playerTurn2].GetComponent<AnimPlayer>().GetMove())
        {
            endTurnButton.interactable = false;
        }
        else
        {
            endTurnButton.interactable = true;
        }

        // if a sloth is shooting, the user cannot end the turn (Disable the end turn button)
        if ((changeTurnModel.GetTeam1()[playerTurn1].GetComponent<ShotScript>().GetShotLoad() || changeTurnModel.GetTeam2()[playerTurn2].GetComponent<ShotScript>().GetShotLoad()))
        {
            endTurnButton.interactable = false;
        }
        else
        {
            endTurnButton.interactable = true;
        }
    }

}
