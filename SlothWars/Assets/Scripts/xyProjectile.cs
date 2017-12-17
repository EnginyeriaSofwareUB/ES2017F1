using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xyProjectile :  Projectile
{
	private float HVelocity = 4; //Projectile velocity
	private Vector3 xyDir; //xY aim vector
	private Quaternion rot; // gun rotation
	private float rg = 10; // max range of the projectile
	private float rd= 1; // explosion radius
	private Vector3 pos; // spawn position
	private GameObject hp; //heal ball gameobject
	private bool explosive;
	private string source;
	public xyProjectile(Ability a)
	{
		ability = a;
	}


	// xy trayectory 
	public override void ApplyLogic()
	{
		hp = (GameObject)GameObject.Instantiate(Resources.Load(source), pos + xyDir * 1.15f, rot);
		hp.GetComponent<Rigidbody>().AddForce(xyDir*HVelocity ,ForceMode.VelocityChange);
		Debug.Log (rg + "rangeeeee");
		hp.AddComponent<AbilityContainer> ().SetAbility (ability);
		if (explosive) {
			hp.GetComponent<ExplosionScript> ().SetOrigin (pos);
			hp.GetComponent<ExplosionScript> ().SetRange (rg);
			hp.GetComponent<ExplosionScript> ().SetRadius (rd);
		}
	}
	// needed to set initial parameters
	public override void SetAll(Vector3 positon, Vector3 aimVector,Quaternion rotation,float range, float radius,bool explosive,string source)
	{
		xyDir = aimVector;
		rot = rotation;
		rg = range;
		pos = positon;
		rd = radius;
		this.explosive = explosive;
		this.source = source;
	}
	public override void Mark(){ }
	public override bool GetApply() { return true; }
    public override void CalcelMark() { }
}
