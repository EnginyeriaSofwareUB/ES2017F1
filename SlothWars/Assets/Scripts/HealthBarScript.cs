using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image currentHealth;
    private double health = 0f;
    RectTransform rt;
    // Initialization
    void Start()
    {
        rt = (RectTransform)GetComponent<RectTransform>();
        rt.rotation = new Quaternion(0, 0, 0, 0);
        rt = (RectTransform)rt.Find("currentHealth");
    }

    void Update()
    {
    }

    // Returns the force associated to the bar
    public float GetHealth()
    {
        return (float)health;
    }
    // Destroys the game object associated to the script
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void ChangeBarLevel(double ratio){
        if (ratio >= 0)
        {
            rt.localScale = new Vector3((float)ratio, 1, 1);
        }else{
            rt.localScale = new Vector3(0, 0, 0);
        }
    }
}