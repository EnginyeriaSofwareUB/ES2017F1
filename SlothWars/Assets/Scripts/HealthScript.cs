using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
    private int health= 100;
    private TextMesh text;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<TextMesh>();
        text.text = ""+ health;
	}
	// Update is called once per frame
	void Update () {
        float inputH = Input.GetAxis("Horizontal");
        if (inputH > 0)
        {
            text.transform.eulerAngles = new Vector3(0, 0, 0);
            text.transform.localPosition = new Vector3(0, 3, -0.5f);
        }
        if (inputH < 0)
        {
            text.transform.eulerAngles = new Vector3(0, 360,0);
            text.transform.localPosition = new Vector3(0, 3, 0.5f);
        }
    }
    public void TakeDamage(int d)
    {
        health -= d;
        text.text = "" + health;
    }
}
