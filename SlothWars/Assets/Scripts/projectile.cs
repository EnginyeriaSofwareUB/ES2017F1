using System;
using UnityEngine;
public abstract class Projectile
{

    protected Ability ability;

    public Ability GetAbility(){
        return ability;
    } 

    public void SetAbility(Ability ab){
        ability = ab;
    }
    //subject to change
    public abstract void ApplyLogic();
	public abstract void SetAll(Vector3 position, Vector3 aimVector, Quaternion rotation, float range, float radius,bool explosive,string source);
    public abstract void Mark();
    public abstract bool GetApply();
}