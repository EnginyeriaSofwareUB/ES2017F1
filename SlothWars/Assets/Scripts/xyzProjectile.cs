using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xyzProjectile : Projectile {

	private float MVelocity = 8; //Projectile velocity
	private Vector3 xyDir; //xY aim vector
	private float zO = 0; //sloths plane
	private float zf = 0.5f; //tree plane
	private float rg = 20; // max range
	private float rd = 1; // explosion radius
	private Vector3 pos; // spawn position
	private Quaternion rot; // gun rotation
	GameObject fb; // fireball gameobject
	private string source;
	private bool explosive;
	public xyzProjectile()
	{
	}

	// xyz trayectory
	public void ApplyLogic()
	{
		Debug.Log(source);
		float zd = zf - zO;
		float vz = Mathf.Sqrt(MVelocity * MVelocity * zd * zd / (zd * zd + rg * rg));
		Vector3 vVector = xyDir * Mathf.Sqrt(MVelocity * MVelocity - vz * vz) + new Vector3(0, 0, vz);
		GameObject fb = (GameObject)GameObject.Instantiate(Resources.Load(source), pos + vVector.normalized * 1.15f, rot);
		fb.GetComponent<Transform>().Rotate(0, 90, 0);
		fb.GetComponent<Rigidbody>().AddForce(vVector , ForceMode.VelocityChange);
		if (explosive) {
			fb.GetComponent<ExplosionScript> ().SetRadius (rd);
            fb.GetComponent<ExplosionScript>().SetDirection(vVector.normalized);
        }
        else if (fb.GetComponent<AxeScript>() != null) { 
			fb.GetComponent<AxeScript> ().SetDirection (vVector.normalized);
		}
        else if (fb.GetComponent<DrillArrow>() != null)
        {
            fb.GetComponent<DrillArrow>().SetDirection(vVector.normalized);
        }

    }
	// needed to set initial parameters
	public void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius,bool exp,string source)
	{
		xyDir = aimVector;
		rot = rotation;
		rg = range;
		pos = positon;
		rd = radius;
		this.explosive = exp;
		Debug.Log (exp);
		this.source = source;
	}
	public void Mark() { }
	public bool GetApply() { return true; }
}
