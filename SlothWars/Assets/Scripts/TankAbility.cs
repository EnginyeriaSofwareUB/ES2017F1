using System;
using SimpleJSON;
using UnityEngine;
public class TankAbility : Ability
{
    private bool shield;
    private double hpShield;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    private float range;
    public TankAbility(string id, JSONNode json)
    {

        this.shield = json[id]["shield"];
        this.mark = json[id]["mark"];
        this.hpShield = json[id]["hpShield"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.range = json[id]["reach"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SetShield(hpShield);
    }

    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Debug.Log("apply Tank");
        Sloth target = g.GetComponent<Sloth>();
        target.SetShield(hpShield);
    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return range;
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