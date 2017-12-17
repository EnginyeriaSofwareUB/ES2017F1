using System;
using SimpleJSON;
using UnityEngine;

public class UtilityAbility : Ability
{
    private float scale = 1;
    private bool buildTerrain;
    private int terrainSize;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;

    public UtilityAbility(string id, JSONNode json)
    {
        this.buildTerrain = json[id]["buildTerrain"];
        this.terrainSize = json[id]["terrainSize"];
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
    {
        GameObject explosion;
        for (int i = 0; i < terrainSize; i++)
        {
            explosion = (GameObject) GameObject.Instantiate(Resources.Load("Objects/snowExplosion"), position + i * scale * new Vector3(0, 1, 0), Quaternion.identity);
            GameObject.Destroy(explosion, 3);
            GameObject.Instantiate(Resources.Load("Objects/WallCube"), position + i * scale * new Vector3(0, 1, 0), Quaternion.identity);
        }

    }
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