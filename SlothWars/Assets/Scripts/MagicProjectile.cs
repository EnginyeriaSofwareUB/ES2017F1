using System;
using UnityEngine;
public class MagicProjectile:  Projectile
{
    private float MVelocity = 3.2f; //Projectile velocity
    private Vector3 xyDir; //xY aim vector
    private float zO = 0; //sloths plane
    private float zf = 1; //tree plane
    private float rg = 20; // max range
    private float rd = 1; // explosion radius
    private Vector3 pos; // spawn position
    private Quaternion rot; // gun rotation
    GameObject fb; // fireball gameobject
    public MagicProjectile()
	{
	}

    // xyz trayectory
    public void ApplyLogic()
    {
        float zd = zf - zO;
        float vz = Mathf.Sqrt(MVelocity * MVelocity * zd * zd / (zd * zd + rg * rg));
        Vector3 vVector = xyDir * Mathf.Sqrt(MVelocity * MVelocity - vz * vz) + new Vector3(0, 0, vz);
        GameObject fb = (GameObject)GameObject.Instantiate(Resources.Load("Objects/FireBall"), pos + xyDir * 1.15f, rot);
        fb.GetComponent<Rigidbody>().AddForce(vVector * MVelocity, ForceMode.VelocityChange);
        fb.GetComponent<ExplosionScript>().SetRadius(rd);

    }
    // needed to set initial parameters
    public void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius)
    {
        xyDir = aimVector;
        rot = rotation;
        rg = range;
        pos = positon;
        rd = radius;
    }
}
