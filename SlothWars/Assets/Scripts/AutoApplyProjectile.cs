using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoApplyProjectile : Projectile {
	private string source;
	AbilityController abilityController = AbilityController.Instance;
	public AutoApplyProjectile()
	{

	}


	// xy trayectory 
	public void ApplyLogic()
    { 
		abilityController.ApplyLastAbility (Camera.main.GetComponent<GameController2>().GetCurrentSloth().gameObject);
	}
	// needed to set initial parameters
	public void SetAll(Vector3 positon, Vector3 aimVector,Quaternion rotation,float range, float radius,bool explosive,string source)
	{
		
		this.source = source;
	}
	public void Mark(){ }
	public bool GetApply() { return true; }
}
