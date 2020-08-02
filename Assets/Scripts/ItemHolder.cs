using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Inventory))]
public class ItemHolder : MonoBehaviour
{
    public Transform holdingHand;
    public GameObject Object { get; private set; }
    public int Index { get; private set; } = 0;

    private Inventory inventory;

    private void Start ()
    {
        inventory = GetComponent<Inventory>();
        EventManager.OnItemCollected += HoldIfEmpty;
        CicleItems(Index);
    }

    public Item GetItem ()
    {
        return Object?.GetComponent<Item>();
    }

    public void CicleItems (int index)
    {
        Index += index;
        if (Index > inventory.Size - 1)
            Index = 0;
        if (Index < 0)
            Index = inventory.Size - 1;

        Item item = inventory.GetItem(Index);
        if (item != null)
        {
            HoldItem(item);
        }
        else if (!IsArrayEmpty(inventory.AllItems))
        {
            CicleItems(Index);
        }
        EventManager.RaiseOnPlayerCurrentItemChanged();
    }

    public void DropCurrentItem ()
    {
        Item item = GetItem();
        ItemDropper.Drop(item, true);
        inventory.RemoveItem(item);
        Object = null;
    }

    private void HoldIfEmpty ()
    {
        if (Object == null || !Object.activeSelf)
            CicleItems(1);
    }

    private void HoldItem (Item item)
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

    private bool IsArrayEmpty (Item[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
                return false;
        }
        return true;
    }
}
