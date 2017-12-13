using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
    public GameObject healthBar;
    private double health = 100;
	private GameObject hp;
    private double maxHealth;
	private GameObject shield;
	private GameObject shieldEffect = null;
	// Use this for initialization
	void Awake () {
      	hp = (GameObject) Instantiate(Resources.Load("Prefabs/health"),this.transform.position + new Vector3(-1,1,0),Quaternion.identity);
		shield = (GameObject) Instantiate(Resources.Load("Prefabs/shield"),this.transform.position + new Vector3(0.5f,1,0),Quaternion.identity);
		shield.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
        health = this.GetComponentInParent<AnimPlayer>().GetSloth().GetHp();
        Debug.Log(health);
        Debug.Log(health/maxHealth);
        healthBar.GetComponent<HealthBarScript>().ChangeBarLevel(health/maxHealth);
		hp.transform.position = this.transform.position + new Vector3 (-2, 1, 0);
		if (shieldEffect != null) {
			shield.transform.position = this.transform.position + new Vector3 (0.5f, 1, 0);
			shieldEffect.transform.position = this.transform.position + new Vector3 (0, 0, -0.5f);
		}
    }

    public void SetHealthBar(GameObject healthBar){
        this.healthBar = healthBar;
    }
    public void turnRight()
    {
        /*texthp.transform.eulerAngles = new Vector3(0, 0, 0);
        texthp.transform.localPosition = new Vector3(0, 3, -0.5f); */
    }
    public void turnLeft()
    {
       /* texthp.transform.eulerAngles = new Vector3(0, 360, 0);
        texthp.transform.localPosition = new Vector3(0, 3, 0.5f); */
    }

	public void setHealth(double health){
		this.health = health;
		hp.GetComponent<TextMesh> ().text = "" + health;
	}

	public double getHealth(){
		return health;
	}

    public void SetMaxHealth(double maxHealth){
        this.maxHealth = maxHealth;
    }

	public void UpdateHP(int hp,int shield)
    {
        health = hp;
		if (shield > 0) {
			if (shieldEffect == null) {
				this.shield.SetActive (true);
				shieldEffect = (GameObject)Instantiate (Resources.Load ("Objects/Shield"), this.transform.position+ new Vector3(0,0,-0.5f), Quaternion.identity);
			}
			this.shield.GetComponent<TextMesh> ().text = "" + shield;
		} else if (shieldEffect != null) {
			Destroy (shieldEffect);
			shieldEffect = null;
			this.shield.SetActive (false);
		}
		this.hp.GetComponent<TextMesh>().text= "" + health;
    }

}
