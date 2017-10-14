using System;
using SimpleJSON;

public class ProjectileAbility: Ability
{
    private double dmg;
    private int reach;

	public ProjectileAbility(string id, JSONNode json)
	{
        this.dmg = json[id]["dmg"];
        this.reach = json[id]["reach"];
	}
}
