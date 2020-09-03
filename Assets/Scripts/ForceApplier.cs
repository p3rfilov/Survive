using UnityEngine;
using System.Collections;

public class ForceApplier : MonoBehaviour
{
    public ForceMode mode = ForceMode.VelocityChange;
    public float strength = 1f;

    public void ApplyForce (Transform hit, Vector3 direction)
    {
        Rigidbody body = hit.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.AddForce(direction * strength, mode);
        }
        else
        {
            Rigidbody[] bodies = hit?.GetComponentsInChildren<Rigidbody>();
            foreach (var item in bodies)
            {
                if (item != null)
                {
                    item.AddForce(direction * strength, mode);
                }
            }
        }
    }
}
