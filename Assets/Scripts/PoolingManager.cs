using UnityEngine;
using System.Collections;

public class PoolingManager : MonoBehaviour
{
    public static void Remove (GameObject obj, bool notify = true)
    {
        if (notify)
        {
            EventManager.RaiseOnObjectAboutToBeDestroyed(obj);
        }
        Destroy(obj);
    }

    public static IEnumerator RemoveDelayed (GameObject obj, float delay = 0f, bool notify = true)
    {
        // TODO: implement pooling system
        yield return new WaitForSeconds(delay);
        if (notify)
        {
            EventManager.RaiseOnObjectAboutToBeDestroyed(obj);
        }
        Destroy(obj);
    }
}
