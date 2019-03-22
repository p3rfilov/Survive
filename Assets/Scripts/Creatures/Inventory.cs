using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int size = 5;
    public Item[] items = new Item[size];

    public int Size { get { return size; } }
    public Item[] AllItems { get { return items; } }

    public bool AddItem(Item item)
    {
        if (!HasItem(item))
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    item.HasOwner = true;
                    item.transform.gameObject.SetActive(false);
                    items[i] = item;
                    return true;
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

    private bool HasItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
                return true;
        }
        return false;
    }
}
