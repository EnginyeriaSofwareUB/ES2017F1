using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour {
    public GameObject ForceBar; //force/range bar gameObject
    private Transform gun; // aim vector transform
    private int rotate = 0; // 0 when loocking to the right, 1 when loocking to left
    bool shotLoad = false; // it says if its calculating range/force
    bool mov = false; // sloth is moving bool
    Projectile onLoad; //projectile shoot
    Ability onloadAbility;
    MagicAbility mA;
    ProjectileAbility pA;
    HealingAbility hA;
    ForceBarScript st;

    private Button bAbility1;
    private Button bAbility2;
    private Button bAbility3;
    private bool active = true;
    //initialization
    void Start () {
        gun = this.GetComponentInChildren<Transform>().Find("GUN");
        hA = new HealingAbility();
        pA = new ProjectileAbility();
        mA = new MagicAbility();
        // Buttons
        bAbility1 = GameObject.Find("Ability1").GetComponent<Button>();
        bAbility2 = GameObject.Find("Ability2").GetComponent<Button>();
        bAbility3 = GameObject.Find("Ability3").GetComponent<Button>();

        // Listeners
        bAbility1.onClick.AddListener(delegate { TriggerAbility(1); });
        bAbility2.onClick.AddListener(delegate { TriggerAbility(2); });
        bAbility3.onClick.AddListener(delegate { TriggerAbility(3); });

    }
	// creates a force bar with the first key pressed , shoots with the second key pressed and aims with the vertical axis 
	void Update () {
        if (!mov)
        {
            gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Shot(mA);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Shot(pA);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Shot(hA);
            }
        }
	}
    private void TriggerAbility(int n)
    {
        if (active && !mov)
        {
            if (n == 1)
            {
                Shot(mA);
            }
            if (n == 2)
            {
                Shot(pA);
            }
            if (n == 3)
            {
                Shot(hA);
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
    private void ShootAfterBar(Ability a)
    {
        float radAngle = (gun.eulerAngles[2]*(1-rotate) + rotate*(180- gun.eulerAngles[2]) )* Mathf.Deg2Rad;
        Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        onLoad.SetAll(gun.position,AimVector,gun.rotation,st.getForce()*(float) a.GetRange(),a.GetRadius());
        onLoad.ApplyLogic();
        st.Destroy();
        shotLoad = false;
    }
    // r = 0 when right moving , left moving r = 1
    public void IsMoving(int r)
    {
        mov = true;
        rotate = r;
        gun.gameObject.SetActive(false);
    }
    // tells to the script that the sloth is not moving
    public void IsNotMoving()
    {
        mov = false;
        gun.gameObject.SetActive(true);
    }
    //desactive the gun of the sloth
    public void Active(bool b)
    {
        active = b;
        gun.gameObject.SetActive(b);
    }
    // used to dont move the sloth when shot is on load
    public bool GetShotLoad()
    {
        return this.shotLoad;
    }
    //shots the projectile asociated to Ability a
    public void Shot(Ability a)
    {
        if (!mov)
        {

            if (!shotLoad)
            {
                ProjectileFactory pf = ProjectileFactory.Instance;
                onLoad = pf.getProjectile(a);
                onloadAbility = a;
                Bar();
            }
            else
            {
                if (a.Equals(onloadAbility))
                {
                    ShootAfterBar(a);
                }
            }
        }
    }
}
