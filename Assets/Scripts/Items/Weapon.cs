using UnityEngine;

public abstract class Weapon : Item, IUsable
{
    public bool automatic = false;
    public float fireRate = 0f;
    public float accuracyVariance = 0f;
    
    protected Transform fireFrom;
    protected float lastFired;

    protected virtual void Start()
    {
        lastFired = float.NegativeInfinity;
        HasOwner = false;
    }

    public virtual void Use()
    {
        if (fireFrom == null)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t.name == "fireFrom")
                {
                    fireFrom = t;
                    return;
                }
            }
        }
    }

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
