using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletProjectile : Projectile
{
    private float bVelocity = 8f; //Projectile velocity
    private Vector3 xyDir; //xY aim vector
    private float zO = 0; //sloths plane
    private float zf = 0.5f; //tree plane
    private float rg = 50; // max range
    private float rd = 1; // explosion radius
    private Vector3 pos; // spawn position
    private Quaternion rot; // gun rotation
    GameObject ea; // electric arrow gameobject
    public BulletProjectile()
    {

    }
    // xyz trajectory
    public void ApplyLogic()
    {
        float zd = zf - zO;
        float vz = Mathf.Sqrt(bVelocity * bVelocity * zd * zd / (zd * zd + rg * rg));
        Vector3 vVector = xyDir * Mathf.Sqrt(bVelocity * bVelocity - vz * vz) + new Vector3(0, 0, vz);
        ea = (GameObject)GameObject.Instantiate(Resources.Load("Objects/Electric Arrow"), pos + vVector.normalized * 1.15f, rot);
        ea.GetComponent<Transform>().Rotate(0, 90, 0);
        ea.GetComponent<Rigidbody>().AddForce(vVector , ForceMode.VelocityChange);
        ea.GetComponent<ExplosionScript>().SetRadius(rd);
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
    public void Mark() { }
    public bool GetApply() { return true; }
}