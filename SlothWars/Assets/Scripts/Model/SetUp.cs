using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public class SetUp : ControllerSingleton<MonoBehaviour> {
    // Use this for initialization
    private GameController gameController;
    private List<GameObject> teamSloths1, teamSloths2;

    void Start () {
        InitializeSetUpVariables();
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
		foreach(Player player in gameController.GetPlayerTeam(1)){
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
		foreach (Player player in gameController.GetPlayerTeam(2)) {
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

    private void InitializeSetUpVariables()
    {
        teamSloths1 = gameController.teamSloths1;
        teamSloths2 = gameController.teamSloths2;
    }
}
