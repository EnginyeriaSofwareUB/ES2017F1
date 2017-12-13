using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour{
    //SINGLETON
    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIController();
            }
            return instance;
        }
    }
    protected  UIController() {
        setActivePanelMain = true;
        setActivePanelOpts = false;
        isPause = false;
    }
    ///////*****///////

    //Static variables for Update
    private static int i = 0;
    private static bool isSet = false;
    private static Image panelOpts;
    private static Image panelMain;
    private static bool setActivePanelMain, setActivePanelOpts, isPause;
    //Get UIModel Constructor.
    private UIModel uiModel;

    // Use this for initialization
	private void Start () {
        uiModel = UIModel.Instance;

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


 
    public void SetPanelOpts(Image panelOptsCont)
    {
        panelOpts = panelOptsCont;
    }

    public void SetPanelMain(Image panelMainCont)
    {
        panelMain = panelMainCont;
    }

    public void SetPause()
    {
        isSet = false;
        setActivePanelOpts = false;
        uiModel.SetStatePanelOpts(setActivePanelOpts);
    }


}
