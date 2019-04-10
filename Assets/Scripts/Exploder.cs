using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(DamageCalculator))]
public class Exploder : MonoBehaviour
{
    public float radius = 1f;
    public float force;
    public float lift;
    public GameObject explosionPrefab;

    private DamageCalculator damageCalculator;
    private bool quitting = false;

    private void Start()
    {
        damageCalculator = GetComponent<DamageCalculator>();
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void OnDestroy()
    {
        if (!quitting)  // TODO: implement pooling system instead of destrying objects
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Projectile>() == null)
                {
                    var body = hit.GetComponent<Rigidbody>();
                    var damageable = hit.GetComponent<IDamageable>();

                    if (damageable != null)
                    {
                        int damage = damageCalculator.CalculateRandomDamage();
                        damageable.TakeDamage(damage);
                    }

                    if (body != null)
                    {
                        body.AddExplosionForce(force, explosionPos, radius, lift);
                    }
                }
            }

            if (explosionPrefab != null)
            {
                var obj = Instantiate(explosionPrefab, transform.position, transform.rotation);
                var explosion = explosionPrefab.GetComponent<ParticleSystem>();
                if (explosion != null)
                    explosion.Play();
                Destroy(obj, explosion.main.duration);
            }
        }
    }
}
