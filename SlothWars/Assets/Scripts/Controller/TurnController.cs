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

    private static List<Image> team1Image, team2Image;
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


        team1Image = new List<Image>();
        team2Image = new List<Image>();

    }

    // Use this for initialization
    private void Start () {
        //We comunicate to View (By setting the beginStopped value in Model) that the game is beginning (=> beginStopped=true).
        changeTurnModel.SetBeginStopped(beginStopped);
        changeTurnModel.SetEndTurnOfPlayer(endTurnOfPlayer);

        //Set the player teams in Model in order to get captured by view and show them in the scene.
        //Set the turns for team1 and team2.
        changeTurnModel.SetPlayerTeams(GetPlayerTeam(1), GetPlayerTeam(2));
        changeTurnModel.SetTurnPlayers(turnPlayer1, turnPlayer2);

        //When the game starts, we got the image of the first sloth in team1 (corresponding to the one who starts playing (by default))
        //Need to put it on Start because team1SlothImages and team2SlothImages are captured in the Awake method of GameController.
        //We send to View (via Model), the image selected.
        if(GetTeamImage1().Contains(null))
        {
            print("Problemon con GameController");
        }
        imageFromPreviousScene = GetTeamImage1()[0];
        print(GetTeamSelectionGameObject().GetComponent<TeamSelection>().team1SlothImages.Contains(null));
        if(imageFromPreviousScene == null)
        {
            print("Problema con GameController");
        }
        changeImageModel.SetImage(imageFromPreviousScene);

        
        
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

            
            //To fix possible bugs in changing turns.
            FixedBugs();

            //Call the method in order to establish the selected image to show.
            GetSelectedPlayerImage();

            //Set the bool to false in order to show that the actions done while pressing the button have ended and persisting during time
            //til an user pressed it again.
            isButtonPressed = false;
        }
        
    }
   
    private void FixedBugs()
    {
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
            imageFromPreviousScene = GetTeamImage1()[turnPlayer1];
            changeImageModel.SetImage(imageFromPreviousScene);
        }
        else
        {
            imageFromPreviousScene = GetTeamImage2()[turnPlayer2];
            changeImageModel.SetImage(imageFromPreviousScene);
        }

    }

    //Method to detect when the button is being pressed.
    public void SetPressedButton()
    {
        print(isButtonPressed);
       
        isButtonPressed = true;
    }
}
