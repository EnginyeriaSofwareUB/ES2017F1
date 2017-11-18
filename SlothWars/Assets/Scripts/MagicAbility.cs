using System;
using SimpleJSON;

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
    }
    public MagicAbility()
    {

    }
    //Apply ability to another sloth
    public void Apply(ref Sloth target)
    {
        target.SumToHp(-dmg);
        //GameControl.control.SumToHpBar(target,-dmg);
        //GameControl.control.SumResidual(target, -residual, residualTurns);
    }

    //WIP: apply ability to terrain
    public void Apply()
    {
    }
    public float GetRange()
    {
        return reach;
    }
    public float GetRadius()
    {
        return hitRadius;
    }
}
