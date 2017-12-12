using System;

public class ProjectileFactory
{
    private static ProjectileFactory instance;

    private ProjectileFactory()
    {
    }

    public static ProjectileFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProjectileFactory();
            }
            return instance;
        }
    }

    //Subject to change
    //Pass abilityID to instantiate a projectile
    public Projectile getProjectile(string projectileID)
    {
       /* if (projectileID[0] == 'M')
        {
            return new MagicProjectile();

        }
        else if (projectileID[0] == 'T')
        {
            return null;

        }
        else if (projectileID[0] == 'P')
        {
            return new BulletProjectile();
        }
        else if (projectileID[0] == 'H')
        {
            return new HealingProjectile();
        }
        else if (projectileID[0] == 'U')
        {
            return null;
        }
	*/
        return null; 

    }
    public Projectile getProjectile(Ability a)
    {
		if (a.GetProjectile().Equals("xyz")) {
			return new xyzProjectile ();

		} else if (a.GetProjectile ().Equals("xy")) {
			return new xyProjectile ();

		} else if (a.GetProjectile ().Equals("terrain")) {
			return new ProjectileTerrain ();
		} else if (a.GetProjectile ().Equals("targetChain")) {
			return new ChainProjectile ();
		} else if (a.GetProjectile ().Equals("autoApply")) {
			return new AutoApplyProjectile ();
		} else {
			return null;
		}

    }

}