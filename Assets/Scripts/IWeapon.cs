using UnityEngine;

public interface IWeapon
{
    int maxDamage { get; }
    int minDamage { get; }
    float force { get; }
    float effectiveDistance { get; }
    float accuracyVariance { get; }
    GameObject model { get; }
    Transform fireFrom { get; }
    Vector3 fireDirection { get; }

    bool automatic { get; }
    float fireRate { get; }
    float lastFired { get; }

    void Use();
}