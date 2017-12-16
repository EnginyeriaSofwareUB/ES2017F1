using System;
using SimpleJSON;
using UnityEngine;
public class TankAbility : Ability
{
    private bool shield;
    private double boostDef;
    private int durBoostDef;
    private double boostHp;
    private int durBoostHp;
    private double hpShield;
    private bool blockAb;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    public TankAbility(string id, JSONNode json)
    {

        this.shield = json[id]["shield"];
        this.mark = json[id]["mark"];
        this.boostDef = json[id]["boostDef"];
        this.durBoostDef = json[id]["durBoostDef"];
        this.boostHp = json[id]["boostHp"];
        this.durBoostHp = json[id]["durBoosthp"];
        this.hpShield = json[id]["hpShield"];
        this.blockAb = json[id]["blockAb"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SetShield(hpShield);
    }

    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<Sloth>();
        target.SetShield(hpShield);
    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return 10;
    }
    public float GetRadius()
    {
        return 1;
    }
    public bool GetBuildTerrain() { return false; }
    public int GetTerrainSize() { return 0; }
    public int GetAp()
    {
        return ap;
    }
    public bool GetMark() { return this.mark; }
    public bool GetAlterTerrain()
    {
        return false;
    }
    public string GetProjectile()
    {
        return projectile;
    }
    public bool GetExplosive()
    {
        return false;
    }
    public string GetSource()
    {
        return source;
    }
}