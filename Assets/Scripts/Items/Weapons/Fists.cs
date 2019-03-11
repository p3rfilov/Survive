using UnityEngine;

public class Fists : Weapon
{
    int ammo;
    GameObject projectile;

    public Fists()
    {
        maxDamage = 2;
        minDamage = 1;
        force = 3f;
        effectiveDistance = 0.5f;
        automatic = true;
        fireRate = 2f;
    }
}
