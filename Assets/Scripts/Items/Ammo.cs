using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public int magazine;
    public float reloadTime;
    public int maxAmmo;
    public int magazineCapacity;

    private bool reloading = false;

    public bool SpendAmmo()
    {
        if (magazine > 0)
        {
            magazine--;
            return true;
        }
        else if (ammo > 0 && !reloading)
        {
            StartCoroutine(Reload());
        }
        return false;
    }

    public IEnumerator Reload()
    {
        int ammoToAdd = Mathf.Min(ammo, magazineCapacity);
        reloading = true;

        print("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammo -= ammoToAdd;
        magazine += ammoToAdd;

        reloading = false;
    }

    private void OnDisable()
    {
        // in case GameObject is disabled (switching weapons) during reloading
        reloading = false;
    }
}
