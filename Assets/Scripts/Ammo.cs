using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public int clip;
    public float reloadTime;
    public AmmoType ammoType;
    public int maxAmmo;
    public int magazineCapacity;

    public int AllAmmo { get { return ammo + clip; } }
    public enum AmmoType {P_Bullets, MG_Bullets, SG_Shells, Grenades, Mines, Propane};

    private bool reloading = false;

    public bool SpendAmmo()
    {
        if (clip > 0)
        {
            clip--;
            EventManager.RaiseOnPlayerCurrentItemChanged();
            if (clip == 0 && ammo > 0 && !reloading)
            {
                StartCoroutine(Reload());
            }
            return true;
        }
        else if (ammo > 0 && !reloading)
        {
            StartCoroutine(Reload());
        }
        return false;
    }

    public bool AddAmmo(int amount)
    {
        if (ammo < maxAmmo)
        {
            ammo = Mathf.Min(ammo + amount, maxAmmo);
            return true;
        }
        return false;
    }

    public IEnumerator Reload()
    {
        int ammoToAdd = Mathf.Min(ammo, magazineCapacity);
        reloading = true;

        print("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        ammo -= ammoToAdd;
        clip += ammoToAdd;

        reloading = false;
        print("Reloaded!");
        EventManager.RaiseOnPlayerCurrentItemChanged();
    }

    private void OnDisable()
    {
        // in case GameObject is disabled (switching weapons) during reloading
        reloading = false;
    }
}
