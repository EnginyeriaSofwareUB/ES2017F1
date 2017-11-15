using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : ControllerSingleton<MonoBehaviour> {

    protected MenuController() { }

	public Image panelMain;
	public Image panelVS;
	public Image panelOpts;

	
	// Use this for initialization
	void Awake() {
        InitializeMenuVariables();
		
		//inactive to show main menu
		panelVS.gameObject.SetActive (false);
		panelOpts.gameObject.SetActive (false);

        //TODO Si no funciona ,comprobar con el github que los botones tienen asociadas las funciones correctamente
	}

	private void InitializeMenuVariables()
    {
        //panelinit
        panelMain = GameObject.Find("mainMenuPanel").GetComponent<Image>();
        panelVS = GameObject.Find("playVSPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionsPanel").GetComponent<Image>();

        
    }
   
}