using UnityEngine;
using System.Collections;

public class ForceApplier : MonoBehaviour
{
    public ForceMode mode = ForceMode.VelocityChange;
    public float strength = 1f;

    public void ApplyForce (Transform hit, Vector3 direction, bool random = true)
    {
        StartCoroutine(_ApplyForce(hit, direction, random));
    }

    IEnumerator _ApplyForce (Transform hit, Vector3 direction, bool random)
    {
        yield return new WaitForFixedUpdate();

        if (hit != null)
        {
            Vector3 randomVector = Vector3.zero;
            if (random)
            {
                randomVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            }

            Rigidbody body = hit?.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.AddForce(direction * strength + randomVector, mode);
                body.AddTorque(randomVector * strength, mode);
            }
            else
            {
                Rigidbody[] bodies = hit?.GetComponentsInChildren<Rigidbody>();
                foreach (var item in bodies)
                {
                    if (item != null)
                    {
                        item.AddForce(direction * strength + randomVector, mode);
                        item.AddTorque(randomVector * strength, mode);
                    }
                }
            }
        }
    }
}
