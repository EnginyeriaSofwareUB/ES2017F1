using System;
using UnityEngine;
using SimpleJSON;

public class Sloth
{
    private double attack;
    private double defense;
    private int ap;
    private double hp;
    private int id;
    private string typeName;
    private string idAb1;
    private string idAb2;
    private string idAb3;
    private string sprite;
    Ability ab1;
    Ability ab2;
    Ability ab3;

    AbilityFactory factory = AbilityFactory.Instance;


	public Sloth(string name)
	{
        string s = ((TextAsset)Resources.Load("slothapedia")).text;
        JSONNode n = JSON.Parse(s);
        int len = n.Count;
        bool end = false;
        int i = 0;

        while(i < len && !end)
        {
            if (n[i]["type"].Equals(name))
            {
                this.attack = n[i]["att"];
                this.defense = n[i]["def"];
                this.hp = n[i]["hp"];
                this.ap = n[i]["ap"];
                this.typeName = n[i]["type"];
                this.idAb1 = n[i]["idAb1"];
                this.idAb2 = n[i]["idAb2"];
                this.idAb3 = n[i]["idAb3"];
                this.sprite = n[i]["photo"];
                end = true;
            }
            i++;
        }
		/*
        this.ab1 = factory.getAbility(idAb1);
        this.ab2 = factory.getAbility(idAb2);
        this.ab3 = factory.getAbility(idAb3);
        */
    }

    public Sloth(int numId)
    {
        
        string s = ((TextAsset)Resources.Load("slothapedia")).text;
        JSONNode n = JSON.Parse(s);

        string id = numId.ToString();
        this.attack = n[id]["att"];
        this.defense = n[id]["def"];
        this.hp = n[id]["hp"];
        this.ap = n[id]["ap"];
        this.typeName = n[id]["type"];
        this.idAb1 = n[id]["idAb1"];
        this.idAb2 = n[id]["idAb2"];
        this.idAb3 = n[id]["idAb3"];
        this.sprite = n[id]["photo"];

        this.ab1 = factory.getAbility(idAb1);
        this.ab2 = factory.getAbility(idAb2);
        this.ab3 = factory.getAbility(idAb3);
    }

    public string GetTypeName()
    {
        return this.typeName;
    }

    public string GetSprite()
    {
        return this.sprite;
    }

	public double GetHp(){
		return this.hp;
	}

}
