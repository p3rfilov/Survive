using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;

    public void SpendAmmo()
    {
        if (ammo > 0)
            ammo--;
    }
}
