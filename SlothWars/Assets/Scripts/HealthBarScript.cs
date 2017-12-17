using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image currentHealth;
    RectTransform rt;
    Text health;
    Text shield;
    Transform goShield;
    // Initialization
    void Start()
    {
        rt = (RectTransform)GetComponent<RectTransform>();
        health = (Text)rt.Find("health").GetComponent<Text>();
        shield = (Text)rt.Find("shield_text").GetComponent<Text>();
        goShield = (Transform)rt.Find("shield");
        DeactivateShield();
        rt.rotation = new Quaternion(0, 0, 0, 0);
        rt = (RectTransform)rt.Find("currentHealth");
    }

    void Update()
    {
    }

    // Destroys the game object associated to the script
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void ChangeBarLevel(double ratio)
    {
        ratio = ratio / 10;
        if (ratio < 0)
        {
            ratio = 0;
        }
        rt.localScale = new Vector3((float)ratio, 0.1f, 0.1f);

    }

    public void ChangeHealthText(double health){
        this.health.text = health.ToString();
    }

    public void ChangeTextShield(double shield){
        this.shield.text = shield.ToString();
    }

    public void ActivateShield(){
        goShield.gameObject.SetActive(true);
        shield.gameObject.SetActive(true);
    }

    public void DeactivateShield(){
        goShield.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
    }
}
