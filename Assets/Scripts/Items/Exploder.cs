using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(DamageCalculator))]
public class Exploder : MonoBehaviour
{
    public float radius = 1f;
    public float force;
    public float lift;

    private DamageCalculator damageCalculator;

    private void Start()
    {
        damageCalculator = GetComponent<DamageCalculator>();
    }

    private void OnDestroy()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            var body = hit.GetComponent<Rigidbody>();
            var damageable = hit.GetComponent<IDamageable>();

            if (body != null)
            {
                body.AddExplosionForce(force, explosionPos, radius, lift);
            }

            if (damageable != null)
            {
                int damage = damageCalculator.CalculateRandomDamage();
                damageable.TakeDamage(damage);
            }
        }
    }
}
