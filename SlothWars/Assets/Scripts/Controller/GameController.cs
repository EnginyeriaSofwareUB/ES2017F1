using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour{
    
    ///////*****///////
    
    //TURNCONTROLLER VARIABLES
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

    //LOGICCONTROLLER VARIABLES
    private static List<Button> listAbilities;
    private static Button firstAbility, secondAbility, thirdAbility;

    //UICONTROLLER VARIABLES
    public static Image panelMain;
    public static Image panelOpts;
    private static bool isPause = false;
   
    private static Button resumeOpts;
    private static Button exitOpts;

    ///////*****///////

    // Use this for initialization
    private TurnController turnController;
    private LogicController logicController;
    private UIController uiController;

    private void Awake()
    {
        InitializeTurnVariables();
        InitializeLogicVariables();
        InitializeUIVariables();

        turnController = new TurnController(isButtonPressed, turnPlayer1, turnPlayer2, beginStopped, endTurnOfPlayer);
        logicController = new LogicController(listAbilities);
        uiController = new UIController(panelMain, panelOpts, isPause, resumeOpts, exitOpts);

    }

    private void InitializeTurnVariables()
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

    }

    private void InitializeLogicVariables()
    {
        listAbilities = new List<Button>();

        firstAbility = GameObject.Find("buttonAbility1").GetComponent<Button>();
        secondAbility = GameObject.Find("buttonAbility2").GetComponent<Button>();
        thirdAbility = GameObject.Find("buttonAbility3").GetComponent<Button>();

        listAbilities.Add(firstAbility);
        listAbilities.Add(secondAbility);
        listAbilities.Add(thirdAbility);

    }

    private void InitializeUIVariables()
    {
        panelMain = GameObject.Find("MainUIPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionPanel").GetComponent<Image>();

        //panelMain init Elements

        //panelOpts init Elements

        resumeOpts = GameObject.Find("ResumeGame").GetComponent<Button>();
        exitOpts = GameObject.Find("QuitButton").GetComponent<Button>();

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
