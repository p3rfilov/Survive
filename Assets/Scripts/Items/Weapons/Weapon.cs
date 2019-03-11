using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo;
    public GameObject projectile;
    public int maxDamage;
    public int minDamage;

    protected float force;
    protected float effectiveDistance;
    protected bool automatic;
    protected float fireRate;
    protected float accuracyVariance;
    protected float projectileSpeed;
    protected Transform fireFrom;

    private float lastfired;

    public Weapon()
    {
        ammo = 1;
        maxDamage = 0;
        minDamage = 0;
        force = 1f;
        effectiveDistance = 1000000f;
        automatic = false;
        fireRate = 0f;
        accuracyVariance = 0f;
        projectileSpeed = 10000f;
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

            if (!automatic || Time.time - lastfired > 1 / fireRate)
            {
                if (projectile != null)
                {
                    GameObject bullet = Instantiate(projectile, fireFrom.position, fireFrom.rotation);
                    Rigidbody bulletBody = bullet.GetComponent<Rigidbody>();
                    bulletBody.AddForce(dir * projectileSpeed);
                    --ammo;
                }

                lastfired = Time.time;
                if (Physics.Raycast(ray, out hit, effectiveDistance))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        var body = hit.transform.GetComponent<Rigidbody>();
                        var creature = hit.transform.GetComponent<Creature>();
                        var damage = Random.Range(minDamage, maxDamage);

                        body.AddForce(ray.direction * damage * force, ForceMode.VelocityChange);
                        if (creature != null && creature.isAlive)
                        {
                            creature.TakeDamage(damage);
                        }
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
