using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject prefab;
    public Transform holdingHand;

    public GameObject Object { get; private set; }

    private void OnEnable()
    {
        if (Object != null)
            Destroy(Object);

        if (prefab != null && holdingHand != null)
        {
            Object = Instantiate(prefab, holdingHand.position, holdingHand.rotation);
            Object.transform.SetParent(holdingHand);
        }
    }
}
