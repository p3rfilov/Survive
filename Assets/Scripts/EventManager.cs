using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnItemCollected();
    public static event OnItemCollected onItemCollected;

    public static void RaiseOnItemCollected()
    {
        if (onItemCollected != null)
        {
            onItemCollected();
        }
    }
}
