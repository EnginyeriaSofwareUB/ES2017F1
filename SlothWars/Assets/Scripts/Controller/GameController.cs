﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	protected IA ia;
	protected UIController2 uiController;
	public List<Sloth> teamSloths1, teamSloths2;
	public Sloth currentSloth;
	protected GameControllerStatus status;
	protected int turns;
	protected bool player;
	protected int currentAp, cancelAp;
	public GameObject leaf;
	public GameObject toTexture;
	public Material blueLeaf;
	public Material redLeaf;
    protected VenomSystem venomSystem;
    private bool shot = false;
	// PLAYER TRUE - LISTA 1
	// PLAYER AZUL - TRUE - 0
	// PLAYER FALSE - LISTA 2
	// PLAYER ROJO - FALSE - 1

	// Use this for initialization
	void Start () {
		//To place sloths
		int point;
        venomSystem = new VenomSystem();
		teamSloths1 = new List<Sloth>();
		teamSloths2 = new List<Sloth>();
		uiController = GameObject.Find("Main Camera").GetComponent<UIController2>();

		List<string> lista = StorePersistentVariables.Instance.slothTeam1;
		List<string> lista2 = StorePersistentVariables.Instance.slothTeam2;
		if (StorePersistentVariables.Instance.iaPlaying){ ia = new IA(); }
        
		TerrainCreator.LoadMap();
		if(lista.Count == 0){
			lista.Add("Wizard");
            lista.Add("Healer");
		}
		if(lista2.Count == 0){
			lista2.Add("Tank");
            lista2.Add("Archer");
		}
        
        
		foreach(string sloth in lista){
			GameObject tmpSloth = new GameObject(sloth+"P1");
            tmpSloth.tag = "physical";
            Rigidbody rb = tmpSloth.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            tmpSloth.AddComponent<IceCollision>();

            GameObject logic = new GameObject("slothlogic");
			logic.tag = "sloth";
			logic.layer = 8;
			logic.transform.SetParent(tmpSloth.transform);
			GameObject model;
			switch(sloth){
				case "Wizard":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_wizard"));
				break;
				case "Archer":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_archer"));
				break;
				case "Tank":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_tank"));
				break;
				case "Healer":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_healer"));
				break;
				case "Utility":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_utility"));
				break;
				default:
					model = new GameObject("EmptyObject");
				break;
			}		
			model.transform.SetParent(tmpSloth.transform);
			//Hay que reescalar los bichos
			model.transform.localScale = new Vector3(27f, 27f, 27f);
			model.transform.Rotate(new Vector3 (90f, 180f, 0f));
			//Los fbx tienen una camara, hay que quitarla
			Destroy(model.transform.Find("Camera").gameObject);
			//Colocar el bichurro en un sitio random
			point = Random.Range(0, TerrainCreator.GetAvailablePlaces().Count);
            TerrainCreator.GetAvailablePlaces().RemoveAt(point);
			
			model.transform.parent.position = new Vector3(TerrainCreator.GetAvailablePlaces()[point].x_coord, 
                       TerrainCreator.GetAvailablePlaces()[point].y_coord + 0.05f, 0.5f);
			
			logic.AddComponent<Sloth>().initSloth(sloth);
			logic.AddComponent<ShotScript>();
			logic.AddComponent<MovementController>();
            logic.AddComponent<SlothGravity>();
            BoxCollider bc = logic.AddComponent<BoxCollider>();
            bc.size = new Vector3(0.5f, 0.7f, 0.9f);
                        

            //Anadiendo Animator a los sloths
            Animator anim = logic.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load("Modelos/sloth_action") as RuntimeAnimatorController;

            HealthScript health = logic.AddComponent<HealthScript>();
            health.enabled = true;
            GameObject healthBar = (GameObject)Instantiate(Resources.Load("Prefabs/HealthBarBlue"), logic.transform.position + new Vector3(0, 1, -0.51f), Quaternion.identity);
            healthBar.GetComponent<RectTransform>().rotation = Quaternion.Euler(90, 0, 0);
            healthBar.transform.SetParent(logic.transform);
            health.SetHealthBar(healthBar);

            logic.GetComponent<Sloth>().SetTeam(1);
            teamSloths1.Add(logic.GetComponent<Sloth>());
		}

		foreach(string sloth in lista2){
			GameObject tmpSloth = new GameObject(sloth+"P2");
            tmpSloth.tag = "physical";
            Rigidbody rb = tmpSloth.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            tmpSloth.AddComponent<IceCollision>();

            GameObject logic = new GameObject("slothlogic");
			logic.tag = "sloth";
			logic.layer = 8;
			logic.transform.SetParent(tmpSloth.transform);
			GameObject model;
			switch(sloth){
				case "Wizard":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_wizard"));
				break;
				case "Archer":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_archer"));
				break;
				case "Tank":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_tank"));
				break;
				case "Healer":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_healer"));
				break;
				case "Utility":
					model = (GameObject)Instantiate(Resources.Load<GameObject>("Modelos/"+"sloth_utility"));
				break;
				default:
					model = new GameObject("EmptyObject");
				break;
			}

			model.transform.SetParent(tmpSloth.transform);
			//Hay que reescalar los bichos
			model.transform.localScale = new Vector3(27f, 27f, 27f);
			model.transform.Rotate(new Vector3 (90f, 180f, 0f));
			//Los fbx tienen una camara, hay que quitarla
			Destroy(model.transform.Find("Camera").gameObject);
			//Colocar el bichurro en un sitio random
			point = Random.Range(0, TerrainCreator.GetAvailablePlaces().Count);
            TerrainCreator.GetAvailablePlaces().RemoveAt(point);
			
			model.transform.parent.position = new Vector3(TerrainCreator.GetAvailablePlaces()[point].x_coord, 
                       TerrainCreator.GetAvailablePlaces()[point].y_coord + 0.05f, 0.5f);
			logic.AddComponent<Sloth>().initSloth(sloth);
			logic.AddComponent<ShotScript>();
			logic.AddComponent<MovementController>();
            logic.AddComponent<SlothGravity>();
            BoxCollider bc = logic.AddComponent<BoxCollider>();
            bc.size = new Vector3(0.5f, 0.7f, 0.9f);
            
            //Anadiendo Animator a los sloths
            Animator anim = logic.AddComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load("Modelos/sloth_action") as RuntimeAnimatorController;
            
            HealthScript health = logic.AddComponent<HealthScript>();
            health.enabled = true;
            GameObject healthBar = (GameObject)Instantiate(Resources.Load("Prefabs/HealthBarRed"), logic.transform.position + new Vector3(0, 1, -0.51f), Quaternion.identity);
            healthBar.transform.SetParent(logic.transform);
			//healthBar.GetComponent<RectTransform>().localRotation = Quaternion.Euler(90, 0, 0);
            health.SetHealthBar(healthBar);

            logic.GetComponent<Sloth>().SetTeam(2);
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
                uiController.DisplaySlothStats(currentSloth, currentAp);
				break;
			case GameControllerStatus.ABILITY_LOGIC:
				CheckAbilitiesAp();
                uiController.DisplaySlothStats(currentSloth, currentAp);
                break;
			case GameControllerStatus.WAITING_FOR_INPUT:
                uiController.DisplaySlothStats(currentSloth, currentAp);
				if(ia != null && !player)
				{
					DoAction(ia.GetAction(this));
				}
				break;
		}
	}




	protected void CheckLogic(){
        venomSystem.ApplyVenoms();
		//Check if a team of sloths is dead. Maybe end the game.
		if(teamSloths1.Count == 0){
			StorePersistentVariables.Instance.winner = 1;
			status = GameControllerStatus.GAME_OVER;
			SceneManager.LoadScene("GameOverScene");
		}
		if(teamSloths2.Count == 0){
			StorePersistentVariables.Instance.winner = 0;
			status = GameControllerStatus.GAME_OVER;
			SceneManager.LoadScene("GameOverScene");
			return;
		}

		player = !player;

		if(player){
			currentSloth = teamSloths1[turns % teamSloths1.Count];
			toTexture.GetComponent<MeshRenderer>().material = blueLeaf;
		} else {
			currentSloth = teamSloths2[turns % teamSloths2.Count];
			turns++;
			toTexture.GetComponent<MeshRenderer>().material = redLeaf;
		}
		leaf.transform.SetParent(currentSloth.gameObject.transform);
		leaf.transform.localPosition = new Vector3(0f, 0f, 0f);

		uiController.DisplaySlothAbilities(currentSloth);
        currentAp = currentSloth.GetAp();
        //uiController.DisplaySlothStats(currentSloth,currentAp);
		uiController.UpdateTurnPlayerInfo(player);
		
		CheckAbilitiesAp();

		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public void CheckAbilitiesAp(){
		if(currentSloth.GetAbility1().GetAp() <= currentAp){
			uiController.SetActiveAb1(true);
		} else {
			uiController.SetActiveAb1(false);
		}
		if(currentSloth.GetAbility2().GetAp() <= currentAp){
			uiController.SetActiveAb2(true);
		} else {
			uiController.SetActiveAb2(false);
		}
		if(currentSloth.GetAbility3().GetAp() <= currentAp){
			uiController.SetActiveAb3(true);
		} else {
			uiController.SetActiveAb3(false);
		}
        //TODO: CAMBIAR ESTO:
        if (this.shot) { status = GameControllerStatus.ANIMATING; }
        else { status = GameControllerStatus.WAITING_FOR_INPUT; }
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
        shot = true;
        currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility1());
        cancelAp = currentAp;
		currentAp -= currentSloth.GetAbility1().GetAp();
        uiController.DisplaySlothStats(currentSloth, currentAp);
        NotifyActionEnded();
	}

	public void CastAbility2(){
        shot = true;
        currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility2());
        cancelAp = currentAp;
		currentAp -= currentSloth.GetAbility2().GetAp();
        uiController.DisplaySlothStats(currentSloth, currentAp);
        NotifyActionEnded();
	}

	public void CastAbility3(){
        shot = true;
        currentSloth.gameObject.GetComponent<ShotScript>().Shot(currentSloth.GetAbility3());
        cancelAp = currentAp;
		currentAp -= currentSloth.GetAbility3().GetAp();
        uiController.DisplaySlothStats(currentSloth, currentAp);
        NotifyActionEnded();
	}

	public void MoveSloth(int x, int y){
		if(currentAp > 0){
            currentAp--;
            uiController.DisplaySlothStats(currentSloth, currentAp);
            currentSloth.gameObject.GetComponent<MovementController>().MoveTo(x, y);
            
            status = GameControllerStatus.ANIMATING;
		} else {
			uiController.NotifyNotEnoughAp();
		}
	}

	public void PauseGame(){
		status = GameControllerStatus.PAUSE;
		uiController.SetActiveGameButtons(false);
	}

	public void UnPauseGame(){
		status = GameControllerStatus.WAITING_FOR_INPUT;
		uiController.SetActiveGameButtons(true);
	}

	public void OnDieSloth(Sloth sloth){
		if(currentSloth == sloth){
			EndTurn();
			leaf.transform.SetParent(GameObject.Find("Main Camera").transform);
		}
		if(teamSloths1.Contains(sloth)){
			teamSloths1.Remove(sloth);
			Destroy(sloth.transform.parent.gameObject);
		}
		if(teamSloths2.Contains(sloth)){
			teamSloths2.Remove(sloth);
			Destroy(sloth.transform.parent.gameObject);
		}
	}

	protected void DoAction(GameAction action)
	{
		switch (action.actionType)
		{
		case GameAction.ActionType.MOVERSE:
			MoveSloth((int)action.x,(int)action.y);
			break;
        case GameAction.ActionType.EJECUTAR_HABILIDAD:
                
            if (currentAp >= action.ability.GetAp())
            {
                Projectile pr = ProjectileFactory.Instance.getProjectile(action.ability);
                    
                pr.SetAll(currentSloth.transform.position, action.aimVector, Quaternion.Euler(action.angleAbility, action.angleAbility, 0), action.ability.GetRange(), action.ability.GetRadius(), action.ability.GetExplosive(), action.ability.GetSource());
                //onLoad.SetAll(gun.position, AimVector, gun.rotation, st.getForce() * (float)onloadAbility.GetRange(), onloadAbility.GetRadius(), onloadAbility.GetExplosive(), onloadAbility.GetSource());

                pr.ApplyLogic();
                    
                cancelAp = currentAp;
                currentAp -= action.ability.GetAp();
                uiController.DisplaySlothStats(currentSloth, currentAp);
            }
            else
            {
                if (teamSloths1.Count != 0)
                {
                    EndTurn();
                }
            }
            break;
		case GameAction.ActionType.ENDTURN:
			EndTurn();
			break;
		}
	}

    public bool GetGravity()
    {
        return currentSloth.transform.parent.GetComponent<Rigidbody>().useGravity;
    }

	public Sloth GetCurrentSloth(){
		return currentSloth;
	}

	public void Surrender(){
		StorePersistentVariables.Instance.winner = 1;
		if(player){
			StorePersistentVariables.Instance.winner = 0;
		}
		status = GameControllerStatus.GAME_OVER;
		SceneManager.LoadScene("GameOverScene");
	}
	
	public void QuitGame(){
		SceneManager.LoadScene("MainMenu");
	}

	public void DestroyTerrain(GameObject destroyable){
        GameObject.Destroy(destroyable);
	}

	public List<Sloth> GetTeamBlue() { 
		return teamSloths1; 
	}

	public List<Sloth> GetTeamRed() { 
		return teamSloths2; 
	}

	public int GetCurrentAp(){
		return currentAp;
	}

    public void SetCurrentAp(int currentAp)
    {
        this.currentAp = currentAp;
    }
	public void CancelAbility(){
		currentSloth.gameObject.GetComponent<ShotScript> ().CancelShot ();
        currentAp += cancelAp - currentAp;
	}

	public enum GameControllerStatus{
		WAITING_FOR_INPUT, ANIMATING, LOGIC, GAME_OVER, ABILITY_LOGIC, PAUSE
	}
    public void SumToCurrentAp(int apFruit)
    {
        currentAp += apFruit;
    }
    public void AddVenom(Venom v)
    {
        venomSystem.AddVenom(v);
    }

	public void NotifyAbilityUsed(){
        status = GameControllerStatus.ANIMATING;
    }

	//TODO: USAR ESTO
	public void NotifyAbilityEnded(){
		status = GameControllerStatus.WAITING_FOR_INPUT;
	}

	public void NotifyFalling(){
		status = GameControllerStatus.ANIMATING;
	}
	public void NotifyFallingEnded(){
		status = GameControllerStatus.WAITING_FOR_INPUT;
	}
    public void SetShot(bool b)
    {
        this.shot = b;
    }
}