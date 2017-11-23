using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	private string winner;
	// Use this for initialization
	void Start () {
		winner = StorePersistentVariables.Instance.winner;
		if (winner == "Red"){
			GameObject.Find("WinnerName").GetComponent<Text>().color = Color.red;
		}else{
			GameObject.Find("WinnerName").GetComponent<Text>().color = Color.blue;
		}
		GameObject.Find("WinnerName").GetComponent<Text>().text = StorePersistentVariables.Instance.winner;
	}

	//Return to main menu
	public void ReturnToMenu(){
		SceneManager.LoadScene("MainMenu");
	}
}
