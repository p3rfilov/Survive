using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(RayShooter))]
public class ProjectileShooter : Weapon
{
    public bool dealDamage;
    public float verticalAngle;
    public Projectile projectile;
    public float projectileSpeed;
    public int projectileCount;
    public Light muzzleFlash;
    public float flashDuration;
    public float noiseLevel;

    private Ammo ammo;
    private RayShooter rayShooter;
    private ForceApplier forceApplier;
    private DamageCalculator damageCalculator;

    private Quaternion _angle;

    protected override void Start()
    {
        base.Start();
        ammo = GetComponent<Ammo>();
        rayShooter = GetComponent<RayShooter>();
        if (dealDamage)
        {
            forceApplier = GetComponent<ForceApplier>();
            damageCalculator = GetComponent<DamageCalculator>();

            if (forceApplier == null)
                forceApplier = gameObject.AddComponent<ForceApplier>();

            if (damageCalculator == null)
                damageCalculator = gameObject.AddComponent<DamageCalculator>();
        }
        _angle = Quaternion.AngleAxis(verticalAngle, Vector3.left);
    }

    public override void Use()
    {
        if (CanUse() && ammo.SpendAmmo())
        {
            base.Use();
            MuzzleFlash();

            Vector3 dir;
            Transform hit;

            for (int i = 0; i < projectileCount; i++)
            {
                dir = rayShooter.GetRandomDirection(fireFrom, accuracyVariance);
                dir = _angle * dir;
                hit = rayShooter.ShootRay(fireFrom.position, dir);
                Shoot(fireFrom, dir);
                if (dealDamage && hit != null)
                {
                    var damageable = hit.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        int damage = damageCalculator.CalculateRandomDamage();
                        damageable.TakeDamage(damage);
                    }
                    forceApplier.ApplyForce(hit, dir);
                }
            }
            EventManager.RaiseOnPlayerNoiseMade(noiseLevel);
        }
    }

    void Shoot(Transform origin, Vector3 direction)
    {
        if (projectile != null)
        {
            // TODO: implement a Pooling System
            Projectile _projectile = Instantiate(projectile, origin.position, origin.rotation);
            Rigidbody body = _projectile.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(_projectile.GetComponent<Collider>(), GetComponent<Collider>());
            body.AddForce(direction * projectileSpeed);
        }
    }

    void MuzzleFlash ()
    {
        if (muzzleFlash != null)
        {
            StartCoroutine(MuzzleFlashCR());
        }
    }

    IEnumerator MuzzleFlashCR ()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlash.enabled = false;
    }
}
