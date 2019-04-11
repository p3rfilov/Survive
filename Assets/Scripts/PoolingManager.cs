using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static void Remove(GameObject obj, float delay = 0f)
    {
        // TODO: implement pooling system
        Destroy(obj, delay);
    }
}
