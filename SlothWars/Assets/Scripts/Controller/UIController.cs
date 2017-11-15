using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : ControllerSingleton<MonoBehaviour> {

    private GameController gameController;

    private bool isPause, setActivePanelOpts, setActivePanelMain;
    private Image panelOpts, panelMain;

    //Static variables for Update
    private static int i = 0;
    private static bool isSet = false;

    //Get UIModel Constructor.
    private UIModel uiModel;

	// Use this for initialization
	private void Start () {
        uiModel = new UIModel();

        InitializeUIControllerVariables();

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


    public void SetPause()
    {
        isSet = true;
        setActivePanelOpts = false;
        uiModel.SetStatePanelOpts(setActivePanelOpts);
    }

    private void InitializeUIControllerVariables()
    {
        isPause = gameController.isPause;
        setActivePanelOpts = gameController.setActivePanelOpts;
        setActivePanelMain = gameController.setActivePanelMain;
        panelOpts = gameController.panelOpts;
        panelMain = gameController.panelMain;
    }

}
