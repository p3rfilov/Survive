using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemCollector : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.gameObject.GetComponent<Item>();
        if (item != null && !item.HasOwner)
        {
            if (inventory.AddItem(item))
            {
                EventManager.RaiseOnItemCollected();
            }
        }
    }
}
