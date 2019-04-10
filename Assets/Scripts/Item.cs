using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ItemFloater))]
public abstract class Item : MonoBehaviour, ICollectable
{
    public bool HasOwner { get; set; }
}
