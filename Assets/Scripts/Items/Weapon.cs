using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo;
    public GameObject projectile;

    protected int maxDamage;
    protected int damageVariance;
    protected float splashRadius;
    protected float force;
    protected float effectiveDistance;
    protected bool automatic;
    protected float fireRate;
    protected float accuracyVariance;
    protected float projectileSpeed;
    protected float projectileLifetime;
    protected Transform fireFrom;

    public Weapon()
    {
        maxDamage = 0;
        damageVariance = 0;
        splashRadius = 0f;
        force = 1f;
        effectiveDistance = 1000000f;
        automatic = false;
        fireRate = 0f;
        accuracyVariance = 0f;
        projectileSpeed = 10000f;
        projectileLifetime = 1f;
    }

    protected virtual void Start()
    {
        fireFrom = transform.Find("WeaponHand").transform;
    }

    public virtual void Use()
    {
        if (ammo > 0)
        {
            RaycastHit hit;

            Vector3 dir = Quaternion.AngleAxis(Random.Range(0f, accuracyVariance), fireFrom.up) * fireFrom.forward;
            var ray = new Ray(fireFrom.position, dir);

            GameObject bullet = Instantiate(projectile, fireFrom.position, fireFrom.rotation);
            Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
            bulletBody.AddForce(dir * projectileSpeed);
            Destroy(bullet, projectileLifetime);
            --ammo;

            if (Physics.Raycast(ray, out hit, effectiveDistance))
            {
                Destroy(bullet, 0.1f);
                if (hit.transform.CompareTag("Enemy"))
                {
                    var body = hit.transform.GetComponent<Rigidbody>();
                    var creature = hit.transform.GetComponent<Creature>();
                    var damage = Random.Range(maxDamage - damageVariance, maxDamage);

                    body.AddForce(ray.direction * damage * force, ForceMode.VelocityChange);
                    if (creature != null && creature.isAlive)
                    {
                        creature.TakeDamage(damage);
                    }
                }
            }
        }
        else
        {
            print("Out of Ammo!");
        }
    }
}
