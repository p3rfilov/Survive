using UnityEngine;
using System;

public abstract class Creature : MonoBehaviour
{
    public static float groundDistance = 0.2f;
    public static LayerMask ground;

    public bool isAlive = true;
    public float health;
    public float moveSpeed;
    public float jumpHeight;

    private Rigidbody body;

    public virtual void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        if (health < 0f && isAlive)
        {
            Kill();
        }
    }

    public virtual void takeDamage(float damage)
    {
        health -= damage;
    }

    public virtual void Kill()
    {
        isAlive = false;
        throw new NotImplementedException("Kill routine not fully implemented");
    }
}
