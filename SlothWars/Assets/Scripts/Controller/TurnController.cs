using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Events;

//TurnController: Created as a controller for changing turns and changing images during the game.
public class TurnController: MonoBehaviour{
    //SINGLETON
    private static TurnController instance;
    public static TurnController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TurnController();
            }
            return instance;
        }
    }
    protected  TurnController() { }
    ///////*****///////

    //Get an instance of ChangeTurnModel in order to set the values updated by the user. 
    private static ChangeTurnModel changeTurnModel;
    private static List<GameObject> newTeamSloths1, newTeamSloths2;
    private static List<Sprite> teamSprite1 = new List<Sprite>();
    private static List<Sprite> teamSprite2 = new List<Sprite>();
    private static int turnPlayer1, turnPlayer2;
    private static Text turnLabel;
    private static bool beginStopped, endTurnOfPlayer, isButtonPressed;
    private static Sprite spriteFromPreviousScene;

    //Get an instance of ChangeImageModel in order to set the values updated by the user.
    private static ChangeImageModel changeImageModel;
    private static Button endTurnButton;

    public TurnController(List<GameObject> teamSloths1, List<GameObject> teamSloths2, List<Sprite> teamSprite1Cont, List<Sprite> teamSprite2Cont, Button endTurnButtonCont, Text turnLabelCont)
    {
        beginStopped = true;
        isButtonPressed = false;
        endTurnOfPlayer = true;
        turnPlayer1 = 0;
        turnPlayer2 = 0;
        teamSprite1 = teamSprite1Cont;
        teamSprite2 = teamSprite2Cont;
        newTeamSloths1 = teamSloths1;
        newTeamSloths2 = teamSloths2;
        endTurnButton = endTurnButtonCont;
        turnLabel = turnLabelCont;
        //We start with the sprite of the first sloth in blue team, being the one who starts the game
        //See DeactivateAllExceptOne() in ChangeTurnModel.cs
        spriteFromPreviousScene = teamSprite1[0];
    }
    // Use this for initialization
    private void OnEnable () {

        changeImageModel = new ChangeImageModel();
        changeTurnModel = new ChangeTurnModel();
        changeTurnModel.SetEndTurnButton(endTurnButton);

        changeTurnModel.SetText(turnLabel);
        //We comunicate to View (By setting the beginStopped value in Model) that the game is beginning (=> beginStopped=true).
        changeTurnModel.SetBeginStopped(beginStopped);
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);

        //Set the player teams in Model in order to get captured by view and show them in the scene.
        //Set the turns for team1 and team2.
        changeTurnModel.SetTeams(newTeamSloths1, newTeamSloths2);
        changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);

        //When the game starts, we got the image of the first sloth in team1 (corresponding to the one who starts playing (by default))
        //Need to put it on Start because team1SlothImages and team2SlothImages are captured in the Awake method of GameController.
        //We send to View (via Model), the image selected.
        changeImageModel.SetSprite(spriteFromPreviousScene);
       
    }

    //Method to update the values from turnPlayer1 and turnPlayer2 when the button is pressed.
    public void FinishTurnOfPlayer()
    {
     
        if (turnPlayer1 != (newTeamSloths1.Count-1) && turnPlayer2 != (newTeamSloths2.Count-1))
        {
            if (turnPlayer1 - turnPlayer2 >= 1) { turnPlayer2 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1,turnPlayer2); return; }
            if (turnPlayer1 - turnPlayer2 <= -1) { turnPlayer1 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1,turnPlayer2); return; }
            if (turnPlayer1 == turnPlayer2) { turnPlayer1 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }

        }
        else
        {
            if (turnPlayer1 - turnPlayer2 == 1) { turnPlayer2 += 1; changeTurnModel.SetTurnPlayers(turnPlayer1,turnPlayer2); return; }
            if (turnPlayer1 == turnPlayer2) { turnPlayer1 = 0; changeTurnModel.SetTurnPlayers(turnPlayer1,turnPlayer2); return; }
            if (turnPlayer1 - turnPlayer2 <= -1) { turnPlayer2 = 0; changeTurnModel.SetTurnPlayers(turnPlayer1,turnPlayer2); return; }
        }
    }

    //Method to update the bool endTurnOfPlayer when the button is pressed.
    public void SetEndTurnOfPlayer()
    {       
        endTurnOfPlayer = !endTurnOfPlayer;
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);
    }

    //Method to detect when the button is pressed.
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

    //Method to update the image selected when the button is pressed.
    public void GetSelectedPlayerImage()
    {
        if (turnPlayer1 > turnPlayer2)
        {
            spriteFromPreviousScene = teamSprite2[turnPlayer2];
            changeImageModel.SetSprite(spriteFromPreviousScene);
        }
        else 
        {
            if (turnPlayer1 == turnPlayer2)
            {
                spriteFromPreviousScene = teamSprite1[turnPlayer1];
                changeImageModel.SetSprite(spriteFromPreviousScene);
            } else if (turnPlayer2 - turnPlayer1 < -1)
            {
                spriteFromPreviousScene = teamSprite1[turnPlayer2];
                changeImageModel.SetSprite(spriteFromPreviousScene);
            }
        }

    }
}
