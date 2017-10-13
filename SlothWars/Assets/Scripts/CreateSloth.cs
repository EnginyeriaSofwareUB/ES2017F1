using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSloth : MonoBehaviour {
	GameObject cube;
	ArrayList teamA;
	ArrayList teamB;

	GameObject allySloth;
	GameObject enemySloth;

	void Start(){
		teamA = new ArrayList ();
		teamB = new ArrayList ();
		allySloth = GameObject.Find ("avatarFriendlySloth");
		enemySloth = GameObject.Find ("avatarEnemySloth");
	}

	// Use this for initialization
	public void CreateSlothTeamA () {
		if (teamA.Count < 4) {
			//cube =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			//cube.transform.position = new Vector3(teamA.Count+1, 0.5F, 0);
			//teamA.Add (cube);
			GameObject clone = Instantiate(allySloth);
			clone.transform.position = new Vector3 (-teamA.Count - 1, 0.5F, 0);
			teamA.Add (clone);
		}

	}

	public void DestroySlothTeamA(){
		cube = (GameObject) teamA [teamA.Count-1];
		teamA.Remove (cube);
		Destroy (cube);
	}


	// Use this for initialization
	public void CreateSlothTeamB () {
		if (teamB.Count < 4) {
			GameObject clone = Instantiate(allySloth);
			clone.transform.position = new Vector3 (+teamB.Count + 1, 0.5F, 0);
			teamB.Add (clone);
		}

	}

	public void DestroySlothTeamB(){
		cube = (GameObject) teamB [teamB.Count-1];
		teamB.Remove (cube);
		Destroy (cube);
	}

}
