using System;
using SimpleJSON;
using UnityEngine;

public class UtilityAbility : Ability
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

    public UtilityAbility(string id, JSONNode json)
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
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {

    }

    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Debug.Log("building wall");
        Vector3 position = new Vector3(0, 0, 0);
        for(int i= 0; i< terrainSize; i++)
        {
            GameObject.Instantiate(Resources.Load("Objects/WallCube"),g.transform.position+i* scale*new Vector3(0, 1, 0) + 2*scale*new Vector3(0,1,0)/2,g.transform.rotation);
        }

    }
    public float GetRange() { return 10; }
    public float GetRadius(){return 1;}
    public bool GetBuildTerrain() { return buildTerrain; }
    public int GetTerrainSize() { return terrainSize; }
	public int GetAp(){
        return ap;
    }

    public bool GetAlterTerrain()
    {
        return "true".Equals(this.alterTerrain);
    }
}