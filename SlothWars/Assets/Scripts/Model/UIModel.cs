using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIModel:MonoBehaviour {
    //SINGLETON
    private static UIModel instance;

    public static UIModel Instance
    {
        get
        {
            if (instance == null)
            {
                //TODO: FIX THIS TO MAKE IT SINGLETON
                instance = new UIModel();
            }
            return instance;
        }
    }
    ///////*****///////
    private static bool statePanelOpts, statePanelMain;
    private static Image panelOpts, panelMain;

    protected UIModel() { }
    
    public bool GetStatePanelOpts() { return statePanelOpts; }
    public void SetStatePanelOpts(bool setActivePanelOpts) { statePanelOpts = setActivePanelOpts;}

    public bool GetStatePanelMain() { return statePanelMain; }
    public void SetStatePanelMain(bool setActivePanelMain) { statePanelMain = setActivePanelMain; }

    public Image GetPanelOpts() { return panelOpts; }
    public void SetPanelOpts(Image panelOptsCont) { panelOpts = panelOptsCont; }

    public Image GetPanelMain() { return panelMain; }
    public void SetPanelMain(Image panelMainCont) { panelMain = panelMainCont; }
}
