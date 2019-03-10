using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider col;

    protected virtual void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        
    }
}
