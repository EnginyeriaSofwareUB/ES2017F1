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
		
	}
    public void TakeDamage(int d)
    {
        health -= d;
        text.text = "" + health;
    }
}
