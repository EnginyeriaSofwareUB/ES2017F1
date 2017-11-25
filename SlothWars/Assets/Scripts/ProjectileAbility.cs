using System;
using SimpleJSON;

public class ProjectileAbility : Ability
{
    private double dmg;
    private int reach = 30;
    private float hitRadius = 2f;
    private int ap;
    //private int sApoints;

    public ProjectileAbility(string id, JSONNode json)
    {
        this.dmg = json[id]["dmg"];
        this.reach = json[id]["reach"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-dmg);
        //GameControl.control.SumToHpBar(target, -dmg);
        //GameControl.control.SubtractActionPoints(target, sApints);
    }
    public ProjectileAbility()
    {

    }
    //WIP: apply ability to terrain
    public void Apply()
    {

    }
    public float GetRange()
    {
        return reach;
    }
    public float GetRadius()
    {
        return hitRadius;
    }

    public int GetAp(){
        return ap;
    }

    public bool GetAlterTerrain()
    {
        return false;
    }
}