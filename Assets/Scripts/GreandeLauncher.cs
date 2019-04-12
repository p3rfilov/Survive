﻿using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Ammo))]
[RequireComponent(typeof(RayShooter))]
[RequireComponent(typeof(ProjectileShooter))]
public class GreandeLauncher : Weapon
{
    public float upAngle;

    private Quaternion _angle;

    private Ammo ammo;
    private RayShooter rayShooter;
    private ProjectileShooter projectileShooter;
    private DamageCalculator damageCalculator;

    public GreandeLauncher()
    {
        upAngle = 0;
        accuracyVariance = 1f;
        automatic = true;
        fireRate = 0.5f;
    }

    protected override void Start()
    {
        base.Start();
        ammo = GetComponent<Ammo>();
        ammo.ammoType = Ammo.AmmoType.Grenades;
        rayShooter = GetComponent<RayShooter>();
        projectileShooter = GetComponent<ProjectileShooter>();
        _angle = Quaternion.AngleAxis(upAngle, Vector3.left);
    }

    public override void Use()
    {
        if (CanUse() && ammo.SpendAmmo())
        {
            base.Use();
            Vector3 dir;

            dir = rayShooter.GetRandomHorizontalDirection(fireFrom, accuracyVariance);
            dir = _angle * dir;
            projectileShooter.Shoot(fireFrom, dir);
        }
    }
}