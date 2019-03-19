using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Projectile projectile;
    public float projectileSpeed;

    public void Shoot(Transform origin, Vector3 direction)
    {
        if (projectile != null)
        {
            // TODO: implement a Pooling System
            Projectile _projectile = Instantiate(projectile, origin.position, origin.rotation);
            Rigidbody body = _projectile.GetComponent<Rigidbody>();
            body.AddForce(direction * projectileSpeed);
        }
    }
}
