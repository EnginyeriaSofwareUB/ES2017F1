﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : GameController {

    private static bool setActivePanelOpts, setActivePanelMain;
    private static bool isSet = false;
    //optionsPanel
	private static Button resumeOpts;
	private static Button exitOpts;
    private static int i = 0;
    //Get UIModel Constructor.
    private UIModel uiModel;

	// Use this for initialization
	private void Start () {
        uiModel = new UIModel();

        InitializeBoolVariables();
        
        uiModel.SetStatePanelOpts(setActivePanelOpts);
        uiModel.SetStatePanelMain(setActivePanelMain);

        uiModel.SetPanelOpts(panelOpts);
        uiModel.SetPanelMain(panelMain);

        panelOpts.gameObject.SetActive (setActivePanelOpts);
		panelMain.gameObject.SetActive (setActivePanelMain);

	}
	
	// Update is called once per frame

	private void Update () {

        //pause time if they press escape keyboard 
        if ( Input.GetKeyDown(KeyCode.Escape))
		{

			isPause = !isPause;

           if (isPause) {
                uiModel.SetStatePanelOpts(true);

            } else {
                if (isSet)
                {
                    uiModel.SetStatePanelOpts(true);
                }
                else
                {
                    uiModel.SetStatePanelOpts(false);
                }                
			}
           
        }
        
	}

    private void InitializeBoolVariables()
    {
        isPause = false;
        setActivePanelOpts = false;
        setActivePanelMain = true;

    }

    public void SetPause()
    {
        isSet = true;
        setActivePanelOpts = false;
        uiModel.SetStatePanelOpts(setActivePanelOpts);
    }


}
