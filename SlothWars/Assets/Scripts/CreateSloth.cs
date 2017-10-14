using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSloth : MonoBehaviour {
	public List<GameObject> teamA, gunsTeamA;
	public List<GameObject> teamB, gunsTeamB;


	GameObject slothTeamA, gunSlothTeamA;
	GameObject slothTeamB, gunSlothTeamB;

	void Start(){

		teamA = new List<GameObject>();
        gunsTeamA = new List<GameObject>();

		teamB = new List<GameObject>();
        gunsTeamB = new List<GameObject>();

        slothTeamA = GameObject.Find ("avatarSlothTeamA");
        teamA.Add(slothTeamA);
       
        gunSlothTeamA = GameObject.Find("gunSlothTeamA");
        gunsTeamA.Add(gunSlothTeamA);


        slothTeamB = GameObject.Find ("avatarSlothTeamB");
        teamB.Add(slothTeamB);

        gunSlothTeamB = GameObject.Find("gunSlothTeamB");
        gunsTeamB.Add(gunSlothTeamB);


		CreateTeamA ();
		CreateTeamB ();
    }

	public void CreateTeamA(){
		for (int i = 0; i < 3; i++) {
			GameObject newSlothTeamA = Instantiate(slothTeamA);
			GameObject newGunTeamA = Instantiate(gunSlothTeamA);

			newSlothTeamA.transform.position = new Vector3 (-teamA.Count - 1, 0.5F, 0);
			newGunTeamA.transform.position = new Vector3(-gunsTeamA.Count - 1, 0.5F, 0);

			teamA.Add (newSlothTeamA);
			gunsTeamA.Add(newGunTeamA);
		}
	}

	public void CreateTeamB(){
		for (int i = 0; i < 3; i++) {
			GameObject newSlothTeamB = Instantiate(slothTeamB);
			GameObject newGunTeamB = Instantiate(gunSlothTeamB);

			newSlothTeamB.transform.position = new Vector3 (teamB.Count + 1, 0.5F, 0);
			newGunTeamB.transform.position = new Vector3(+gunsTeamB.Count + 1, 0.5F, 0);

			teamB.Add (newSlothTeamB);
			gunsTeamB.Add(newGunTeamB);
		}
	}

    // Use this for initialization
    public void CreateSlothTeamA () {
		if (teamA.Count < 4 && gunsTeamA.Count < 4)
        {

			GameObject newSlothTeamA = Instantiate(slothTeamA);
            GameObject newGunTeamA = Instantiate(gunSlothTeamA);

			newSlothTeamA.transform.position = new Vector3 (-teamA.Count - 1, 0.5F, 0);
            newGunTeamA.transform.position = new Vector3(-gunsTeamA.Count - 1, 0.5F, 0);

            teamA.Add (newSlothTeamA);
            gunsTeamA.Add(newGunTeamA);
		}

	}

/*
	public void DestroySlothTeamA(){
        if (teamA.Count == 0 && gunsTeamA.Count == 0)
        {
		    teamA.Remove((GameObject) teamA [teamA.Count-1]);
            gunsTeamA.Remove((GameObject)gunsTeamA[gunsTeamA.Count - 1]);

		    Destroy ((GameObject)teamA[teamA.Count - 1]);
            Destroy ((GameObject)gunsTeamA[gunsTeamA.Count - 1]);

        }
    }
*/
    // Use this for initialization
    public void CreateSlothTeamB () {
		if (teamB.Count < 4 && gunsTeamB.Count < 4)
        {

			GameObject newSlothTeamB = Instantiate(slothTeamB);
            GameObject newGunTeamB = Instantiate(gunSlothTeamB);

			newSlothTeamB.transform.position = new Vector3 (+teamB.Count + 1, 0.5F, 0);
            newGunTeamB.transform.position = new Vector3(-gunsTeamB.Count - 1, 0.5F, 0);

            teamB.Add (newSlothTeamB);
            gunsTeamB.Add(newGunTeamB);

        }

	}
    /*
    public void DestroySlothTeamB()
    {
        if (teamB.Count == 0 && gunsTeamB.Count == 0)
        {           
            teamB.Remove((GameObject)teamB[teamB.Count - 1]);
            gunsTeamB.Remove((GameObject)gunsTeamB[gunsTeamB.Count - 1]);

            Destroy((GameObject)teamB[teamB.Count - 1]);
            Destroy((GameObject)gunsTeamB[gunsTeamB.Count - 1]);
        }
    }
    */
}
