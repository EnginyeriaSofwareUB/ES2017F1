using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//TurnController: Created as a controller for changing turns and changing images during the game.
public class TurnController{
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
    protected  TurnController()
    {

    }
    ///////*****///////

    //Get an instance of ChangeTurnModel in order to set the values updated by the user. 
    private List<GameObject> newTeamSloths1, newTeamSloths2;
    private List<Sprite> teamSprite1;
    private List<Sprite> teamSprite2;
    private int turnPlayer1, turnPlayer2;
    private Text turnLabel;
    private bool beginStopped, endTurnOfPlayer, isButtonPressed;
    private Sprite spriteFromPreviousScene;

    //Get an instance of ChangeImageModel in order to set the values updated by the user.
    private ChangeImageModel changeImageModel;
    private ChangeTurnModel changeTurnModel;
    private static Button endTurnButton;

    // Use this for initialization
    public void StartTurns () {

        beginStopped = true;
        isButtonPressed = false;
        endTurnOfPlayer = true;
        turnPlayer1 = 0;
        turnPlayer2 = 0;
        //We start with the sprite of the first sloth in blue team, being the one who starts the game
        //See DeactivateAllExceptOne() in ChangeTurnModel.cs
        spriteFromPreviousScene = teamSprite1[0];

        changeImageModel = ChangeImageModel.Instance;
        changeTurnModel = ChangeTurnModel.Instance;

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
        changeImageModel.SendSlothInfo(StorePersistentVariables.Instance.slothTeam1[0].GetTypeName());

        endTurnButton.onClick.AddListener(delegate { FinishTurnOfPlayer(); });
        endTurnButton.onClick.AddListener(delegate { SetEndTurnOfPlayer(); });
        endTurnButton.onClick.AddListener(delegate { SetPressedButton(); });

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

        //Call the method in order to establish the selected image to show.
        GetSelectedPlayerImage();

    }

    //Method to update the image selected when the button is pressed.
    public void GetSelectedPlayerImage()
    {
        //NO BORRAR
        Debug.Log(turnPlayer1);
        Debug.Log(turnPlayer2);
        if (turnPlayer1 > turnPlayer2)
        {
            spriteFromPreviousScene = teamSprite2[turnPlayer2];
            changeImageModel.SetSprite(spriteFromPreviousScene);
            changeImageModel.SendSlothInfo(StorePersistentVariables.Instance.slothTeam1[turnPlayer2].GetTypeName());
        }
        else 
        {
            if (turnPlayer1 == turnPlayer2)
            {
                spriteFromPreviousScene = teamSprite1[turnPlayer1];
                changeImageModel.SetSprite(spriteFromPreviousScene);
                changeImageModel.SendSlothInfo(StorePersistentVariables.Instance.slothTeam1[turnPlayer1].GetTypeName());
            } else if (turnPlayer2 - turnPlayer1 < -1)
            {
                spriteFromPreviousScene = teamSprite2[turnPlayer2];
                changeImageModel.SetSprite(spriteFromPreviousScene);
                changeImageModel.SendSlothInfo(StorePersistentVariables.Instance.slothTeam2[turnPlayer2].GetTypeName());
            }
        }

    }
    public GameObject GetActiveSloth()
    {
        if (turnPlayer1 == 0 && turnPlayer2 == 0) { return newTeamSloths1[turnPlayer1]; }
        else if (turnPlayer1 == turnPlayer2) { return newTeamSloths1[turnPlayer1]; }
        else if (turnPlayer2 -turnPlayer1<-1){ return newTeamSloths1[turnPlayer2]; }
        else { return newTeamSloths2[turnPlayer2]; }
    }

    public void SetTeamSprite1(List<Sprite> spriteTeamCont)
    {
        teamSprite1 = new List<Sprite>();
        teamSprite1 = spriteTeamCont;
    }

    public void SetTeamSprite2(List<Sprite> spriteTeamCont)
    {
        teamSprite2 = new List<Sprite>();
        teamSprite2 = spriteTeamCont;
    }

    public void SetTeamSloths1(List<GameObject> slothTeamCont)
    {

        newTeamSloths1 = new List<GameObject>();
        newTeamSloths1 = slothTeamCont;
    }

    public void SetTeamSloths2(List<GameObject> slothTeamCont)
    {
        newTeamSloths2 = new List<GameObject>();
        newTeamSloths2 = slothTeamCont;
    }

    public void SetEndTurnButton(Button endTurnButtonCont)
    {
        endTurnButton = endTurnButtonCont;
    }

    public void SetTurnLabel(Text labelTextCont)
    {
        turnLabel = labelTextCont;
    }
}
