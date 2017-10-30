using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour{
  
    private static List<Player> playerTeam1;
    private static List<Player> playerTeam2;

    private static List<Sprite> teamSprite1, teamSprite2;
    private static Button endTurnButton;
    private static Sprite spriteFromPreviousScene;

    private bool isButtonPressed;

    private static bool endTurnOfPlayer;
    private static bool beginStopped;
    private static int turnPlayer1;
    private static int turnPlayer2;

    // Use this for initialization
    private TurnController turnController;
    private LogicController logicController;

    private void Awake()
    {

        isButtonPressed = false;
        endTurnOfPlayer = true;
        beginStopped = true;
        turnPlayer1 = 0;
        turnPlayer2 = 0;

        playerTeam1 = new List<Player>();
        playerTeam2 = new List<Player>();
        teamSprite1 = new List<Sprite>();
        teamSprite2 = new List<Sprite>();
   
        CreateTeamsSetSprites();

        endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();

        turnController = new TurnController(isButtonPressed, turnPlayer1, turnPlayer2, beginStopped, endTurnOfPlayer);


    }

    public Button GetEndTurnButton()
    {
        return endTurnButton;
    }

    public List<Sprite> GetTeamSprite1()
    {
        return teamSprite1;  
    }

    public List<Sprite> GetTeamSprite2()
    {
        return teamSprite2;
    }


    public List<Player> GetPlayerTeam(int idTeam)
    {
        if (idTeam == 1)
        {
            return playerTeam1;
        }
        return playerTeam2;
    }

    private void CreateTeamsSetSprites()
    {

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam1)
        {
            playerTeam1.Add(new Player(sloth));
            teamSprite1.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam2)
        {
            playerTeam2.Add(new Player(sloth));
            teamSprite2.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        
    }
}
