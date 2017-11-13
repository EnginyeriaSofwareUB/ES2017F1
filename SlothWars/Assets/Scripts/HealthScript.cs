using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
    private double health = 100;
    private TextMesh text;



	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<TextMesh>();
        text.text = ""+ health;
	}
	// Update is called once per frame
	void Update () {
     
    }
    public void TakeDamage(double d)
    {
        health -= d;
        text.text = "" + health;
    }
    public void turnRight()
    {
        text.transform.eulerAngles = new Vector3(0, 0, 0);
        text.transform.localPosition = new Vector3(0, 3, -0.5f);
    }
    public void turnLeft()
    {
        text.transform.eulerAngles = new Vector3(0, 360, 0);
        text.transform.localPosition = new Vector3(0, 3, 0.5f);
    }

	public void setHealth(double health){
		this.health = health;
	}

	public double getHealth(){
		return health;
	}

}
