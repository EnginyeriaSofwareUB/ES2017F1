using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TeleportAbility : Ability
{
    private float scale = 1;
    private bool buildTerrain;
    private string terrainType;
    private int terrainSize;
    private bool alterTerrain;
    private int durAlterTerrain;
    private int alterationSize;
    private double boostAp;
    private double boostAtt;
    private int durBoostAtt;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;

    public TeleportAbility(string id, JSONNode json)
    {
        this.buildTerrain = json[id]["buildTerrain"];
        this.terrainType = json[id]["terrainType"];
        this.terrainSize = json[id]["terrainSize"];
        this.alterTerrain = json[id]["alterTerrain"];
        this.durAlterTerrain = json[id]["durAlterTerrain"];
        this.alterationSize = json[id]["alterationSize"];
        this.boostAp = json[id]["boostAp"];
        this.boostAtt = json[id]["boostAtt"];
        this.durBoostAtt = json[id]["durBoostAtt"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {

    }
    public void Apply(GameObject g) { }
    //WIP: apply ability to terrain
    public void Apply(Vector3 position)
    {}
    public float GetRange() { return 10; }
    public float GetRadius() { return 1; }
    public bool GetBuildTerrain() { return buildTerrain; }
    public int GetTerrainSize() { return terrainSize; }
    public int GetAp()
    {
        return ap;
    }

    public bool GetAlterTerrain()
    {
        return "true".Equals(this.alterTerrain);
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