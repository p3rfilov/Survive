using UnityEngine;

public abstract class Weapon : MonoBehaviour, IUsable
{
    public float force = 0f;
    public bool automatic = false;
    public float fireRate = 0f;
    public float accuracyVariance = 0f;

    protected Transform fireFrom;
    protected float lastFired = 0f;

    protected virtual void Start()
    {
        fireFrom = GameObject.Find("fireFrom").transform;
    }

    public abstract void Use();

    public virtual bool CanUse()
    {
        if (!automatic || Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            return true;
        }
        return false;
    }
}
