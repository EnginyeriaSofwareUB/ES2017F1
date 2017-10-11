using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
    public GameObject Projectile;
    public GameObject ForceBar;
    private Transform gun;
    private float baseForce= 700;
    bool shotLoad = false;
    ForceBarScript st;
	//initialization
	void Start () {
        gun = GetComponent<Transform>();

    }
	// creates a force bar with the first Q , shoots with the second Q and aims with the vertical axis 
	void Update () {
        gun.Rotate(0 , 0, Time.deltaTime * Input.GetAxis("Vertical")*100);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!shotLoad)
            {
                Bar();
            }
            else
            {
                ShootAfterBar();
            }
        }
	}
    // creates a force bar
    private void Bar()
    {
        GameObject bar = (GameObject)Instantiate(ForceBar, gun.position+ new Vector3(0, 3, 0), gun.rotation);
        st = (ForceBarScript)bar.GetComponent<ForceBarScript>();
        shotLoad = true;
    }
    // creates a projectile and shoots it then destroys de force bar
    private void ShootAfterBar()
    {
        GameObject shot = (GameObject)Instantiate(Projectile, gun.position, gun.rotation);
        float radAngle = gun.eulerAngles[2] * Mathf.Deg2Rad;
        shot.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(radAngle),Mathf.Sin(radAngle),0)* st.getForce() * baseForce);
        st.Destroy();
        shotLoad = false;
    }
}
