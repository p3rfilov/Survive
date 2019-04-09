using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float dropDistance = 2f;
    public const int size = 5;
    public Item[] items = new Item[size];

    public int Size { get { return size; } }
    public Item[] AllItems { get { return items; } }

    private void OnEnable()
    {
        InstantiateItems();
    }

    public bool AddItem(Item item)
    {
        if (!Replenish(item))
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    item.HasOwner = true;
                    item.gameObject.SetActive(false);
                    items[i] = item;
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
                items[i] = null;
        }
    }

    public void DropItem(Item item, bool active = true)
    {
        RemoveItem(item);
        item.transform.SetParent(null);
        item.gameObject.SetActive(active);
        item.transform.position = transform.position + transform.forward * dropDistance;
        item.HasOwner = !active;
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    private Item GetItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].GetType() == item.GetType())
                return items[i];
        }
        return null;
    }

    public bool Replenish(Item item)
    {
        Item inventoryItem = GetItem(item);
        Ammo inventoryAmmo = inventoryItem?.GetComponent<Ammo>();
        Ammo newAmmo = item.GetComponent<Ammo>();

        if (inventoryAmmo != null && newAmmo != null)
        {
            if (inventoryAmmo.ammoType == newAmmo.ammoType)
            {
                // swap items if picking up IUsable and we are only carrying ammo
                // TODO: implementation feels wrong; separate inventory for ammo? initial idea was to have dropable/destructable ammo crates?
                if (!(inventoryItem is IUsable) && item is IUsable)
                {
                    newAmmo.AddAmmo(inventoryAmmo.AllAmmo);
                    DropItem(inventoryItem, false);
                    return false;
                }
                else if (inventoryAmmo.AddAmmo(newAmmo.AllAmmo))
                {
                    item.gameObject.SetActive(false);
                    return true;
                }
            }
        }
        return false;
    }

    private void InstantiateItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                var obj = items[i].gameObject;
                if (obj.scene.name == null)
                {
                    items[i] = null;
                    var newItem = Instantiate(obj).GetComponent<Item>();
                    AddItem(newItem);
                }
            }
        }
    }
}
