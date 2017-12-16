using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PathAbility : Ability
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
    private Vector3 direction;

    public PathAbility(string id, JSONNode json)
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
    public void Apply(GameObject g) {
        GameObject explosion;
        GameObject cube;
        Material m = g.GetComponent<Renderer>().material;
        for (int i = 0; i < terrainSize; i++)
        {
            if (Physics.OverlapBox(g.transform.position + i * scale * direction + direction * scale, new Vector3(1, 1, 1) * (scale - 0.001f) / 2f).Length == 0)
            {
                if (i == 0)
                {
                    GameObject.Instantiate(Resources.Load("Objects/ApplePrefab"), g.transform.position + i * scale * direction + scale * direction +new Vector3(0,0,-1), Quaternion.identity);
                }
                explosion = (GameObject)GameObject.Instantiate(Resources.Load("Objects/SmokeExplosion"), g.transform.position + i * scale * direction + scale * direction, Quaternion.identity);
                GameObject.Destroy(explosion, 3);
                cube = (GameObject)GameObject.Instantiate(Resources.Load("Objects/PathCube"), g.transform.position + i * scale * direction + scale * direction, g.transform.rotation);
                cube.GetComponent<Renderer>().material = m;
            }
        }
    }
    //WIP: apply ability to terrain
    public void Apply(Vector3 position)
    {
        direction = position;
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
