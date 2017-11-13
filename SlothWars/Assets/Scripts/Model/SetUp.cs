using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public class SetUp : GameController {
	// Use this for initialization
	List<Player> team1; 
	List<Player> team2;
	List<GameObject> teamSloths1;
	List<GameObject> teamSloths2;
	GameController gameControl;
	void Awake () {
        //CreateMap ();
		gameControl = GameObject.Find("GameController").GetComponent<GameController>();
		team1 = gameControl.GetPlayerTeam (1);
		team2 = gameControl.GetPlayerTeam (2);
		teamSloths1 = StorePersistentVariables.Instance.createdSlothTeam1;
		teamSloths2 = StorePersistentVariables.Instance.createdSlothTeam2;
        PlacePlayers ();

	}

	private void PlacePlayers(){
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3 (100, 1, 100);
		int i = 0;
		GameObject sloth;
		Player pla;
		HealthScript health;
		Animator anim;
		ShotScript shot;
		SlothSelected selected;
		foreach(Player player in team1){
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (i+2, 0, 0), Quaternion.identity);
			// setting health
			health = sloth.AddComponent <HealthScript>();
			health.setHealth (player.GetSloth ().GetHp ());
			health.enabled = true;
			//Start the animation
			anim = sloth.GetComponent <Animator>();
			anim.enabled = true;

			pla = sloth.GetComponent <Player> ();
			pla.SetSloth (player.GetSloth ());
			pla.enabled = true;

			shot = sloth.GetComponent <ShotScript>();
			shot.enabled = true;

			selected = sloth.AddComponent <SlothSelected> ();
			selected.enabled = false;
			teamSloths1.Add (sloth);

			i++;
		}
		i = 0;
		foreach (Player player in team2) {
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (-i-2, 0, 0), Quaternion.identity);
			// setting health
			health = sloth.AddComponent <HealthScript>();
			health.setHealth (player.GetSloth ().GetHp ());
			health.enabled = true;
			//Start the animation
			anim = sloth.GetComponent <Animator>();
			anim.enabled = true;

			pla = sloth.GetComponent <Player> ();
			pla.SetSloth (player.GetSloth ());
			pla.enabled = true;

			shot = sloth.GetComponent <ShotScript>();
			shot.enabled = true;

			selected = sloth.AddComponent <SlothSelected> ();
			selected.enabled = false;

			teamSloths2.Add (sloth);
			i++;
		}
		StorePersistentVariables.Instance.createdSlothTeam1 = teamSloths1;
		StorePersistentVariables.Instance.createdSlothTeam2 = teamSloths2;
	}

}
