using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

	float offset;                           //y-axis offset used to scroll text upward
	Rect viewArea;                          //area in which credits will appear

	public float speed = 9.0f;              //speed at which credits will scroll
	public GUIStyle creditsStyle;           //style in which credits will appear in-game
	public TextAsset creditsText;           //text document used for credits

	void Start()
	{
		InitializeValuesForScript();
	}

	void Update()
	{
		//keeps view area as large as the screen in all aspect ratios
		viewArea = new Rect(0, 0, Screen.width, Screen.height);

		//scrolls text upward based time step
		offset -= Time.deltaTime * this.speed *3f;
	}

	void OnGUI()
	{
		DisplayCredits();
	}

	//initialize all global private variables used in this script
	void InitializeValuesForScript() {
		creditsStyle.normal.textColor = Color.red;
		creditsStyle.alignment = TextAnchor.UpperCenter;
		creditsStyle.fontSize = 24;
		viewArea = new Rect(0, 0, Screen.width, Screen.height);
		offset = this.viewArea.height;
	}

	//creates view area for placing credits inside
	void DisplayCredits()
	{
		GUI.BeginGroup(this.viewArea);

		Rect position = new Rect(0, offset, this.viewArea.width, this.viewArea.height);

		string text = SetExampleText();

		if (creditsText == null)
			GUI.Label(position, text, this.creditsStyle);
		else 	
			GUI.Label(position, creditsText.text, this.creditsStyle);

		GUI.EndGroup();
	}
	public void GoBackMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	//sets up example text to test script with if no
	//  credit text documents are available within the project
	string SetExampleText()
	{
		string text;

		text = @"
SLOTHWARS





AINA FERRÀ

ADRIÀ TORRALBA

JOSÉ PEÑA

JOSE LUIS

ALBERTO LEIVA

ALBERT SEGURA

JOSE LAMAS

IXENT









ENGINYERIA DEL SOFTWARE

CURS 2017-18 GRUP F-1		

";

		return text;
	}
}