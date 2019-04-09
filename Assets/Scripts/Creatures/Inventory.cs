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
        Item invItem = GetItem(item);
        Ammo invAmmo = invItem?.GetComponent<Ammo>();
        Ammo newAmmo = item.GetComponent<Ammo>();

        if (invAmmo != null && newAmmo != null)
        {
            if (invAmmo.ammoType == newAmmo.ammoType)
            {
                // need to check here for IUsable and swap items if we are only carrying ammo
                if (invAmmo.AddAmmo(newAmmo.AllAmmo))
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
