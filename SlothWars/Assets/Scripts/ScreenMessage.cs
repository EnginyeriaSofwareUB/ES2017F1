using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMessage : MonoBehaviour {

    public static ScreenMessage sm;

    GameObject mainText;
    GameObject tipText;
    GameObject tipImage;

    private bool showingMessage;

    // Use this for initialization
    void Start () {
        showingMessage = false;
        mainText = GameObject.Find("MainMessage");
        mainText.GetComponent<Text>().text = "";
        sm = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMessage(string message, float seconds)
    {
        if (!showingMessage)
        {
            StartCoroutine(InvokeMessage(message, seconds));
        }
    }

    public void ForceShowMessage(string message, float seconds)
    {
        StopAllCoroutines();
        showingMessage = false;
        StartCoroutine(InvokeMessage(message, seconds));
    }

    private IEnumerator InvokeMessage(string msg, float sec)
    {
        showingMessage = true;
        mainText.SetActive(true);
        mainText.GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(sec);
        mainText.GetComponent<Text>().text = "";
        showingMessage = false;
    }

}
