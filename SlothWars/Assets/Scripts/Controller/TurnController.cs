using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Events;

//TurnController: Created as a controller for changing turns and changing images during the game.
public class TurnController: GameController{

    //Get an instance of ChangeTurnModel in order to set the values updated by the user. 
    private static ChangeTurnModel changeTurnModel;

    //Get an instance of ChangeImageModel in order to set the values updated by the user.
    private static ChangeImageModel changeImageModel;

    // Use this for initialization
    private void OnEnable () {
        print("CONT TURN");
        changeImageModel = new ChangeImageModel();
        changeTurnModel = new ChangeTurnModel();
        changeTurnModel.SetEndTurnButton(endTurnButton);

        //We comunicate to View (By setting the beginStopped value in Model) that the game is beginning (=> beginStopped=true).
        changeTurnModel.SetBeginStopped(beginStopped);
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);

        //Set the player teams in Model in order to get captured by view and show them in the scene.
        //Set the turns for team1 and team2.
        changeTurnModel.SetTeams(teamSloths1, teamSloths2);
        changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);

        //When the game starts, we got the image of the first sloth in team1 (corresponding to the one who starts playing (by default))
        //Need to put it on Start because team1SlothImages and team2SlothImages are captured in the Awake method of GameController.
        //We send to View (via Model), the image selected.
        changeImageModel.SetSprite(spriteFromPreviousScene);
       
    }

    //Method to update the values from turnPlayer1 and turnPlayer2.
    public void FinishTurnOfPlayer()
    {
        
        if (turnPlayer1 != (teamSloths1.Count-1) && turnPlayer2 != (teamSloths2.Count-1))
        {
            if (turnPlayer1 - turnPlayer2 == 1) { turnPlayer2 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            if (turnPlayer1 - turnPlayer2 == -1) { turnPlayer1 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            if (turnPlayer1 == turnPlayer2) { turnPlayer1 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            if (turnPlayer1 - turnPlayer2 > 1) { turnPlayer2 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            if (turnPlayer1 - turnPlayer2 < -1) { turnPlayer1 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
        }
        else
        {
            if (turnPlayer1 - turnPlayer2 == 1) { turnPlayer2 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            if(turnPlayer1 == teamSloths1.Count-1) { turnPlayer1 = 0; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
            else { turnPlayer2 = 0; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
        }
    }

    //Method to update the bool endTurnOfPlayer.
    public void SetEndTurnOfPlayer()
    {
        
        endTurnOfPlayer = !endTurnOfPlayer;
    }

    //Method to update the image selected.
    public void GetSelectedPlayerImage()
    {
        if (turnPlayer1 > turnPlayer2)
        {
            spriteFromPreviousScene = teamSprite1[turnPlayer1];
            changeImageModel.SetSprite(spriteFromPreviousScene);
        }
        else
        {
            spriteFromPreviousScene = teamSprite2[turnPlayer2];
            changeImageModel.SetSprite(spriteFromPreviousScene);
        }

    }

    //Method to detect when the button is being pressed.
    public void SetPressedButton()
    {
        beginStopped = false;

        //We comunicate to the View that the game starts.
        changeTurnModel.SetBeginStopped(beginStopped);

        //We change the new values in Model in order to comunicate to View and show the results.
        changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);


        //To fix possible bugs in changing turns.
        //Quit comments when animator is integrated
        //FixedBugs();

        //Call the method in order to establish the selected image to show.
        GetSelectedPlayerImage();

    }
    
}
