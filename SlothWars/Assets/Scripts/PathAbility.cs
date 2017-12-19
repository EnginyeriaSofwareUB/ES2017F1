using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class PathAbility : Ability
{
    private float scale = 1;
    private bool buildTerrain;
    private int terrainSize;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    private Vector3 direction;
    private double fruitHeal;
    private int fruitAp;
    private float range;
    public PathAbility(string id, JSONNode json)
    {
        this.buildTerrain = json[id]["buildTerrain"];
        this.terrainSize = json[id]["terrainSize"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
        this.fruitHeal = json[id]["fruitHeal"];
        this.fruitAp = json[id]["fruitAp"];
        this.range = json[id]["reach"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {

    }
    public void Apply(GameObject g) {
        GameObject explosion;
        GameObject cube;
        GameObject fruit;
        Material m = g.GetComponent<Renderer>().material;
        for (int i = 0; i < terrainSize; i++)
        {
            if (Physics.OverlapBox(g.transform.position + i * scale * direction + direction * scale, new Vector3(1, 1, 1) * (scale - 0.001f) / 2f).Length == 0)
            {
                explosion = (GameObject)GameObject.Instantiate(Resources.Load("Objects/SmokeExplosion"), g.transform.position + i * scale * direction + scale * direction, Quaternion.identity);
                GameObject.Destroy(explosion, 3);
                cube = (GameObject)GameObject.Instantiate(Resources.Load("Objects/PathCube"), g.transform.position + i * scale * direction + scale * direction, g.transform.rotation);
                cube.GetComponent<Renderer>().material = m;
                if (i == 0)
                {
                    fruit = (GameObject)GameObject.Instantiate(Resources.Load("Objects/ApplePrefab"), g.transform.position + i * scale * direction + scale * direction + new Vector3(0, 0, -0.75f), Quaternion.identity);
                    fruit.GetComponent<FruitScript>().SetParams(fruitHeal, fruitAp);
                    fruit.GetComponent<FruitScript>().SetCube(cube.transform);
                }
            }
        }
    }
    //WIP: apply ability to terrain
    public void Apply(Vector3 position)
    {
        direction = position;
    }
    public float GetRange() { return range; }
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
