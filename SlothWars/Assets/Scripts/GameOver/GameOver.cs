using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private int winner;
	private string text;
	public List<GameObject> sloths;
	// Use this for initialization
	void Start () {
		winner = StorePersistentVariables.Instance.winner;
		if (winner == 0){
			GameObject.Find("WinnerName").GetComponent<Text>().color = Color.blue;
			text = "BLUE TEAM WINS!";
		}else{
			GameObject.Find("WinnerName").GetComponent<Text>().color = Color.red;
			text = "RED TEAM WINS!";
		}
		GameObject.Find("WinnerName").GetComponent<Text>().text = text;


	}

	//Return to main menu
	public void ReturnToMenu(){
		SceneManager.LoadScene("MainMenu");
	}


	public void Update(){
		foreach(GameObject sloth in sloths){
			sloth.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-200f, 200f), Random.Range(-200f, 200f), Random.Range(-200f, 200f)));
		}
	}
}
