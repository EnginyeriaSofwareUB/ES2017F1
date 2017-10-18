using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {
    public GameObject Projectile;
    public GameObject ForceBar;
    private Transform gun;
    private int rotate = 0;
    private float baseForce= 700;
    bool shotLoad = false;
    bool mov = false;
    ForceBarScript st;
	//initialization
	void Start () {
        gun = this.GetComponentInChildren<Transform>().Find("GUN");

    }
	// creates a force bar with the first Q , shoots with the second Q and aims with the vertical axis 
	void Update () {
        if (!mov)
        {
            gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
            float inputH = Input.GetAxis("Horizontal");
            if (inputH > 0)
            {
                rotate = 0;
            }
            if (inputH < 0)
            {
                rotate = 1;
            }
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
        float radAngle = (gun.eulerAngles[2]*(1-rotate) + rotate*(180- gun.eulerAngles[2]) )* Mathf.Deg2Rad;
        Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        GameObject shot = (GameObject)Instantiate(Projectile, gun.position + AimVector * 1.15f , gun.rotation);
        shot.GetComponent<Rigidbody>().AddForce(AimVector* st.getForce() * baseForce);
        st.Destroy();
        shotLoad = false;
    }
    public void IsMoving()
    {
        mov = true;
    }
    public void IsNotMoving()
    {
        mov = false;
    }
}
