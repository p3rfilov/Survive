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

    private IEnumerator Reload()
    {
        reloading = true;
        int ammoToAdd = Mathf.Min(ammo, magazineCapacity);

        print("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammo -= ammoToAdd;
        magazine += ammoToAdd;

        reloading = false;
    }
}
