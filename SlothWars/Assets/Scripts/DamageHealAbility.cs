using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DamageHealAbility : Ability
{

    private double dmg;
    private double healHp;
    private float range = 15;
    private int ap;
    private string projectile;
    private string source;
    private bool mark;
    public DamageHealAbility(string id, JSONNode json)
    {

        this.dmg = json[id]["dmg"];
        this.healHp = json[id]["healHP"];
        this.range = json[id]["reach"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        Debug.Log("dam/heal");
        if (target.GetTeam() == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam())
        {
            Debug.Log("heal");
            target.SumToHp(healHp);
        }
        else
        {
            Debug.Log("damage");
            target.SumToHp(-dmg);
        }
    }
    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<Sloth>();
        if (target.GetTeam() == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam())
        {
            target.SumToHp(healHp);
        }
        else
        {
            target.SumToHp(-dmg);
        }
    }
    public void Apply(Vector3 p) { }
    public float GetRange()
    {
        return range;
    }
    public float GetRadius()
    {
        return 0;
    }
    public bool GetBuildTerrain() { return true; }
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