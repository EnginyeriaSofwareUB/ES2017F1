﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

public class GameController: ControllerSingleton<MonoBehaviour>{

    protected GameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    ///////*****///////
    //GameObjects (sloths) in the scene
    public List<GameObject> teamSloths1, teamSloths2;

    //Player Lists to attach to the gameObjects in the scene.
    public List<AnimPlayer> playerTeam1, playerTeam2;

    //TURNCONTROLLER VARIABLES
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
    
    //UICONTROLLER VARIABLES
    public Image panelMain;
    public Image panelOpts;
    public bool setActivePanelOpts;
    public bool setActivePanelMain;
    public bool isPause;


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
        GameObject.Find("GameController").GetComponent<AnimPlayer>().enabled = false;
    }

    private void InitializeTurnVariables()
    {
        isButtonPressed = false;
        endTurnOfPlayer = true;
        beginStopped = true;
        turnPlayer1 = 0;
        turnPlayer2 = 0;

        playerTeam1 = new List<AnimPlayer>();
        playerTeam2 = new List<AnimPlayer>();
        teamSprite1 = new List<Sprite>();
        teamSprite2 = new List<Sprite>();

        CreateTeamsSetSprites();

        endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();
        GameObject.Find("GameController").GetComponent<TurnController>().enabled = true;

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
        GameObject.Find("GameController").GetComponent<LogicController>().enabled = true;
    }

    private void InitializeUIVariables()
    {
        setActivePanelMain = true;
        setActivePanelOpts = false;
        isPause = false;
        panelMain = GameObject.Find("MainUIPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionPanel").GetComponent<Image>();
        //panelMain init Elements

        //panelOpts init Elements

        GameObject.Find("GameController").GetComponent<UIController>().enabled = true;

    }
		
    private void CreateTeamsSetSprites()
    {

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam1)
        {
            playerTeam1.Add(new AnimPlayer(sloth));
            teamSprite1.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam2)
        {
            playerTeam2.Add(new AnimPlayer(sloth));
            teamSprite2.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        
    }
    
}