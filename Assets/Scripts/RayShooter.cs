using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public float effectiveDistance = 1000000f;

    /// <summary>
    /// Returns a random horizontal direction based on accuracy parameter.
    /// </summary>
    public Vector3 GetRandomDirection(Transform origin, float accuracyVariance)
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(0f, accuracyVariance), origin.up) * origin.forward;
        direction = Quaternion.AngleAxis(Random.Range(0f, accuracyVariance), origin.right) * direction;
        return direction;
    }

    /// <summary>
    /// Shoots a ray in a given direction.
    /// Returns a null transform if nothing is hit.
    /// </summary>
    public Transform ShootRay(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        var ray = new Ray(origin, direction);

        Physics.Raycast(ray, out hit, effectiveDistance);
        return hit.transform;
    }
}
