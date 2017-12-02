using System;
using SimpleJSON;
using UnityEngine;

public class MagicAbility: Ability
{
    private string elem;
    private string onHitEff;
    private double onHitProb;
    private double dmg;
    private double residual;
    private string distance;
    private bool alterTerrain;
    private int reach =15;
    private int residualTurns;
    private float hitRadius = 2f;
    private int ap;
    AbilityController abilityController = AbilityController.Instance;

    public MagicAbility(string id, JSONNode json)
    {

        this.elem = json[id]["elem"];
        this.onHitEff = json[id]["onHit"];
        this.onHitProb = json[id]["onHitProb"];
        this.dmg = json[id]["dmg"];
        this.distance = json[id]["distance"];
        this.residual = json[id]["residual"];
        this.alterTerrain = json[id]["alterTerrain"];
        this.reach = json[id]["reach"];
        this.residualTurns = json[id]["residual turns"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
    }
    public MagicAbility()
    {
    }
    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-dmg);
        abilityController.SumToHpBar(-dmg);
        //abilityController.SumResidual(target, -residual, residualTurns);
    }

    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return reach;
    }
    public float GetRadius()
    {
        return hitRadius;
    }
    public bool GetBuildTerrain() { return false; }
	
    public int GetTerrainSize() { return 0; }
	
	public int GetAp(){
        //Debug.Log("MAGIC ABILITY AP: "+ap);
        //Debug.Log("onHit AP: "+onHitEff);
        //Debug.Log("dmg AP: "+dmg);
        return ap;
    }

    public bool GetAlterTerrain() {
        return "true".Equals(this.alterTerrain);
    }
}
