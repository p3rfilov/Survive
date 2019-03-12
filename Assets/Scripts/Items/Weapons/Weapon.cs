using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int maxDamage;
    public int minDamage;
    public GameObject model;

    protected float force;
    protected float effectiveDistance;
    protected bool automatic;
    protected float fireRate;
    protected float accuracyVariance;
    protected Transform weaponHand;
    protected Vector3 fireDirection;

    private float lastfired;

    public Weapon()
    {
        maxDamage = 0;
        minDamage = 0;
        force = 1f;
        effectiveDistance = 1000000f;
        automatic = false;
        fireRate = 0f;
        accuracyVariance = 0f;
    }

    protected virtual void Start()
    {
        weaponHand = transform.Find("WeaponHand").transform;
    }

    public virtual bool Use()
    {
        RaycastHit hit;

        fireDirection = Quaternion.AngleAxis(Random.Range(0f, accuracyVariance), weaponHand.up) * weaponHand.forward;
        var ray = new Ray(weaponHand.position, fireDirection);

        if (!automatic || Time.time - lastfired > 1 / fireRate)
        {
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
            return true;
        }
        return false;
    }
}
