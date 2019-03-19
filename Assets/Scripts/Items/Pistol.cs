using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(RayShooter))]
[RequireComponent(typeof(ProjectileShooter))]
[RequireComponent(typeof(DamageCalculator))]
public class Pistol : Weapon
{
    private Ammo ammo;
    private RayShooter rayShooter;
    private ProjectileShooter projectileShooter;
    private DamageCalculator damageCalculator;

    public Pistol()
    {
        force = 10f;
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 2f;
    }

    protected override void Start()
    {
        base.Start();
        ammo = GetComponent<Ammo>();
        rayShooter = GetComponent<RayShooter>();
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
            dir = rayShooter.GetRandomHorizontalDirection(fireFrom, accuracyVariance);
            hit = rayShooter.ShootRay(fireFrom.position, dir);
            projectileShooter.Shoot(fireFrom, dir);
            if (hit != null)
            {
                var body = hit.GetComponent<Rigidbody>();
                var damageable = hit.GetComponent<IDamageable>();

                if (body != null)
                    body.AddForce(dir * force, ForceMode.VelocityChange);

                if (damageable != null)
                {
                    int damage = damageCalculator.CalculateRandomDamage();
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}
