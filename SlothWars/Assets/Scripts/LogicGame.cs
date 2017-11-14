using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGame : MonoBehaviour {

	private GameObject managerTeam;
	private List<GameObject> teamSloths1, teamSloths2;
	// Use this for initialization
	void Start () {
		managerTeam = GameObject.Find ("managerTeam");
		teamSloths1 = managerTeam.GetComponent <SetUp> ().createdTeam1;
		teamSloths2 = managerTeam.GetComponent <SetUp> ().createdTeam2;
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckSlothsAlive ();
	}

	private void CheckSlothsAlive(){
		HealthScript health;
		Player pla;
		int i = 1;
		foreach (GameObject sloth in teamSloths1) {
			health = (HealthScript) sloth.GetComponent <HealthScript> ();
			if (health.getHealth () <= 0){
				pla = (Player)sloth.GetComponent <Player> ();
				pla.Die ();
				teamSloths1.Remove (sloth);
				managerTeam.GetComponent <SetUp>().SetTeamSloths1 (teamSloths1);
			}
			i++;
		}
		i = 0;
		foreach (GameObject sloth in teamSloths2) {
			health = (HealthScript) sloth.GetComponent <HealthScript> ();
			if (health.getHealth () <= 0){
				pla = (Player)sloth.GetComponent <Player> ();
				pla.Die ();
				teamSloths2.Remove (sloth);
				managerTeam.GetComponent <SetUp>().SetTeamSloths2 (teamSloths2);
			}
			i++;
		}
	}
}
