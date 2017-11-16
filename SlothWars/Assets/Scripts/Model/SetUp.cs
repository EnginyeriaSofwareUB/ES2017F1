using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NUnit.Framework;

public class SetUp : GameController {
    // Use this for initialization
    
    void Start () {
        PlacePlayers();
	}

	private void PlacePlayers(){ 
		GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		plane.transform.localScale = new Vector3 (100, 1, 100);
		int i = 0;
		GameObject sloth;
		AnimPlayer pla;
		HealthScript health;
		Animator anim;
		ShotScript shot;
		SlothSelected selected;
		foreach(AnimPlayer playerSloth in playerTeam1){
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (i+20, 0, 0), Quaternion.identity);
			// setting health
			health = sloth.AddComponent <HealthScript>();
			health.setHealth (playerSloth.GetSloth ().GetHp ());
			health.enabled = true;
			//Start the animation
			anim = sloth.GetComponent <Animator>();
			anim.enabled = true;

			pla = sloth.AddComponent<AnimPlayer>();
			pla.SetSloth (playerSloth.GetSloth());
			pla.enabled = true;

			shot = sloth.GetComponent <ShotScript>();
			shot.enabled = true;

			selected = sloth.AddComponent <SlothSelected> ();
			selected.enabled = false;
			teamSloths1.Add (sloth);

			i++;
		}
		i = 0;
		foreach (AnimPlayer playerSloth in playerTeam2) {
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (i+10, 0, 0), Quaternion.identity);
			// setting health
			health = sloth.AddComponent <HealthScript>();
			health.setHealth (playerSloth.GetSloth ().GetHp ());
			health.enabled = true;
			//Start the animation
			anim = sloth.GetComponent <Animator>();
			anim.enabled = true;

			pla = sloth.AddComponent <AnimPlayer> ();
			pla.SetSloth (playerSloth.GetSloth ());
			pla.enabled = true;

			shot = sloth.GetComponent <ShotScript>();
			shot.enabled = true;

			selected = sloth.AddComponent <SlothSelected> ();
			selected.enabled = false;

			teamSloths2.Add (sloth);
			i++;
		}
        StorePersistentVariables.Instance.teamSloths1 = teamSloths1;
        StorePersistentVariables.Instance.teamSloths2 = teamSloths2;
	}
}
