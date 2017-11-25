using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    public GameObject ForceBar; //force/range bar gameObject
    private static bool beginStopped;
    private Transform gun;  // aim vector transform
    private AbilityModel abilityModel;
    private ChangeTurnModel changeTurnModel;
    private int rotate = 0; // 0 when loocking to the right, 1 when loocking to left
    bool shotLoad = false; // it says if its calculating range/force
    bool mov = false; // sloth is moving bool
    Projectile onLoad; //projectile shoot
    Ability onloadAbility;
    ForceBarScript st;
    private bool active = false;
    //initialization
    void Start()
    {
        abilityModel = new AbilityModel();
        changeTurnModel = new ChangeTurnModel();
        gun = GetComponentInChildren<Transform>().Find("Gun");
        gun.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        Active(false);
    }
    void Update()
    {
        if (!mov && active)
        {
            AimWithMouse();
            MarkBuildTerrain();
            if (Input.GetMouseButtonDown(0))
            {
                ShootAfterBar();
            }
        }
       
    }
    private void AimWithMouse()
    {
        Plane playerPlane = new Plane(Vector3.forward, gun.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        playerPlane.Raycast(ray, out hitdist);
        Vector3 targetPoint = ray.GetPoint(hitdist);
        targetPoint -= gun.position;
        float radAngle = (gun.eulerAngles[2] * (1 - rotate) + rotate * (180 - gun.eulerAngles[2])) * Mathf.Deg2Rad;
        Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        Quaternion r = Quaternion.FromToRotation(AimVector, targetPoint);
        AimVector = r.eulerAngles;
        if (rotate == 1)
        {
            AimVector *= -1;
        }
        gun.Rotate(AimVector);
    }
    private void MarkBuildTerrain()
    {
        if (onloadAbility.GetBuildTerrain()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            onLoad.SetAll(ray.origin,ray.direction,Quaternion.identity, 1,this.onloadAbility.GetTerrainSize());
            onLoad.Mark();
        }
    }
    // creates a force bar
    private void Bar()
    {
        if (!onloadAbility.GetBuildTerrain())
        {
            GameObject bar = (GameObject)Instantiate(Resources.Load("Objects/ForceBar"), gun.position + new Vector3(0, 3, 0), gun.rotation);
            st = bar.GetComponent<ForceBarScript>();
        }
        shotLoad = true;
    }
    // creates a projectile and shoots it then destroys de force bar
    private void ShootAfterBar()
    {
        if (onloadAbility.GetBuildTerrain())
        {
            if (onLoad.GetApply())
            {
                shotLoad = false;
                Active(false);
                onLoad.ApplyLogic();
                changeTurnModel.DecrementApCurrentSloth(changeTurnModel.GetCurrentSloth().GetAbility1().GetAp());
            }
        }
        else
        {
            float radAngle = (gun.eulerAngles[2] * (1 - rotate) + rotate * (180 - gun.eulerAngles[2])) * Mathf.Deg2Rad;
            Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
            Debug.Log("rangeeeeeeeee"+(float)onloadAbility.GetRange());
            onLoad.SetAll(gun.position, AimVector, gun.rotation, st.getForce() * (float)onloadAbility.GetRange(), onloadAbility.GetRadius());
            onLoad.ApplyLogic();
            st.Destroy();
            shotLoad = false;
            Active(false);
            changeTurnModel.DecrementApCurrentSloth(changeTurnModel.GetCurrentSloth().GetAbility1().GetAp());
        }
    }
    // r = 0 when right moving , left moving r = 1
    public void IsMoving(int r)
    {
        mov = true;
        rotate = r;
    }
    // tells to the script that the sloth is not moving
    public void IsNotMoving()
    {
        mov = false;
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
				Active(true);            
			}
        }
    }
}
