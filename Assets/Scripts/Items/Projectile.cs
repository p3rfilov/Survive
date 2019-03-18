using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public int maxDamage = 0;
    public int minDamage = 0;
    public float splashRadius = 0f;
    public float force = 0;
    public float lifetime = 0.1f;

    private Collider col;
    private Rigidbody body;

    protected virtual void Start()
    {
        col = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        
    }
}
