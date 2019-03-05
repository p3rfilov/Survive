using UnityEngine;

public class WeaponPistol : MonoBehaviour
{
    public float damage = 5f;

    private CapsuleCollider playerCollider;

    void Start()
    {
        playerCollider = transform.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            var playerCenter = transform.position + playerCollider.center;
            var ray = new Ray(playerCenter, transform.forward);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    var body = hit.transform.GetComponent<Rigidbody>();
                    hit.transform.GetComponent<Enemy>().takeDamage(damage);
                    body.AddForce(ray.direction * damage, ForceMode.Impulse);
                }
            }
        }
    }
}
