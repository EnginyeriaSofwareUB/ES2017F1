using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DecrementApAbility : Ability
{
    private double dmg;
    private double residual;
    private int reach = 15;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    private int apDecrement;

    public DecrementApAbility(string id, JSONNode json)
    {

        this.dmg = json[id]["dmg"];
        this.reach = json[id]["reach"];
        this.ap = json[id]["ap"];
        this.apDecrement = json[id]["apDecrement"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
    }
    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-dmg);
        //abilityController.SumResidual(target, -residual, residualTurns);
    }

    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<Sloth>();
        target.SumToHp(-dmg);
        target.SumExtraAp(-apDecrement);
        GameObject lightning = (GameObject)GameObject.Instantiate(Resources.Load("Objects/LightningBolt/DecrementApLightning"), g.transform.position, Quaternion.identity);
        lightning.GetComponentInChildren<Transform>().Find("LightningStart").position = Camera.main.GetComponent<GameController>().GetCurrentSloth().transform.position;
        lightning.GetComponentInChildren<Transform>().Find("LightningEnd").position = g.transform.position;
        GameObject.Destroy(lightning, 2);
    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return reach;
    }
    public float GetRadius()
    {
        return 1;
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
