using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Inventory))]
public class ItemHolder : MonoBehaviour
{
    public Transform holdingHand;
    public GameObject Object { get; private set; }

    private Inventory inventory;
    private int currentIndex = 0;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        EventManager.onItemCollected += HoldIfEmpty;
        CicleItems(currentIndex);
    }

    public void CicleItems(int index)
    {
        currentIndex += index;
        if (currentIndex > inventory.Size - 1)
            currentIndex = 0;
        if (currentIndex < 0)
            currentIndex = inventory.Size - 1;

        Item item = inventory.GetItem(currentIndex);
        if (item != null)
        {
            HoldItem(item);
        }
        else if (!IsArrayEmpty(inventory.AllItems))
        {
            CicleItems(currentIndex);
        }
    }

    public void DropCurrentItem()
    {
        ItemDropper.Drop(Object?.GetComponent<Item>(), true);
        inventory.RemoveItem(Object?.GetComponent<Item>());
        Object = null;
    }

    private void HoldIfEmpty()
    {
        if (Object == null || !Object.activeSelf)
            CicleItems(1);
    }

    private void HoldItem(Item item)
    {
        if (Object != null)
            Object.SetActive(false);

        if (holdingHand != null)
        {
            Object = item.transform.gameObject;
            Object.transform.position = holdingHand.position;
            Object.transform.rotation = holdingHand.rotation;
            Object.transform.SetParent(holdingHand);
            Object.SetActive(true);
        }
    }

    private bool IsArrayEmpty(Item[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
                return false;
        }
        return true;
    }
}
