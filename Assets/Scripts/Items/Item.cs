using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable
{
    public bool HasOwner { get; set; }
}
