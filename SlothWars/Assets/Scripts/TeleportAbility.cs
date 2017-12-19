using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TeleportAbility : Ability
{
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    private float range;
    public TeleportAbility(string id, JSONNode json)
    {
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
        this.range = json[id]["reach"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {

    }
    public void Apply(GameObject g) { }
    //WIP: apply ability to terrain
    public void Apply(Vector3 position)
    {}
    public float GetRange() { return range; }
    public float GetRadius() { return 1; }
    public bool GetBuildTerrain() { return false; }
    public int GetTerrainSize() { return 0; }
    public int GetAp()
    {
        return ap;
    }

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
    public bool GetMark() { return this.mark; }
}