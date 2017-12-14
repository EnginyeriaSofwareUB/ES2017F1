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
	private int currentAp;

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
			lista2.Add("Tank");
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
			logic.AddComponent<ShotScript>();
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
			logic.AddComponent<ShotScript>();
			teamSloths2.Add(logic.GetComponent<Sloth>());
		}

		status = GameControllerStatus.LOGIC;
		player = false;
		turns = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch(status){
			case GameControllerStatus.LOGIC:
				CheckLogic();
				break;
			case GameControllerStatus.ABILITY_LOGIC:
				CheckAbilitiesAp();
				break;
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

		uiController.DisplaySlothAbilities(currentSloth);
		currentAp = currentSloth.GetAp();
		CheckAbilitiesAp();




		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	private void CheckAbilitiesAp(){
		if(currentSloth.GetAbility1().GetAp() < currentAp){
			uiController.SetActiveAb1(true);
		} else {
			uiController.SetActiveAb1(false);
		}
		if(currentSloth.GetAbility2().GetAp() < currentAp){
			uiController.SetActiveAb2(true);
		} else {
			uiController.SetActiveAb2(false);
		}
		if(currentSloth.GetAbility3().GetAp() < currentAp){
			uiController.SetActiveAb3(true);
		} else {
			uiController.SetActiveAb3(false);
		}
		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public GameControllerStatus GetStatus(){
		return status;
	}

	public void EndTurn(){
		status = GameControllerStatus.LOGIC;
	}

	public void NotifyActionEnded(){
		status = GameControllerStatus.ABILITY_LOGIC;
	}

	public void CastAbility1(){
		currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility1());
		currentAp -= currentSloth.GetAbility1().GetAp();
		NotifyActionEnded();
	}

	public void CastAbility2(){
		currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility2());
		currentAp -= currentSloth.GetAbility2().GetAp();
		NotifyActionEnded();
	}

	public void CastAbility3(){
		currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility3());
		currentAp -= currentSloth.GetAbility3().GetAp();
		NotifyActionEnded();
	}

	public enum GameControllerStatus{
		WAITING_FOR_INPUT, ANIMATING, LOGIC, GAME_OVER, ABILITY_LOGIC
	}
}
