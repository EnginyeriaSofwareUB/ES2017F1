using System;

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
        if (abilityID[0] == 'M')
        {
            return new MagicAbility();

        }
        else if (abilityID[0] == 'T')
        {
            return new TankAbility();

        }
        else if (abilityID[0] == 'P')
        {
            return new ProjectileAbility();
        }
        else if (abilityID[0] == 'H')
        {
            return new HealingAbility();
        }
        else if (abilityID[0] == 'U')
        {
            return new UtilityAbility();
        }

        return null;
    }

}
