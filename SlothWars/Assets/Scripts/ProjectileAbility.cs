using System;
using SimpleJSON;
using UnityEngine;

public class ProjectileAbility : Ability
{
    private double dmg;
    private int reach = 30;
    private float hitRadius = 2f;
    private int ap;
	private string projectile;
	private bool explosive;
	private string source;
    private bool mark;
    //private int sApoints;
    AbilityController abilityController = AbilityController.Instance;
    public ProjectileAbility(string id, JSONNode json)
    {
        this.mark = json[id]["mark"];
        this.dmg = json[id]["dmg"];
        this.reach = json[id]["reach"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
		this.projectile = json[id]["projectile"];
		this.explosive = json[id]["explosive"];
		this.source = json[id]["source"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-dmg);
		abilityController.UpdateHpBar(target.GetHp(),target.GetShield());
        //GameControl.control.SubtractActionPoints(target, sApints);
    }
    public ProjectileAbility()
    {

    }
    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {

    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return reach;
    }
    public float GetRadius()
    {
        return hitRadius;
    }
    public bool GetBuildTerrain() { return false; }
    public int GetTerrainSize() { return 0; }
	public int GetAp(){
        return ap;
    }

    public bool GetAlterTerrain()
    {
        return false;
    }
	public string GetProjectile(){
		return projectile;
	}
	public bool GetExplosive(){
		return explosive;
	}
	public string GetSource(){
		return source;
	}
    public bool GetMark() { return this.mark; }
}