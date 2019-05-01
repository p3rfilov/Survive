using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float lifetime = 0.1f;
    public bool destroyOnCollision = true;
    public bool ignoreOtherProjectiles = true;

    private Collider col;
    private Rigidbody body;

    private void Start()
    {
        transform.SetParent(null);
        col = GetComponent<Collider>();
        col.isTrigger = true;
        body = GetComponent<Rigidbody>();
        PoolingManager.Remove(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnCollision)
        {
            var _projectile = other.GetComponent<Projectile>();
            if (ignoreOtherProjectiles && _projectile != null)
                return;
            PoolingManager.Remove(gameObject);
        }
    }
}
