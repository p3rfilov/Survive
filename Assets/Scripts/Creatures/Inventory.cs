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
        if (!HasItem(item))
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
            if (items[i] != null && items[i].name == item.name)
                return true;
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
