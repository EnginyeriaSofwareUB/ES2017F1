using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIView : MonoBehaviour {

    // Use this for initialization

    private UIModel uiModel;
    private ChangeTurnModel changeTurnModelInstance;

    private void Start()
    {
        uiModel = UIModel.Instance;
        changeTurnModelInstance = ChangeTurnModel.Instance;
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
        ChangeUIStats();

    }

    private void ChangeUIStats()
    {
        GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Text>()[0].text = changeTurnModelInstance.GetCurrentSloth().GetHp().ToString();
        GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Text>()[1].text = changeTurnModelInstance.GetCurrentSloth().GetAttack().ToString();
        GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Text>()[2].text = changeTurnModelInstance.GetCurrentSloth().GetDefense().ToString();
        if (changeTurnModelInstance.GetApCurrentSloth() >= 0)
        {
            GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Text>()[3].text = changeTurnModelInstance.GetApCurrentSloth().ToString();
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

    public void SurrenderGame()
    {
        if (changeTurnModelInstance.GetText().text == "Blue Turn")
        {
            //StorePersistentVariables.Instance.winner = "Red"; 
        }
        else
        {
            //StorePersistentVariables.Instance.winner = "Blue";     
        }
        SceneManager.LoadScene("GameOverScene");
    }
}
