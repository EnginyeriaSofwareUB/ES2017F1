using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

public class GameController: ControllerSingleton<MonoBehaviour>{

    protected GameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    ///////*****///////

    //TURNCONTROLLER VARIABLES
    public List<Player> playerTeam1, playerTeam2;

    public List<Sprite> teamSprite1, teamSprite2;
    public Button endTurnButton;
    public Sprite spriteFromPreviousScene;

    public bool isButtonPressed;

    public bool endTurnOfPlayer;
    public bool beginStopped;
    public int turnPlayer1;
    public int turnPlayer2;

    //LOGICCONTROLLER VARIABLES
    public List<Button> listAbilities;
    public Button firstAbility, secondAbility, thirdAbility;

    //Used in SetUp too.
    public List<GameObject> teamSloths1, teamSloths2;

    //UICONTROLLER VARIABLES
    public Image panelMain;
    public Image panelOpts;
    public bool setActivePanelOpts = false;
    public bool setActivePanelMain = true;
    public bool isPause = false;


    ///////*****///////

    private void Awake()
    {
        InitializePlayer();
        InitializeTurnVariables();
        InitializeLogicVariables();
        InitializeUIVariables();

    }

    private void InitializePlayer()
    {
        GameObject.Find("GameController").GetComponent<Player>().enabled = false;
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
        firstAbility = GameObject.Find("buttonAbility1").GetComponent<Button>();
        secondAbility = GameObject.Find("buttonAbility2").GetComponent<Button>();
        thirdAbility = GameObject.Find("buttonAbility3").GetComponent<Button>();

        listAbilities.Add(firstAbility);
        listAbilities.Add(secondAbility);
        listAbilities.Add(thirdAbility);

        teamSloths1 = new List<GameObject>();
        teamSloths2 = new List<GameObject>();
    }

    private void InitializeUIVariables()
    {
        panelMain = GameObject.Find("MainUIPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionPanel").GetComponent<Image>();

        //panelMain init Elements

        //panelOpts init Elements
        

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
