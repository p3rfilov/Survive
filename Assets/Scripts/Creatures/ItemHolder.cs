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
        Item item = inventory.GetItem(currentIndex);
        if (item != null)
        {
            HoldItem(item);
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
}
