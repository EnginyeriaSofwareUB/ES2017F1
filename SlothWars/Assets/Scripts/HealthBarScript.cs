using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    public Image currentHealth;
    private double health = 0f;
    private float stps = 0.4f;
    RectTransform rt;
    private bool up= true;
	// Initialization
	void Start () {
        rt = (RectTransform)GetComponent<RectTransform>();
        rt.rotation = new Quaternion(0, 0, 0, 0);
        rt = (RectTransform) rt.Find("currentHealth");
        rt.localScale = new Vector3(1, 1, 1);
    }
	
	void Update () {
        
    }

    // Returns the force associated to the bar
    public float GetHealth()
    {
        return (float) health;
    }
    // Destroys the game object associated to the script
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
