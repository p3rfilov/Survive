using UnityEngine;

public class Inventory : MonoBehaviour
{
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
                    EventManager.RaiseOnItemCollected();
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

    public Item GetItem(int index)
    {
        if (index >= 0 && (index < items.Length))
            return items[index];
        return null;
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

    private Item GetItem(Ammo.AmmoType type)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Ammo ammo = items[i]?.GetComponent<Ammo>();
            if (ammo != null && ammo.ammoType == type)
            {
                return items[i];
            }
        }
        return null;
    }

    public bool Replenish(Item item)
    {
        Ammo newAmmo = null;
        Item inventoryItem = null;
        Ammo inventoryAmmo = null;

        newAmmo = item.GetComponent<Ammo>();
        if (newAmmo != null)
        {
            inventoryItem = GetItem(newAmmo.ammoType);
            inventoryAmmo = inventoryItem?.GetComponent<Ammo>();
        }

        if (inventoryAmmo != null && newAmmo != null)
        {
            if (inventoryAmmo.ammoType == newAmmo.ammoType)
            {
                // swap items if picking up IUsable and we are only carrying ammo
                // TODO: implementation feels wrong; separate inventory for ammo? initial idea was to have dropable/destructable ammo crates?
                if (!(inventoryItem is IUsable) && item is IUsable)
                {
                    newAmmo.AddAmmo(inventoryAmmo.AllAmmo);
                    RemoveItem(inventoryItem);
                    ItemDropper.Drop(inventoryItem);
                    EventManager.RaiseOnItemCollected();
                    return false;
                }
                else if (inventoryAmmo.AddAmmo(newAmmo.AllAmmo))
                {
                    item.gameObject.SetActive(false);
                    EventManager.RaiseOnItemCollected();
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
