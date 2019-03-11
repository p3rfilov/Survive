using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int maxDamage;
    protected int minDamage;
    protected float splashRadius;
    protected float force;
    protected float projectileLifetime;

    private Collider col;

    public Projectile()
    {
        maxDamage = 0;
        minDamage = 0;
        splashRadius = 0f;
        force = 0f;
        projectileLifetime = 0.1f;
    }

    protected virtual void Start()
    {
        col = GetComponent<Collider>();
        Destroy(gameObject, projectileLifetime);
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        
    }
}
