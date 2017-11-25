using System;
using UnityEngine;

public class HealingProjectile :  Projectile
{
    private float HVelocity = 4; //Projectile velocity
    private Vector3 xyDir; //xY aim vector
    private Quaternion rot; // gun rotation
    private float rg = 10; // max range of the projectile
    private float rd= 1; // explosion radius
    private Vector3 pos; // spawn position
    private GameObject hp; //heal ball gameobject
    public HealingProjectile()
	{
        
	}
   

    // xy trayectory 
    public void ApplyLogic()
    {
        hp = (GameObject)GameObject.Instantiate(Resources.Load("Objects/HealBall"), pos + xyDir * 1.15f, rot);
        hp.GetComponent<Rigidbody>().AddForce(xyDir*HVelocity ,ForceMode.VelocityChange);
        hp.GetComponent<ExplosionScript>().SetOrigin(pos);
        hp.GetComponent<ExplosionScript>().SetRange(rg);
        hp.GetComponent<ExplosionScript>().SetRadius(rd);
    }
    // needed to set initial parameters
    public void SetAll(Vector3 positon, Vector3 aimVector,Quaternion rotation,float range, float radius)
    {
        xyDir = aimVector;
        rot = rotation;
        rg = range;
        pos = positon;
        rd = radius;
    }
    public void Mark(){ }
    public bool GetApply() { return true; }
}
