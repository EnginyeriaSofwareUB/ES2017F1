using System;
using UnityEngine;
using SimpleJSON;

public class AbilityFactory
{
    private static AbilityFactory instance;

    private AbilityFactory()
	{
	}

    public static AbilityFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AbilityFactory();
            }
            return instance;
        }
    }

    public Ability getAbility(string abilityID)
    {

        string s = ((TextAsset)Resources.Load("slothability")).text;
        JSONNode n = JSON.Parse(s);

        if (abilityID[0] == 'M')
        {
            return new MagicAbility(abilityID, s);

        }
        else if (abilityID[0] == 'T')
        {
            return new TankAbility(abilityID, s);

        }
        else if (abilityID[0] == 'P')
        {
            return new ProjectileAbility(abilityID, s);
        }
        else if (abilityID[0] == 'H')
        {
            return new HealingAbility(abilityID, s);
        }
        else if (abilityID[0] == 'U')
        {
            return new UtilityAbility(abilityID, s);
        }

        return null;
    }

}
