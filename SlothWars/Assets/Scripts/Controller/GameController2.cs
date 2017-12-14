using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : MonoBehaviour {
	private UIController2 uiController;
	public List<Sloth> teamSloths1, teamSloths2;
	public GameObject slothWizard, slothArcher, slothTank, slothHealer, slothUtility;
	public Sloth currentSloth;
	private GameControllerStatus status;
	private int turns;
	private bool player;

	// PLAYER TRUE - LISTA 1
	// PLAYER AZUL - TRUE - 0
	// PLAYER FALSE - LISTA 2
	// PLAYER ROJO - FALSE - 1

	// Use this for initialization
	void Start () {
		teamSloths1 = new List<Sloth>();
		teamSloths2 = new List<Sloth>();
		uiController = GameObject.Find("Main Camera").GetComponent<UIController2>();

		List<string> lista = StorePersistentVariables.Instance.slothTeam1;
		List<string> lista2 = StorePersistentVariables.Instance.slothTeam2;

		if(lista.Count == 0){
			lista.Add("Wizard");
		}
		if(lista2.Count == 0){
			lista2.Add("Wizard");
		}
		foreach(string sloth in lista){
			GameObject tmpSloth = new GameObject(sloth+"P1");
			GameObject logic = new GameObject("slothlogic");
			logic.transform.SetParent(tmpSloth.transform);
			GameObject model;
			switch(sloth){
				case "Wizard":
					model = (GameObject)Instantiate(slothWizard);
				break;
				case "Archer":
					model = (GameObject)Instantiate(slothArcher);
				break;
				case "Tank":
					model = (GameObject)Instantiate(slothTank);
				break;
				case "Healer":
					model = (GameObject)Instantiate(slothHealer);
				break;
				case "Utility":
					model = (GameObject)Instantiate(slothUtility);
				break;
				default:
					model = new GameObject("EmptyModel");
				break;
			}

			model.transform.SetParent(tmpSloth.transform);
			model.transform.localPosition = new Vector3(0f, 0f, 0f);
			logic.AddComponent<Sloth>().initSloth(sloth);
			teamSloths1.Add(logic.GetComponent<Sloth>());
		}

		foreach(string sloth in lista2){
			GameObject tmpSloth = new GameObject(sloth+"P2");
			GameObject logic = new GameObject("slothlogic");
			logic.transform.SetParent(tmpSloth.transform);
			GameObject model;
			switch(sloth){
				case "Wizard":
					model = (GameObject)Instantiate(slothWizard);
				break;
				case "Archer":
					model = (GameObject)Instantiate(slothArcher);
				break;
				case "Tank":
					model = (GameObject)Instantiate(slothTank);
				break;
				case "Healer":
					model = (GameObject)Instantiate(slothHealer);
				break;
				case "Utility":
					model = (GameObject)Instantiate(slothUtility);
				break;
				default:
					model = new GameObject("EmptyModel");
				break;
			}

			model.transform.SetParent(tmpSloth.transform);
			model.transform.localPosition = new Vector3(0f, 0f, 0f);
			logic.AddComponent<Sloth>().initSloth(sloth);
			teamSloths2.Add(logic.GetComponent<Sloth>());
		}

		status = GameControllerStatus.WAITING_FOR_INPUT;
		player = true;
		turns = 0;
		currentSloth = teamSloths1[turns % teamSloths1.Count];
	}
	
	// Update is called once per frame
	void Update () {
		if(status == GameControllerStatus.LOGIC){
			CheckLogic();
		}
	}




	private void CheckLogic(){

		//Check if a team of sloths is dead. Maybe end the game.
		if(teamSloths1.Count == 0){
			StorePersistentVariables.Instance.winner = 1;
			status = GameControllerStatus.GAME_OVER;
		}
		if(teamSloths1.Count == 0){
			StorePersistentVariables.Instance.winner = 0;
			status = GameControllerStatus.GAME_OVER;
		}

		player = !player;

		if(player){
			currentSloth = teamSloths1[turns % teamSloths1.Count];
		} else {
			currentSloth = teamSloths2[turns % teamSloths2.Count];
			turns++;
		}




		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public GameControllerStatus GetStatus(){
		return status;
	}

	public void NotifyActionEnded(){
		status = GameControllerStatus.LOGIC;
	}

	public enum GameControllerStatus{
		WAITING_FOR_INPUT, ANIMATING, LOGIC, GAME_OVER
	}
}
