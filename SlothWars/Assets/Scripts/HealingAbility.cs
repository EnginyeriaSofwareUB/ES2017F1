using System;
using SimpleJSON;
using UnityEngine;
public class HealingAbility : Ability
{

    private double shield;
    private int durShield;
    private double healHp;
    private bool healEffects;
    private double boostDef;
    private int durBoostDef;
    private float range = 15;
    private float hitRadius = 2f;
    private int ap;
    private string projectile;
    private bool explosive;
    private string source;
    private bool mark;
    AbilityController abilityController = AbilityController.Instance;
    public HealingAbility(string id, JSONNode json)
    {

        this.shield = json[id]["shield"];
        this.durShield = json[id]["durShield"];
        this.healHp = json[id]["healHp"];
        this.healEffects = json[id]["healEffects"];
        this.boostDef = json[id]["boostDef"];
        this.durBoostDef = json[id]["durBoostDef"];
        this.range = json[id]["reach"];
        this.hitRadius = json[id]["hitRadius"];
        this.ap = json[id]["ap"];
        this.projectile = json[id]["projectile"];
        this.explosive = json[id]["explosive"];
        this.source = json[id]["source"];
        this.mark = json[id]["mark"];
    }
    public HealingAbility()
    {

    }

    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(healHp);
        abilityController.UpdateHpBar(target.GetHp(), target.GetShield());
    }
    //WIP: apply ability to terrain
    public void Apply(GameObject g)
    {
        Sloth target = g.GetComponent<AnimPlayer>().sloth;
        target.SumToHp(healHp);
        abilityController.UpdateHpBar(target.GetHp(), target.GetShield());
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
    public bool GetBuildTerrain() { return false; }
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