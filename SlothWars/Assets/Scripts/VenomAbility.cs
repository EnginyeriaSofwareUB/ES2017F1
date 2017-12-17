using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class VenomAbility : Ability
{
    private double dmgOnHit;
    private double residual;
    private bool alterTerrain;
    private int reach = 15;
    private int residualTurns;
    private float hitRadius = 2f;
    private int ap;
    private string projectile;
    private string source;
    private bool explosive;
    private bool mark;

    public VenomAbility(string id, JSONNode json)
    {
        this.dmgOnHit = json[id]["dmgOnHit"];
        this.residual = json[id]["residual"];
        this.alterTerrain = json[id]["alterTerrain"];
        this.reach = json[id]["reach"];
        this.residualTurns = json[id]["residualTurns"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.explosive = json[id]["explosive"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
    }
    public VenomAbility()
    {
    }
    //Apply venom
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-residual);
        //abilityController.SumResidual(target, -residual, residualTurns);
    }

    //WIP: apply onHit
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<Sloth>();
        target.SumToHp(-dmgOnHit);
        Camera.main.GetComponent<GameController>().AddVenom(new Venom(g.GetComponent<Sloth>(), this, residualTurns));

    }
    public void Apply(Vector3 p) {

    }
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

    public int GetAp()
    {
        //Debug.Log("MAGIC ABILITY AP: "+ap);
        //Debug.Log("onHit AP: "+onHitEff);
        //Debug.Log("dmg AP: "+dmg);
        return ap;
    }

    public bool GetAlterTerrain()
    {
        return this.alterTerrain;
    }
    public string GetProjectile()
    {
        return projectile;
    }
    public bool GetExplosive()
    {
        return explosive;
    }
    public string GetSource()
    {
        return source;
    }
    public bool GetMark() { return this.mark; }
}