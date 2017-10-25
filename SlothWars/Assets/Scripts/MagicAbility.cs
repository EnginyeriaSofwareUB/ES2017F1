using System;
using SimpleJSON;

public class MagicAbility: Ability
{
    private string elem;
    private string onHitEff;
    private double onHitProb;
    private double dmg;
    private string distance;
    private bool alterTerrain;
    private int reach;

    public MagicAbility(string id, JSONNode json)
    {

        this.elem = json[id]["elem"];
        this.onHitEff = json[id]["onHit"];
        this.onHitProb = json[id]["onHitProb"];
        this.dmg = json[id]["dmg"];
        this.distance = json[id]["distance"];
        this.alterTerrain = json[id]["alterTerrain"];
        this.reach = json[id]["reach"];
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
