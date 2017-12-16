using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ButtonOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private UIController2 uiController;
	// Use this for initialization

	public void Start(){
		uiController = GameObject.Find("Main Camera").GetComponent<UIController2>();
	}
	public void OnPointerEnter(PointerEventData eventData)
    {
		StartCoroutine (waiter ());
    }

	public void OnPointerExit(PointerEventData eventData){
		StartCoroutine (waiter ());

	}

	IEnumerator waiter(){
		yield return new WaitForSeconds (0.7f);
		if (EventSystem.current.IsPointerOverGameObject())
            {
            show();
            }
		if (!(EventSystem.current.IsPointerOverGameObject()))
		{
			unshow();
		}


	}

	public void show(){
		uiController.SetActiveInfoAbPanel(true);

		Text abText = GameObject.Find ("abilityText").GetComponent<Text> ();
		abText.text = uiController.getAbilityInfo (gameObject.name);

		//If your mouse hovers over the GameObject with the script attached, output this message
	}

	public void unshow(){
		uiController.SetActiveInfoAbPanel(false);
	}

}
