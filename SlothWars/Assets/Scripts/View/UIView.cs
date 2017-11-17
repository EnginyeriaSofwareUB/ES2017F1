using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour {

    // Use this for initialization

    private UIModel uiModel;

    private void Start()
    {
        uiModel = new UIModel();
    }

    private void Update()
    {
        if (uiModel.GetPanelOpts() != null)
        {
            
            if (uiModel.GetStatePanelOpts())
            {
                Time.timeScale = 0;
                print("He pasado");
                uiModel.GetPanelOpts().gameObject.SetActive(true);

            }
            else
            {
                Time.timeScale = 1;
                uiModel.GetPanelOpts().gameObject.SetActive(false);

            }

        }
    }
    public void ResumeSelected()
    {
        uiModel.GetPanelOpts().gameObject.SetActive(false);
        
    }

    public void ExitGame()
    {
        uiModel.GetPanelOpts().gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
