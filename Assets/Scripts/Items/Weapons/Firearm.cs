using UnityEngine;

public class Firearm : Weapon
{
    public int ammo;
    public GameObject projectile;

    protected float projectileSpeed;
    private float lastfired;

    public Firearm()
    {
        ammo = 0;
        automatic = false;
        fireRate = 0f;
        accuracyVariance = 0f;
        projectileSpeed = 10000f;
    }

    public override bool Use()
    {
        if (ammo > 0)
        {
            if (base.Use())
            {
                if (projectile != null)
                {
                    GameObject bullet = Instantiate(projectile, weaponHand.position, weaponHand.rotation);
                    Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
                    bulletBody.AddForce(fireDirection * projectileSpeed);
                    --ammo;
                }
                return true;
            }
        }
        else
        {
            print("Out of Ammo!");
        }
        return false;
    }
}
