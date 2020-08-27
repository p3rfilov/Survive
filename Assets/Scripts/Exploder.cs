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

    private void Start()
    {
        damageCalculator = GetComponent<DamageCalculator>();
        EventManager.OnObjectAboutToBeDestroyed += Explode;
    }

    private void Explode (GameObject gameObj)
    {
        if (gameObj == transform.gameObject)
        {
            EventManager.OnObjectAboutToBeDestroyed -= Explode;
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
                var explosionObj = Instantiate(explosionPrefab, transform.position, transform.rotation);
                var explosion = explosionPrefab.GetComponent<ParticleSystem>();
                if (explosion != null)
                    explosion.Play();
                Destroy(explosionObj, explosion.main.duration);
            }
        }
    }
}
