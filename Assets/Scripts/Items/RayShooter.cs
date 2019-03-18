using UnityEngine;

public class RayShooter : MonoBehaviour
{
    /// <summary>
    /// Returns a random horizontal direction based on accuracy parameter.
    /// </summary>
    public static Vector3 GetRandomHorizontalDirection(Transform origin, float accuracy)
    {
        Vector3 direction = Quaternion.AngleAxis(Random.Range(0f, accuracy), origin.up) * origin.forward;
        return direction;
    }

    /// <summary>
    /// Shoots a ray in a given direction.
    /// Returns a null transform if nothing is hit.
    /// </summary>
    public static Transform ShootRay(Vector3 origin, Vector3 direction, float distance)
    {
        RaycastHit hit;
        var ray = new Ray(origin, direction);

        Physics.Raycast(ray, out hit, distance);
        return hit.transform;
    }
}
