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
        if (a.GetProjectile().Equals("xyz"))
        {
            return new xyzProjectile(a);

        }
        else if (a.GetProjectile().Equals("xy"))
        {
            return new xyProjectile(a);

        }
        else if (a.GetProjectile().Equals("terrain"))
        {
            return new ProjectileTerrain(a);
        }
        else if (a.GetProjectile().Equals("path"))
        {
            return new PathProjectile(a);
        }
        else if (a.GetProjectile().Equals("teleport"))
        {
            return new TeleportProjectile(a);
        }
        else if (a.GetProjectile().Equals("targetChain"))
        {
            return new ChainProjectile(a);
        }
        else if (a.GetProjectile().Equals("autoApply"))
        {
            return new AutoApplyProjectile(a);
        }
        else if (a.GetProjectile().Equals("allyTarget"))
        {
            return new TargetTeamProjectile(a);
        }
        else if (a.GetProjectile().Equals("link"))
        {
            return new LinkProjectile(a);
        }
        else
        {
            return null;
        }

    }

}