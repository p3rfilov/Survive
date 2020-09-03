using UnityEngine;

public class EventManager
{
    public delegate void ItemCollectedCallback ();
    public delegate void ItemDroppedCallback ();
    public delegate void PlayerHealthChangedCallback ();
    public delegate void PlayerCurrentItemChangedCallback ();
    public delegate void PlayerWeaponReloadingCallback (bool reloading);
    public delegate void PlayerNoiseMadeCallback (float noiseLevel);
    public delegate void SomethingDiedCallback (Transform objectTransfrom);
    public delegate void ObjectAboutToBeDestroyedCallback (GameObject obj);
    public delegate void GameStatsChangedCallback ();

    public static event ItemCollectedCallback OnItemCollected;
    public static event ItemDroppedCallback OnItemDropped;
    public static event PlayerHealthChangedCallback OnPlayerHealthChanged;
    public static event PlayerCurrentItemChangedCallback OnPlayerCurrentItemChanged;
    public static event PlayerWeaponReloadingCallback OnPlayerWeaponReloadingStateChanged;
    public static event PlayerNoiseMadeCallback OnPlayerNoiseMade;
    public static event SomethingDiedCallback OnSomethingDied;
    public static event ObjectAboutToBeDestroyedCallback OnObjectAboutToBeDestroyed;
    public static event GameStatsChangedCallback OnGameStatsChanged;

    public static void RaiseOnItemCollected ()
    {
        OnItemCollected?.Invoke();
    }

    public static void RaiseOnItemDropped ()
    {
        OnItemDropped?.Invoke();
    }

    public static void RaiseOnPlayerHealthChanged ()
    {
        OnPlayerHealthChanged?.Invoke();
    }

    public static void RaiseOnPlayerCurrentItemChanged ()
    {
        OnPlayerCurrentItemChanged?.Invoke();
    }

    public static void RaiseOnPlayerWeaponReloadingStateChanged (bool reloading)
    {
        OnPlayerWeaponReloadingStateChanged?.Invoke(reloading);
    }

    public static void RaiseOnPlayerNoiseMade (float noiseLevel)
    {
        OnPlayerNoiseMade?.Invoke(noiseLevel);
    }

    public static void RaiseOnSomethingDied (Transform objectTransfrom)
    {
        OnSomethingDied?.Invoke(objectTransfrom);
    }

    public static void RaiseOnObjectAboutToBeDestroyed (GameObject obj)
    {
        OnObjectAboutToBeDestroyed?.Invoke(obj);
    }

    public static void RaiseOnGameStatsChanged ()
    {
        OnGameStatsChanged?.Invoke();
    }
}
