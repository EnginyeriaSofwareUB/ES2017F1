using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : GameController {
	protected TutorialControllerStatus tutorialStatus;
	protected ArrowController arrow;
	protected Vector3 target = new Vector3(22f, 12 + 0.05f, 0.5f);

	// Use this for initialization
	void Start () {
		//To place sloths
		venomSystem = new VenomSystem();
		teamSloths1 = new List<Sloth>();
		teamSloths2 = new List<Sloth>();
		uiController = GameObject.Find("Main Camera").GetComponent<TutorialUIController>();
		arrow = GameObject.Find("Main Camera").GetComponent<ArrowController>();

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
		logic.AddComponent<Sloth>().initSloth("Tutorial");
		logic.AddComponent<ShotScript>();
		logic.AddComponent<MovementController>();
		logic.AddComponent<BoxCollider>();
		//Anadiendo Animator a los sloths
		Animator anim = logic.AddComponent<Animator>();
		anim.runtimeAnimatorController = Resources.Load("ModelosDefinitivos/sloth_action") as RuntimeAnimatorController;
		HealthScript health = logic.AddComponent<HealthScript>();
		health.enabled = true;
		GameObject healthBar = (GameObject)Instantiate(Resources.Load("ModelosDefinitivos/Prefabs/HealthBarBlue"), logic.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
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
		healthBar = (GameObject)Instantiate(Resources.Load("ModelosDefinitivos/Prefabs/HealthBarRed"), logic.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
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
				arrow.SetArrowActive(false);
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				uiController.SetActiveGameButtons(false);
				((TutorialUIController)uiController).SetTutorialText("Welcome to the Sloth Wars tutorial! \n Here you will learn basic info about the game. Enjoy :)");
				break;
			case TutorialControllerStatus.TEACHING_MOVEMENT:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("Use your key arrows to move arround the map. Try reaching the marked position!");
				arrow.SetArrowActive(true);
				break;
			case TutorialControllerStatus.TEACHING_MOVEMENT2:
				if((currentSloth.gameObject.transform.parent.position - target).magnitude <= 0.05f){
					tutorialStatus = TutorialControllerStatus.TEACHING_ABILITIES;
					arrow.SetArrowActive(false);
				}
				break;
			case TutorialControllerStatus.TEACHING_ABILITIES:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("GREAT! Now try using one ability. \n The bar represents the force of the projectile \n" + 
				"and you can use the scope to aim. \n You can use the scroll to zoom in or out if you want to have a better vision of the map!");
				tutorialStatus = TutorialControllerStatus.WAITING_FOR_ABILITY;
				break;
			case TutorialControllerStatus.TEACHING_ABILITIES2:
				uiController.SetActiveAb1(false);
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("As you can see, the abilities have a wide variety of effects. \n"+
				"Some can destroy terrain and damage other sloths, while others heal. \n Visit the Slothapedia for more info!");
				break;
			case TutorialControllerStatus.TEACHING_AP:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("Notice that your actions consume Ability Points (AP). \n"+
				"Moving consumes 1 AP but the cost of the ability depends on its type. \n If you consume all your AP you will have to end your turn.");
				break;
			case TutorialControllerStatus.TEACHING_TURNS:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("Speaking of turns... \n"+
				"Normally, the enemy team and you will move in turns. \n Clicking the end turn button will switch the turn.");
				break;
			case TutorialControllerStatus.TEACHING_OPTIONS:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("Press 'ESC' to open an options menu.\n"+
				"There, you can chose to Surrender a game or just simply Quit it. Try it now, altough the options will be disabled for the tutorial...");
				break;
			case TutorialControllerStatus.KILL_DUMMY:
				((TutorialUIController)uiController).SetTutorialPopUpActive(true);
				((TutorialUIController)uiController).SetTutorialText("You now know all the basic to play SlothWars!\n"+
				"Before leaving, try killing the dummy!");
				break;
			case TutorialControllerStatus.WAITING_ESC:
				((TutorialUIController)uiController).SetActiveMainMessage(true);
				((TutorialUIController)uiController).ChangeMainMessage("Press 'ESC'");
				break;
			case TutorialControllerStatus.KILL_DUMMY2:
				uiController.SetActiveAb1(true);
				uiController.SetActiveAb1(true);
				uiController.SetActiveAb1(true);

				if(teamSloths2.Count == 0){
					SceneManager.LoadScene("GameOverScene");
				}
				break;
			default:
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
				arrow.SetTarget(((TutorialUIController)uiController).GetPositionTurns());
				arrow.SetRotation(new Vector3(0f, 0f, -90f));
				arrow.SetOffset(new Vector3(100f, 0f, 0f));
				arrow.SetVertical(false);
				break;
			case TutorialControllerStatus.WAITING_FOR_ABILITY:
				uiController.SetActiveAb1(true);
				arrow.SetArrowActive(true);
				arrow.SetIsWorld(false);
				arrow.SetOffset(new Vector3(0f, 60f, 0f));
				arrow.SetTarget(((TutorialUIController)uiController).GetPositionAb1());
				break;
			case TutorialControllerStatus.TEACHING_ABILITIES2:
				tutorialStatus = TutorialControllerStatus.TEACHING_AP;
				arrow.SetArrowActive(true);
				arrow.SetTarget(((TutorialUIController)uiController).GetPositionAp());
				arrow.SetOffset(new Vector3(5f, 60f, 0f));
				break;
			case TutorialControllerStatus.TEACHING_TURNS:
				tutorialStatus = TutorialControllerStatus.TEACHING_OPTIONS;
				arrow.SetArrowActive(false);
				break;
			case TutorialControllerStatus.TEACHING_OPTIONS:
				tutorialStatus = TutorialControllerStatus.WAITING_ESC;
				break;
			case TutorialControllerStatus.KILL_DUMMY:
				tutorialStatus = TutorialControllerStatus.KILL_DUMMY2;
				arrow.SetArrowActive(false);
				break;
		}
		((TutorialUIController)uiController).SetTutorialPopUpActive(false);
	}

	protected void CheckAbilitiesAp(){
		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public TutorialControllerStatus GetTutorialStatus(){
		return tutorialStatus;
	}

	public void NotifyAbilityUsed(){
		if (tutorialStatus == TutorialControllerStatus.WAITING_FOR_ABILITY){
			tutorialStatus = TutorialControllerStatus.TEACHING_ABILITIES2;
			arrow.SetArrowActive(false);
		}
	}

	public void NotifyOptionsChecked(){
		tutorialStatus = TutorialControllerStatus.KILL_DUMMY;
		arrow.SetIsWorld(true);
		arrow.SetTarget(teamSloths2[0].transform.position);
		arrow.SetRotation(new Vector3(0f, 0f, 90f));
		arrow.SetOffset(new Vector3(0f, 1.5f, 0f));
		arrow.SetVertical(true);
	}

	public void NotifyEsc(){
		tutorialStatus = TutorialControllerStatus.WAIT;
		arrow.SetArrowActive(true);
		arrow.SetTarget(((TutorialUIController)uiController).GetPositionResume());
		arrow.SetOffset(new Vector3(200f, 0f, 0f));
	}

	public void UnPauseGame(){
		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public enum TutorialControllerStatus{
		WELCOME, TEACHING_MOVEMENT, TEACHING_MOVEMENT2, TEACHING_ABILITIES, WAITING_FOR_ABILITY, TEACHING_ABILITIES2, TEACHING_AP, TEACHING_TURNS, TEACHING_OPTIONS, WAITING_ESC, KILL_DUMMY, KILL_DUMMY2, WAIT
	}
}
