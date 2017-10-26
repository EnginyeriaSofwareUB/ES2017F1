using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp : MonoBehaviour {
	public List<Player> playerTeam1;
	public List<Player> playerTeam2;
	// Use this for initialization
	private GameObject teamSelectionGameObject;
	void Start () {
		Debug.Log ("HOLA");
		teamSelectionGameObject = GameObject.Find ("sceneBehaviour");
		CreateTeams ();
		//CreateMap ();
		PlacePlayers ();

	}

	private void CreateTeams(){
		playerTeam1 = new List<Player> ();
		playerTeam2 = new List<Player> ();

		foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam1) {
			playerTeam1.Add (new Player (sloth));
		}

		foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam2) {
			playerTeam2.Add(new Player(sloth));
		}
	}

	private void PlacePlayers(){
		/*GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = new Vector3(0, 0.5F, 0);
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = new Vector3(0, 1.5F, 0);
		GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		capsule.transform.position = new Vector3(2, 1, 0);
		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		cylinder.transform.position = new Vector3(-2, 1, 0);*/
		Resources.Load ("/Assets/3D Models");
	}

}
