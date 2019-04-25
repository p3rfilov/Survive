using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(RayShooter))]
[RequireComponent(typeof(ForceApplier))]
[RequireComponent(typeof(ProjectileShooter))]
[RequireComponent(typeof(DamageCalculator))]
public class MachineGun : Weapon
{
    private Ammo ammo;
    private RayShooter rayShooter;
    private ForceApplier forceApplier;
    private ProjectileShooter projectileShooter;
    private DamageCalculator damageCalculator;

    public MachineGun()
    {
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 6f;
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
        if (CanUse() && ammo.SpendAmmo())
        {
            base.Use();
            Vector3 dir;
            Transform hit;

            dir = rayShooter.GetRandomDirection(fireFrom, accuracyVariance);
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
