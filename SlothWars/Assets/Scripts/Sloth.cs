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
    Ability ab1;
    Ability ab2;
    Ability ab3;


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
                end = true;
            }
            i++;
        }
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
    }

    public string getType()
    {
        return this.typeName;
    }

}
