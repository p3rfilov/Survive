using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnItemCollected ();
    public delegate void OnItemDropped ();
    public delegate void OnPlayerHealthChanged ();
    public delegate void OnPlayerCurrentItemChanged ();

    public static event OnItemCollected onItemCollected;
    public static event OnItemDropped onItemDropped;
    public static event OnPlayerHealthChanged onPlayerHealthChanged;
    public static event OnPlayerCurrentItemChanged onPlayerCurrentItemChanged;

    public static void RaiseOnItemCollected ()
    {
        onItemCollected?.Invoke();
    }

    public static void RaiseOnItemDropped ()
    {
        onItemDropped?.Invoke();
    }

    public static void RaiseOnPlayerHealthChanged ()
    {
        onPlayerHealthChanged?.Invoke();
    }

    public static void RaiseOnPlayerCurrentItemChanged ()
    {
        onPlayerCurrentItemChanged?.Invoke();
    }
}
