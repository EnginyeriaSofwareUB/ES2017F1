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
        if (projectileID[0] == 'M')
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

        return null;

    }
    public Projectile getProjectile(Ability a)
    {
        if (a is ProjectileAbility)
        {
            return new BulletProjectile();

        }
        else if (a is HealingAbility)
        {
            return new HealingProjectile();

        }
        else if (a is MagicAbility)
        {
            return new MagicProjectile();
        }
        
        return new BulletProjectile();

    }

}
