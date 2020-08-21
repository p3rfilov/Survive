using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Color zoneColor;
    public GameObject[] objects;
    [Range(0f, 100f)] public float spawnChance;

    public GameObject Spawn ()
    {
        int length = objects.Length;
        if (length > 0)
        {
            if (spawnChance >= Random.Range(0f, 100f))
            {
                return Instantiate(objects[Random.Range(0, length)], GetRandomPoint(), transform.rotation);
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = zoneColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    private Vector3 GetRandomPoint ()
    {
        return transform.position + new Vector3(
           (Random.value - 0.5f) * transform.localScale.x,
           (Random.value - 0.5f) * transform.localScale.y,
           (Random.value - 0.5f) * transform.localScale.z
        );
    }
}
