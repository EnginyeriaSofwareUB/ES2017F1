using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : ControllerSingleton<MonoBehaviour>{


    //Static variables for Update
    private static int i = 0;
    private static bool isSet = false;
    private static Image panelOpts;
    private static Image panelMain;
    private static bool setActivePanelMain, setActivePanelOpts, isPause;
    //Get UIModel Constructor.
    private UIModel uiModel;

    public UIController(Image panelMainCont, Image panelOptsCont)
    {
        panelOpts = panelOptsCont;
        panelMain = panelMainCont;
        setActivePanelMain = true;
        setActivePanelOpts = false;
        isPause = false;
    }
	// Use this for initialization
	private void Start () {
        uiModel = new UIModel();

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
                isSet = true;

            } else {
                if (isSet)
                {
                    uiModel.SetStatePanelOpts(true);
                    isSet = true;
                }
                else
                {
                    uiModel.SetStatePanelOpts(false);
                    isSet = false;
                }                
			}
           
        }
        
	}


    public void SetPause()
    {
        isSet = false;
        setActivePanelOpts = false;
        uiModel.SetStatePanelOpts(setActivePanelOpts);
    }


}
