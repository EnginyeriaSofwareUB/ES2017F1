using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


	private List<Player> playerTeam1;
	private List<Player> playerTeam2;
	private GameObject gameObject;
	static AbilityFactory abilityFactory;
	// Use this for initialization
	void Start () {
		
		gameObject = GameObject.Find("SetUp");
		playerTeam1 = gameObject.GetComponent<SetUp> ().playerTeam1;
		playerTeam2 = gameObject.GetComponent<SetUp> ().playerTeam2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static AbilityFactory GetAbilityFactory(){
		return abilityFactory;
	}
}
