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

	public HealingAbility(string id, JSONNode json)
	{

        this.shield = json[id]["shield"];
        this.durShield = json[id]["durShield"];
        this.healHp = json[id]["healHp"];
        this.healEffects = json[id]["healEffects"];
        this.boostDef = json[id]["boostDef"];
        this.durBoostDef = json[id]["durBoostDef"];
}
}
