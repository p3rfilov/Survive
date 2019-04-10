using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public ForceMode mode = ForceMode.VelocityChange;
    public float strength = 1f;

    public void ApplyForce(Rigidbody body, Vector3 direction)
    {
        if (body != null)
            body.AddForce(direction * strength, mode);
    }
}
