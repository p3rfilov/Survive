using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
[RequireComponent(typeof(DamageCalculator))]
public class Exploder : MonoBehaviour
{
    public float radius = 1f;
    public float force;
    public float lift;
    public GameObject explosionPrefab;
    public float duration;

    private DamageCalculator damageCalculator;

    private void Start()
    {
        damageCalculator = GetComponent<DamageCalculator>();
        EventManager.OnObjectAboutToBeDestroyed += Explode;
    }

    private void OnDestroy ()
    {
        EventManager.OnObjectAboutToBeDestroyed -= Explode;
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
                    var damageable = hit.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        int damage = damageCalculator.CalculateRandomDamage();
                        damageable.TakeDamage(damage);
                    }
                    StartCoroutine(_AddExplosionForce(hit.transform, force, explosionPos, radius, lift));
                }
            }

            if (explosionPrefab != null)
            {
                var explosionObj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                var explosion = explosionPrefab.GetComponent<ParticleSystem>();
                if (explosion != null)
                    explosion.Play();
                Destroy(explosionObj, duration);
            }
        }
    }

    IEnumerator _AddExplosionForce (Transform hit, float force, Vector3 pos, float radius, float upVector)
    {
        if (hit != null)
        {
            Rigidbody body = hit.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddExplosionForce(force, pos, radius, upVector);
            }
            else
            {
                Rigidbody[] bodies = hit.GetComponentsInChildren<Rigidbody>();
                foreach (var item in bodies)
                {
                    if (item != null)
                    {
                        body.AddExplosionForce(force, pos, radius, upVector);
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
