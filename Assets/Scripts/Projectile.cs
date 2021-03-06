﻿using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Tooltip("0 = infinity")]
    public float lifetime = 0.1f;
    public bool destroyOnCollision = true;
    public bool ignoreOtherProjectiles = true;

    private Collider col;
    private Rigidbody body;

    private void Start()
    {
        transform.SetParent(null);
        col = GetComponent<Collider>();
        col.isTrigger = true;
        body = GetComponent<Rigidbody>();
        if (lifetime > 0)
            StartCoroutine(PoolingManager.RemoveDelayed(gameObject, lifetime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnCollision)
        {
            var _projectile = other.GetComponent<Projectile>();
            if (ignoreOtherProjectiles && _projectile != null)
                return;
            PoolingManager.Remove(gameObject);
        }
    }
}
