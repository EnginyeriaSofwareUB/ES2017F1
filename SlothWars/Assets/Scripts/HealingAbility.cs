using System;
using SimpleJSON;

public class HealingAbility: Ability
{
    private double shield;
    private int durShield;
    private double healHp;
    private bool healEffects;
    private double boostDef;
    private int durBoostDef;
    private float range = 15;
    private float hitRadius= 2f;

	public HealingAbility(string id, JSONNode json)
	{

        this.shield = json[id]["shield"];
        this.durShield = json[id]["durShield"];
        this.healHp = json[id]["healHp"];
        this.healEffects = json[id]["healEffects"];
        this.boostDef = json[id]["boostDef"];
        this.durBoostDef = json[id]["durBoostDef"];
        this.range = json[id]["range"];
        this.hitRadius = json[id]["hitRadius"];
    }
    public HealingAbility()
    {

    }

    //Apply ability to another sloth
    public void Apply(Sloth target)
    {
        target.SumToHp(healHp);
        GameControl.control.SumToHpBar(target, healHp);
    }
    //WIP: apply ability to terrain
    public void Apply()
    {
    }
    public float GetRange()
    {
        return range;
    }
    public float GetRadius()
    {
        return hitRadius;
    }

}
