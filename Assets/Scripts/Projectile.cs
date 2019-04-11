﻿using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float lifetime = 0.1f;
    public bool destroyOnCollision = true;

    private Collider col;
    private Rigidbody body;

    private void Start()
    {
        transform.SetParent(null);
        col = GetComponent<Collider>();
        col.isTrigger = true;
        body = GetComponent<Rigidbody>();
        PoolingManager.Remove(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnCollision)
            Destroy(gameObject);
    }
}
