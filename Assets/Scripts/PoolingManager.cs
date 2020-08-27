using UnityEngine;
using System.Collections;

public class PoolingManager : MonoBehaviour
{
    public static IEnumerator Remove (GameObject obj, float delay = 0f, bool notify = true)
    {
        // TODO: implement pooling system
        yield return new WaitForSeconds(delay);
        if (notify)
        {
            EventManager.RaiseOnObjectAboutToBeDestroyed(obj);
        }
        Destroy(obj);
        print(obj.name);
    }
}
