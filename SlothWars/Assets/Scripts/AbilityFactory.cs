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

		if (abilityID [0] == 'M') {
			return new MagicAbility (abilityID, n);

		} else if (abilityID [0] == 'T') {
			return new TankAbility (abilityID, n);

		} else if (abilityID [0] == 'P') {
			return new ProjectileAbility (abilityID, n);
		} else if (abilityID [0] == 'H') {
			return new HealingAbility (abilityID, n);
		} else if (abilityID [0] == 'U') {
            switch (abilityID[1])
            {
                case '0':
                    return new UtilityAbility(abilityID, n);
                case '1':
                    return new PathAbility(abilityID, n);
                case '2':
                    return new TeleportAbility(abilityID, n);
                default: return null;
            }
		} else if (abilityID [0] == 'D') {
			return new DamageHealAbility (abilityID, n);
		}
        else if (abilityID[0] == 'L')
        {
            return new LinkAbility(abilityID, n);
        }
        else if (abilityID[0] == 'V')
        {
            return new VenomAbility(abilityID, n);
        }
        else if (abilityID[0] == 'A')
        {
            return new DecrementApAbility(abilityID, n);
        }
        return null;
    }

}
