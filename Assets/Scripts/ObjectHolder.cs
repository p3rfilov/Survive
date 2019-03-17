using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
    public GameObject prefab;
    public Transform holdingHand;

    private GameObject _object;

    private void OnEnable()
    {
        if (_object != null)
            Destroy(_object);

        if (prefab != null && holdingHand != null)
        {
            _object = Instantiate(prefab, holdingHand.position, holdingHand.rotation);
            _object.transform.SetParent(holdingHand);
        }
    }

    public GameObject GetObject()
    {
        return _object;
    }
}
