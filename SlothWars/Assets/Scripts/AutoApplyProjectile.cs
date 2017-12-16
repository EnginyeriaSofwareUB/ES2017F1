using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoApplyProjectile : Projectile {
	private string source;
	public AutoApplyProjectile()
	{

	}


	// xy trayectory 
	public void ApplyLogic()
	{
		GameObject s = GameObject.Find("Main Camera").GetComponent<GameController2>().GetCurrentSloth().gameObject;
		//GameObject effect = (GameObject)GameObject.Instantiate (Resources.Load (source), s.transform);
		//GameObject.Destroy (effect, 3);
		//TODO:FIX THIS
		//abilityController.ApplyLastAbility (s);
	}
	// needed to set initial parameters
	public void SetAll(Vector3 positon, Vector3 aimVector,Quaternion rotation,float range, float radius,bool explosive,string source)
	{
		
		this.source = source;
	}
	public void Mark(){ }
	public bool GetApply() { return true; }
}
