using System;
using SimpleJSON;

public class UtilityAbility : Ability
{

    private bool buildTerrain;
    private string terrainType;
    private int terrainSize;
    private bool alterTerrain;
    private int durAlterTerrain;
    private int alterationSize;
    private double boostAp;
    private double boostAtt;
    private int durBoostAtt;

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
    }

    //Apply ability to another sloth
    public void apply(Sloth target)
    {

    }

    //WIP: apply ability to terrain
    public void apply()
    {
    }
}
