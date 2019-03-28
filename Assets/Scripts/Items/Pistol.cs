using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(RayShooter))]
[RequireComponent(typeof(ForceApplier))]
[RequireComponent(typeof(ProjectileShooter))]
[RequireComponent(typeof(DamageCalculator))]
public class Pistol : Weapon
{
    private Ammo ammo;
    private RayShooter rayShooter;
    private ForceApplier forceApplier;
    private ProjectileShooter projectileShooter;
    private DamageCalculator damageCalculator;

    public Pistol()
    {
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 2f;
    }

    protected override void Start()
    {
        base.Start();
        ammo = GetComponent<Ammo>();
        rayShooter = GetComponent<RayShooter>();
        forceApplier = GetComponent<ForceApplier>();
        projectileShooter = GetComponent<ProjectileShooter>();
        damageCalculator = GetComponent<DamageCalculator>();
    }

    public override void Use()
    {
        if (ammo.ammo > 0 && CanUse())
        {
            base.Use();
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

                forceApplier.ApplyForce(body, dir);
                if (damageable != null)
                {
                    int damage = damageCalculator.CalculateRandomDamage();
                    damageable.TakeDamage(damage);
                }
            }
        }
    }
}
