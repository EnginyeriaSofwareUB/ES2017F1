using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    public GameObject ForceBar; //force/range bar gameObject
    private static bool beginStopped;
    private static int turnPlayer1, turnPlayer2;
    private Transform gun;
    private AbilityModel abilityModel;
    public List<Transform> listGunTeam1, listGunTeam2; // aim vector transform
    private int rotate = 0; // 0 when loocking to the right, 1 when loocking to left
    bool shotLoad = false; // it says if its calculating range/force
    bool mov = false; // sloth is moving bool
    Projectile onLoad; //projectile shoot
    Ability onloadAbility;
    MagicAbility mA;
    ProjectileAbility pA;
    HealingAbility hA;
    ForceBarScript st;

    private bool active = true;
    //initialization
    void Start()
    {
        abilityModel = new AbilityModel();
        listGunTeam1 = abilityModel.GetGunTeam1();
        listGunTeam2 = abilityModel.GetGunTeam2();
        hA = new HealingAbility();
        pA = new ProjectileAbility();
        mA = new MagicAbility();

    }
    // creates a force bar with the first key pressed , shoots with the second key pressed and aims with the vertical axis 
    void Update()
    {
        beginStopped = abilityModel.GetBeginStopped();

        if (beginStopped)
        {
            if (!mov)
            {
                gun = listGunTeam1[0];
                gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
            }
        } else
        {
            turnPlayer1 = abilityModel.GetTurnPlayer1();
            turnPlayer2 = abilityModel.GetTurnPlayer2();
            listGunTeam1[0].gameObject.SetActive(false);
            if (turnPlayer1 - turnPlayer2 == 1)
            {
                if (!mov)
                {
                    gun = listGunTeam2[turnPlayer2];
                    gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
                }
            } else if (turnPlayer1 == turnPlayer2)
            {
                if(turnPlayer1 == 0)
                {
                    listGunTeam1[0].gameObject.SetActive(true);
                }
                if (!mov)
                {
                    
                    gun = listGunTeam1[turnPlayer1];
                    gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
                }
            } else if (turnPlayer1 - turnPlayer2 < 0)
            {
                if (!mov)
                {
                    gun = listGunTeam2[turnPlayer2];
                    gun.Rotate(0, 0, Time.deltaTime * Input.GetAxis("Vertical") * 100);
                }
            }
        }
    }
    public void TriggerAbility1()
    {
        if (active && !mov){
            Shot(mA);
        }
    }

    public void TriggerAbility2()
    {
        if (active && !mov)
        {
            Shot(pA);
        }
    }

    public void TriggerAbility3()
    {
        if (active && !mov)
        {
            Shot(hA);
        }
    }

    // creates a force bar
    private void Bar()
    {
        GameObject bar = (GameObject) Instantiate(Resources.Load("Objects/ForceBar"), gun.position + new Vector3(0, 3, 0), gun.rotation);
        st = bar.GetComponent<ForceBarScript>();
        shotLoad = true;
    }
    // creates a projectile and shoots it then destroys de force bar
    private void ShootAfterBar(Ability a)
    {
        float radAngle = (gun.eulerAngles[2] * (1 - rotate) + rotate * (180 - gun.eulerAngles[2])) * Mathf.Deg2Rad;
        Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        onLoad.SetAll(gun.position, AimVector, gun.rotation, st.getForce() * (float)a.GetRange(), a.GetRadius());
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
                Debug.Log("The type of the used projectile is " + onLoad.GetType().ToString());
                onloadAbility = a;
                abilityModel.SetLastAbility(a);
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
