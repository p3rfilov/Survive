using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnItemCollected();
    public delegate void OnItemDropped();

    public static event OnItemCollected onItemCollected;
    public static event OnItemDropped onItemDropped;

    public static void RaiseOnItemCollected()
    {
        onItemCollected?.Invoke();
    }

    public static void RaiseOnItemDropped()
    {
        onItemDropped?.Invoke();
    }
}
