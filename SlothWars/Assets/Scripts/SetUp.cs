using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SetUp : MonoBehaviour {
	// Use this for initialization
	List<Player> playerTeam1, playerTeam2;
	public List<GameObject> createdTeam1;
	public List<GameObject> createdTeam2;
	private GameObject teamSelection;
	void Awake () {
        //CreateMap ();
		teamSelection = GameObject.Find("sceneBehaviour");
		playerTeam1 = new List<Player> ();
		playerTeam2 = new List<Player> ();

		createdTeam1 = new List<GameObject> ();
		createdTeam2 = new List<GameObject> ();
		CreatePlayers ();
        PlacePlayers ();

	}


	private void CreatePlayers(){
		foreach (Sloth sloth in teamSelection.GetComponent<TeamSelection>().slothTeam1)
		{
			playerTeam1.Add(new Player(sloth));
		}

		foreach (Sloth sloth in teamSelection.GetComponent<TeamSelection>().slothTeam2)
		{
			playerTeam2.Add(new Player(sloth));
		}

	}

	private void PlacePlayers(){
		int i = 0;
		GameObject sloth;
		Player pla;
		HealthScript health;
		Animator anim;
		ShotScript shot;
		SlothSelected selected;
		foreach(Player player in playerTeam1){
			
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (i*i+50, 0, 0), Quaternion.identity);
			print ("HOLA");
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
			createdTeam1.Add (sloth);

			i++;
		}
		foreach (Player player in playerTeam2) {
			sloth = (GameObject) Instantiate (Resources.Load ("Prefabs/Sloth"), new Vector3 (-i*i+50, 0, 0), Quaternion.identity);
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

			createdTeam2.Add (sloth);
			i++;
		}
	}


	public void SetTeamSloths1(List<GameObject> createdTeam1){
		this.createdTeam1 = createdTeam1;
	}

	public void SetTeamSloths2(List<GameObject> createdTeam2){
		this.createdTeam2 = createdTeam2;
	}
}
