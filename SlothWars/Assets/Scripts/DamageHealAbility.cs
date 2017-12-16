using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DamageHealAbility : Ability
{

    private double dmg;
    private double healHp;
    private float range = 15;
    private float hitRadius = 2f;
    private int ap;
    private string projectile;
    private bool explosive;
    private string source;
    private bool mark;
	private GameController2 gameController;

    public DamageHealAbility(string id, JSONNode json)
    {

        this.dmg = json[id]["dmg"];
        this.healHp = json[id]["healHp"];
        this.range = json[id]["reach"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.explosive = json[id]["explosive"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];

		gameController = GameObject.Find ("Main Camera").GetComponent<GameController2> ();
    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
		if (target.GetTeam() == gameController.GetActualTeam())
        {
            target.SumToHp(healHp);
        }
        else
        {
            target.SumToHp(-dmg);
        }
    }
    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<Sloth>();
		if (target.GetTeam() == gameController.GetActualTeam())
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
        return hitRadius;
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
        return explosive;
    }
    public string GetSource()
    {
        return source;
    }
    public bool GetMark() { return this.mark; }
}