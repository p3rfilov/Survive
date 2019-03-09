using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int ammo;

    protected int maxDamage;
    protected int damageVariance;
    protected float splashRadius;
    protected float effectiveDistance;
    protected bool automatic;
    protected float fireRate;
    protected CapsuleCollider playerCollider;

    public Weapon()
    {
        maxDamage = 0;
        damageVariance = 0;
        splashRadius = 0f;
        effectiveDistance = 1000000f;
        automatic = false;
        fireRate = 0f;
    }

    protected virtual void Start()
    {
        playerCollider = transform.GetComponent<CapsuleCollider>();
    }

    public virtual void Use()
    {
        if (ammo > 0)
        {
            RaycastHit hit;
            var playerCenter = transform.position + playerCollider.center;
            var ray = new Ray(playerCenter, transform.forward);
            --ammo;

            if (Physics.Raycast(ray, out hit, effectiveDistance))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    var body = hit.transform.GetComponent<Rigidbody>();
                    var creature = hit.transform.GetComponent<Creature>();
                    var damage = Random.Range(maxDamage - damageVariance, maxDamage);

                    body.AddForce(ray.direction * maxDamage, ForceMode.Impulse);
                    if (creature != null && creature.isAlive)
                    {
                        creature.TakeDamage(maxDamage);
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
