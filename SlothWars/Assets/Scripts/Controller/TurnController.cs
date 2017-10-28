using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Events;

//TurnController: Created as a controller for changing turns and changing images during the game.
public class TurnController: GameController{
   
    //Get an instance of ChangeTurnModel in order to set the values updated by the user. 
    private ChangeTurnModel changeTurnModel;

    //Get an instance of ChangeImageModel in order to set the values updated by the user.
    private ChangeImageModel changeImageModel;
    
    //GameObjects from the scene.
    private static Button endTurnButton;
    private static Image imageFromPreviousScene;

    //Parametres need to change the values in the view.
    private bool isButtonPressed;
    private static bool endTurnOfPlayer;
    private static bool beginStopped;
    private static int turnPlayer1;
    private static int turnPlayer2;

    
    private void Awake()
    {

        isButtonPressed = false;
        //At the beginning of the game, both turnPlayer1 and turnPlayer2 begin with 0 value.
        turnPlayer1 = 0;
        turnPlayer2 = 0;

        changeImageModel = new ChangeImageModel();
        changeTurnModel = new ChangeTurnModel();

        //At the beginning, we set these booleans value to true (To establish that the game has not started yet).
        beginStopped = true;
        endTurnOfPlayer = true;


        //Get an instance of the end Turn Button in the scene.
        endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
        
        //Escoge la primera image (En este caso SlothImage) COMPROBAR
        //Get an instance of the sloth Image in the scene.
        imageFromPreviousScene = GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Image>()[1];
        
    }

    // Use this for initialization
    private void Start () {
        //We comunicate to View (By setting the beginStopped value in Model) that the game is beginning (=> beginStopped=true).
        changeTurnModel.SetBeginStopped(beginStopped);
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);

        //Set the player teams in Model in order to get captured by view and show them in the scene.
        changeTurnModel.SetPlayerTeams(GetPlayerTeam(1), GetPlayerTeam(2));

        //When the game starts, we got the image of the first sloth in team1 (corresponding to the one who starts playing (by default))
        imageFromPreviousScene = GetTeamSelectionGameObject().GetComponent<TeamSelection>().team1SlothImages[0];

        //We send to View (via Model), the image selected.
        changeImageModel.SetImage(imageFromPreviousScene);

        //Set the turns for team1 and team2.
        changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);
    }

    private void Update()
    {
        if (isButtonPressed)
        {
            beginStopped = false;
            //We comunicate to the View that the game starts.
            changeTurnModel.SetBeginStopped(beginStopped);

            //We change the new values in Model in order to comunicate to View and show the results.
            changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);
            changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);

            /////////////*****//////////////
            //TO FIX BUGS

            // if a sloth is walking, the user cannot end the turn (Disable the end turn button)
            if (GetPlayerTeam(1)[turnPlayer1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk") || GetPlayerTeam(2)[turnPlayer2].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                changeTurnModel.SetEndTurnButton(false);
            }
            else
            {
                changeTurnModel.SetEndTurnButton(true);
            }

            // if a sloth is shooting, the user cannot end the turn (Disable the end turn button)
            if ((GetPlayerTeam(1)[turnPlayer1].GetComponent<ShotScript>().GetShotLoad() || GetPlayerTeam(2)[turnPlayer2].GetComponent<ShotScript>().GetShotLoad()))
            {
                changeTurnModel.SetEndTurnButton(false);
            }
            else
            {
                changeTurnModel.SetEndTurnButton(true);
            }
        }
        /////////////*****//////////////
    }
   
    //Method to update the values from turnPlayer1 and turnPlayer2.
    public void FinishTurnOfPlayer()
    {
        if (turnPlayer1 != (GetPlayerTeam(1).Count-1) && turnPlayer2 != (GetPlayerTeam(2).Count-1))
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
            if(turnPlayer1 == GetPlayerTeam(1).Count-1) { turnPlayer1 = 0; changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2); return; }
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
            imageFromPreviousScene = GetTeamSelectionGameObject().GetComponent<TeamSelection>().team1SlothImages[turnPlayer1];
            changeImageModel.SetImage(imageFromPreviousScene);
        }
        else
        {
            imageFromPreviousScene = GetTeamSelectionGameObject().GetComponent<TeamSelection>().team2SlothImages[turnPlayer2];
            changeImageModel.SetImage(imageFromPreviousScene);
        }

    }

    public void SetPressedButton()
    {
        isButtonPressed = true;
    }
}
