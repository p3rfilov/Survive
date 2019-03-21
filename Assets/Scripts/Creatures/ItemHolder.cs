using System.Collections;
using UnityEngine;

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
            CicleItems(index);
        }
    }

    private void HoldItem(Item item)
    {
        if (Object != null)
            Destroy(Object);

        if (holdingHand != null)
        {
            Object = Instantiate(item.transform.gameObject, holdingHand.position, holdingHand.rotation);
            Object.transform.SetParent(holdingHand);
            return;
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
