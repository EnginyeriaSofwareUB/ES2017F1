using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoApplyProjectile : Projectile {
	private string source;

	public AutoApplyProjectile(Ability a){
		ability = a;
	}

	// xy trayectory 
	public override void ApplyLogic()
	{
		GameObject s = GameObject.Find("Main Camera").GetComponent<GameController>().GetCurrentSloth().gameObject;
        ability.Apply(s); 
	}
	// needed to set initial parameters
	public override void SetAll(Vector3 positon, Vector3 aimVector,Quaternion rotation,float range, float radius,bool explosive,string source)
	{
		
		this.source = source;
	}
	public override void Mark(){ }
	public override bool GetApply() { return true; }
}
