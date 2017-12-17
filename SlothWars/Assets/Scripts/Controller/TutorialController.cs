using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : GameController {
	protected TutorialControllerStatus tutorialStatus;
	protected Vector3 target = new Vector3(22f, 12 + 0.05f, 0.5f);

	// Use this for initialization
	void Start () {
		//To place sloths
		venomSystem = new VenomSystem();
		teamSloths1 = new List<Sloth>();
		teamSloths2 = new List<Sloth>();
		uiController = GameObject.Find("Main Camera").GetComponent<TutorialUIController>();

		TerrainCreator.LoadMap();
        
		GameObject tmpSloth = new GameObject("player");
		GameObject logic = new GameObject("slothlogic");
		logic.tag = "sloth";
		logic.layer = 8;
		logic.transform.SetParent(tmpSloth.transform);
		GameObject model;
		model = (GameObject)Instantiate(Resources.Load<GameObject>("ModelosDefinitivos/ModelosSlothapedia/"+"sloth_wizard"));	
		model.transform.SetParent(tmpSloth.transform);
		//Hay que reescalar los bichos
		model.transform.localScale = new Vector3(27f, 27f, 27f);
		model.transform.Rotate(new Vector3 (90f, 180f, 0f));
		//Los fbx tienen una camara, hay que quitarla
		Destroy(model.transform.Find("Camera").gameObject);
		//Colocar el bichurro en un sitio random
		model.transform.parent.position = new Vector3(20f, 10 + 0.05f, 0.5f);
		logic.AddComponent<Sloth>().initSloth("Wizard");
		logic.AddComponent<ShotScript>();
		logic.AddComponent<MovementController>();
		logic.AddComponent<BoxCollider>();
		//Anadiendo Animator a los sloths
		Animator anim = logic.AddComponent<Animator>();
		anim.runtimeAnimatorController = Resources.Load("ModelosDefinitivos/sloth_action") as RuntimeAnimatorController;
		HealthScript health = logic.AddComponent<HealthScript>();
		health.enabled = true;
		GameObject healthBar = (GameObject)Instantiate(Resources.Load("ModelosDefinitivos/Prefabs/HealthBar"), logic.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
		healthBar.GetComponent<RectTransform>().rotation = Quaternion.Euler(90, 0, 0);
		healthBar.transform.SetParent(logic.transform);
		health.SetHealthBar(healthBar);
		logic.GetComponent<Sloth>().SetTeam(1);
		teamSloths1.Add(logic.GetComponent<Sloth>());
		

		tmpSloth = new GameObject("dummy");
		logic = new GameObject("slothlogic");
		logic.tag = "sloth";
		logic.layer = 8;
		logic.transform.SetParent(tmpSloth.transform);
		model = (GameObject)Instantiate(Resources.Load<GameObject>("ModelosDefinitivos/ModelosSlothapedia/"+"sloth_healer"));
		model.transform.SetParent(tmpSloth.transform);
		//Hay que reescalar los bichos
		model.transform.localScale = new Vector3(27f, 27f, 27f);
		model.transform.Rotate(new Vector3 (90f, 180f, 0f));
		//Los fbx tienen una camara, hay que quitarla
		Destroy(model.transform.Find("Camera").gameObject);
		model.transform.parent.position = new Vector3(16f,10 +  0.05f, 0.5f);
		logic.AddComponent<Sloth>().initSloth("Healer");
		logic.AddComponent<ShotScript>();
		logic.AddComponent<MovementController>();
		logic.AddComponent<BoxCollider>();

		health = logic.AddComponent<HealthScript>();
		health.enabled = true;
		healthBar = (GameObject)Instantiate(Resources.Load("ModelosDefinitivos/Prefabs/HealthBar"), logic.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
		healthBar.transform.SetParent(logic.transform);
		health.SetHealthBar(healthBar);
		logic.GetComponent<Sloth>().SetTeam(2);
		teamSloths2.Add(logic.GetComponent<Sloth>());

		tutorialStatus = TutorialControllerStatus.WELCOME;
		status = GameControllerStatus.LOGIC;
		player = false;
		turns = 0;



	}


	void Update () {
		switch(status){
			case GameControllerStatus.LOGIC:
				CheckLogic();
                uiController.DisplaySlothStats(currentSloth, currentAp);
				break;
			case GameControllerStatus.ABILITY_LOGIC:
				CheckAbilitiesAp();
                uiController.DisplaySlothStats(currentSloth, currentAp);
				break;
			case GameControllerStatus.WAITING_FOR_INPUT:
                uiController.DisplaySlothStats(currentSloth, currentAp);
				if(ia != null && !player){
					DoAction(ia.GetAction(this));
				}
				break;
		}
		switch(tutorialStatus){
			case TutorialControllerStatus.WELCOME:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				uiController.SetActiveGameButtons(false);
				((TutorialUIController)uiController).SetTutorialText("Welcome to the Sloth Wars tutorial! \n Here you will learn basic info about the game. Enjoy :)");
				break;
			case TutorialControllerStatus.TEACHING_MOVEMENT:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("Use your key arrows to move arround the map. Try reaching the marked position!");
				break;
			case TutorialControllerStatus.TEACHING_MOVEMENT2:
				if((currentSloth.gameObject.transform.parent.position - target).magnitude <= 0.05f){
					tutorialStatus = TutorialControllerStatus.TEACHING_ABILITIES;
				}
				break;
			case TutorialControllerStatus.TEACHING_ABILITIES:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("GREAT! Now try using one ability. \n The bar represents the force of the projectile \n" + 
				"and you can use the scope to aim. \n You can use the scroll to zoom in or out if you want to have a better vision of the map!");
				break;
		}
	}

	public void GotIt(){
		switch(tutorialStatus){
			case TutorialControllerStatus.WELCOME:
				tutorialStatus = TutorialControllerStatus.TEACHING_MOVEMENT;
				break;
			case TutorialControllerStatus.TEACHING_MOVEMENT:
				tutorialStatus = TutorialControllerStatus.TEACHING_MOVEMENT2;
				break;
			case TutorialControllerStatus.TEACHING_AP:
				tutorialStatus = TutorialControllerStatus.TEACHING_TURNS;
				break;
		}
		((TutorialUIController)uiController).SetTutorialPopUpActive(false);
	}
	

	public enum TutorialControllerStatus{
		WELCOME, TEACHING_MOVEMENT, TEACHING_MOVEMENT2, TEACHING_ABILITIES, TEACHING_AP, TEACHING_TURNS, KILL_DUMMY
	}
}
