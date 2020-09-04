using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Exploder))]
public class ItemCrate : Item
{
    private Ammo ammo;
    private Health health;
    private Exploder exploder;

    private void Start()
    {
        ammo = GetComponent<Ammo>();
        health = GetComponent<Health>();
        exploder = GetComponent<Exploder>();
    }
}
