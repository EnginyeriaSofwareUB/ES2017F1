using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotScript : MonoBehaviour
{
    public GameObject ForceBar; //force/range bar gameObject
    private Transform gun;  // aim vector transform
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
		
        gun = transform;
		gun.position = new Vector3(gun.position.x,gun.position.y,gun.position.z-0.5f);
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
				GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundEffects>().playLaunchEffect ();
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
        float radAngle = gun.eulerAngles[2]  * Mathf.Deg2Rad;
        Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
        Quaternion r = Quaternion.FromToRotation(AimVector, targetPoint);
        AimVector = r.eulerAngles;
        gun.Rotate(AimVector);
    }
    private void MarkBuildTerrain()
    {
        if (onloadAbility.GetMark()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			onLoad.SetAll(gun.position, ray.direction,Quaternion.identity, 1,this.onloadAbility.GetTerrainSize(),false,onloadAbility.GetSource());
            onLoad.Mark();
        }
    }
    // creates a force bar
    private void Bar()
    {
        if (!onloadAbility.GetMark())
        {
            GameObject bar = (GameObject)Instantiate(Resources.Load("Objects/ForceBar"), gun.position + new Vector3(0, 0.8f, 0), gun.rotation);
            st = bar.GetComponent<ForceBarScript>();
        }
        shotLoad = true;
    }
    // creates a projectile and shoots it then destroys de force bar
    private void ShootAfterBar()
    {
        if (onloadAbility.GetMark())
        {
            if (onLoad.GetApply())
            {
				onLoad.SetAll(gun.position,new Vector3(0,0,0),Quaternion.identity, 1,this.onloadAbility.GetTerrainSize(),false,onloadAbility.GetSource());
                shotLoad = false;
                Active(false);
                onLoad.ApplyLogic();

            }
        }
        else
        {
            float radAngle = gun.eulerAngles[2]  * Mathf.Deg2Rad;
            Vector3 AimVector = new Vector3(Mathf.Cos(radAngle), Mathf.Sin(radAngle), 0);
			onLoad.SetAll(gun.position, AimVector, gun.rotation, st.getForce() * (float)onloadAbility.GetRange(), onloadAbility.GetRadius(),onloadAbility.GetExplosive(),onloadAbility.GetSource());
            onLoad.ApplyLogic();
            st.Destroy();
            shotLoad = false;
            Active(false);
        }
    }
    public void CancelShot()
    {
        shotLoad = false;
        Active(false);
        if (!onloadAbility.GetMark())
        {
            if(st != null) { st.Destroy(); }
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
        if (!b) {
            Destroy(gun.gameObject);
            gun = this.transform;
        }
        else{
            GameObject g = (GameObject)Instantiate(Resources.Load("Objects/Gun"), transform.position, Quaternion.identity);
            gun = g.transform;
        }
        //gun.gameObject.SetActive(b);
    }
    // used to dont move the sloth when shot is on load
    public bool GetShotLoad()
    {
        return this.shotLoad;
    }
    public void SetShotLoad(bool shot)
    {
        this.shotLoad = shot;
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
				Debug.Log("The type of the used projectile is " + a.GetHashCode());
                Debug.Log("The type of the used projectile is " + onLoad.GetType().ToString());
                onloadAbility = a;

				if (a.GetProjectile ().Equals ("autoApply")) {
					onLoad.ApplyLogic ();
                } else {
					Bar ();
					Active (true);
				}
			}
        }
    }
}
