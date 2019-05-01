using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public int magazine;
    public float reloadTime;
    public AmmoType ammoType;
    public int maxAmmo;
    public int magazineCapacity;

    public int AllAmmo { get { return ammo + magazine; } }
    public enum AmmoType {P_Bullets, MG_Bullets, SG_Shells, Grenades, Mines, Propane};

    private bool reloading = false;

    public bool SpendAmmo()
    {
        if (magazine > 0)
        {
            magazine--;
            if (magazine == 0 && ammo > 0 && !reloading)
            {
                StartCoroutine(Reload());
            }
            return true;
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
        magazine += ammoToAdd;

        reloading = false;
        print("Reloaded!");
    }

    private void OnDisable()
    {
        // in case GameObject is disabled (switching weapons) during reloading
        reloading = false;
    }
}
