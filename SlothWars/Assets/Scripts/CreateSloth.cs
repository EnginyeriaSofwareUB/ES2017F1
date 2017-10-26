using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSloth : MonoBehaviour {
	public List<GameObject> teamA;
	public List<GameObject> teamB;


	GameObject slothTeamA;
	GameObject slothTeamB;

	void Start(){

		teamA = new List<GameObject>();

		teamB = new List<GameObject>();

		slothTeamA = GameObject.Find ("avatarSlothTeamA");
        teamA.Add(slothTeamA);


        slothTeamB = GameObject.Find ("avatarSlothTeamB");
        teamB.Add(slothTeamB);


		CreateTeamA ();
		CreateTeamB ();
    }

	public void CreateTeamA(){
		for (int i = 0; i < 3; i++) {
			GameObject newSlothTeamA = Instantiate(slothTeamA);

			newSlothTeamA.transform.position = new Vector3 (-teamA.Count - 1, 0.5F, 0);

			teamA.Add (newSlothTeamA);
		}
	}

	public void CreateTeamB(){
		for (int i = 0; i < 3; i++) {
			GameObject newSlothTeamB = Instantiate(slothTeamB);

			newSlothTeamB.transform.position = new Vector3 (teamB.Count + 1, 0.5F, 0);

			teamB.Add (newSlothTeamB);
		}
	}
		
  
}
