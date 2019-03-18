using UnityEngine;

[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(ProjectileShooter))]
[RequireComponent(typeof(DamageCalculator))]
public class Pistol : Weapon
{
    private Ammo ammo;
    private ProjectileShooter projectileShooter;
    private DamageCalculator damageCalculator;

    public Pistol()
    {
        force = 10f;
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 2f;
    }

    public override void Start()
    {
        base.Start();
        ammo = GetComponent<Ammo>();
        projectileShooter = GetComponent<ProjectileShooter>();
        damageCalculator = GetComponent<DamageCalculator>();
    }

    public override void Use()
    {
        if (ammo.ammo > 0 && CanUse())
        {
            Vector3 dir;
            Transform hit;

            ammo.SpendAmmo();
            dir = RayShooter.GetRandomHorizontalDirection(fireFrom, accuracyVariance);
            hit = RayShooter.ShootRay(fireFrom.position, dir, effectiveDistance);
            projectileShooter.Shoot(fireFrom, dir);
            if (hit != null)
            {
                var damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    int damage = damageCalculator.CalculateRandomDamage();
                    damageable.TakeDamage(damage);

                    var body = hit.GetComponent<Rigidbody>();
                    if (body != null)
                        body.AddForce(dir * damage * force, ForceMode.VelocityChange);
                }
            }
        }
    }
}
