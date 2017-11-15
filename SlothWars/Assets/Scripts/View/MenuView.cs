using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour {

    // Use this for initialization
    private MenuModel menuModel;

    private void Start () {
        menuModel = new MenuModel();
	}

    public void PlaySelected()
    {
        menuModel.GetPanelMain().gameObject.SetActive(false);
        menuModel.GetPanelVS().gameObject.SetActive(true);
    }
    //this should go to new screen
    public void PlaySoloSelected()
    {
        SceneManager.LoadScene("TeamSelection");
    }
    //this should go 
    public void PlayFriendSelected()
    {
        SceneManager.LoadScene("TeamSelection");
    }
    public void GoBackPlay()
    {
        menuModel.GetPanelMain().gameObject.SetActive(true);
        menuModel.GetPanelVS().gameObject.SetActive(false);
    }
    public void ShowOptions()
    {
        menuModel.GetPanelMain().gameObject.SetActive(false);
        menuModel.GetPanelOpts().gameObject.SetActive(true);
    }

    public void GoBackOptions()
    {
        menuModel.GetPanelMain().gameObject.SetActive(true);
        menuModel.GetPanelOpts().gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
