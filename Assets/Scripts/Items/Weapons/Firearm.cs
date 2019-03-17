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

    public override void Use()
    {
        if (ammo > 0)
        {
            //if (base.Use())
            {
                if (projectile != null)
                {
                    GameObject bullet = Instantiate(projectile, fireFrom.position, fireFrom.rotation);
                    Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
                    bulletBody.AddForce(fireDirection * projectileSpeed);
                    --ammo;
                }
            }
        }
        else
        {
            print("Out of Ammo!");
        }
    }
}
