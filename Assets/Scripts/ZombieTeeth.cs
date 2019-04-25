using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(RayShooter))]
[RequireComponent(typeof(ForceApplier))]
[RequireComponent(typeof(DamageCalculator))]
public class ZombieTeeth : Weapon
{
    private RayShooter rayShooter;
    private ForceApplier forceApplier;
    private DamageCalculator damageCalculator;

    public ZombieTeeth()
    {
        accuracyVariance = 0f;
        automatic = true;
        fireRate = 1f;
    }

    protected override void Start()
    {
        base.Start();
        rayShooter = GetComponent<RayShooter>();
        forceApplier = GetComponent<ForceApplier>();
        damageCalculator = GetComponent<DamageCalculator>();
    }

    public override void Use()
    {
        if (CanUse())
        {
            base.Use();
            Vector3 dir;
            Transform hit;

            dir = rayShooter.GetRandomDirection(fireFrom, accuracyVariance);
            hit = rayShooter.ShootRay(fireFrom.position, dir);
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
