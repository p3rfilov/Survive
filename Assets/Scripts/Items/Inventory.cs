using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool instantiateItems = true;
    public Item[] items;
    public int Size { get { return items.Length; } }
    public Item[] AllItems { get { return items; } }

    private void OnEnable()
    {
        if (instantiateItems)
        {
            InstantiateItems();
        }
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
                if (!(inventoryItem is IUsable) && item is IUsable)
                {
                    newAmmo.AddAmmo(inventoryAmmo.AllAmmo);
                    RemoveItem(inventoryItem);
                    PoolingManager.Remove(inventoryItem.gameObject, false);
                    EventManager.RaiseOnItemCollected();
                    return false;
                }
                else if (inventoryAmmo.AddAmmo(newAmmo.AllAmmo))
                {
                    item.gameObject.SetActive(false);
                    PoolingManager.Remove(item.gameObject, false);
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
