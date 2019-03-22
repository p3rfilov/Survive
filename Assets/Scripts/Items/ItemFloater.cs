using UnityEngine;

public class ItemFloater : MonoBehaviour
{
    public Item item;
    public float rotationSpeed = 60f;
    public float wobbleSpeed = 2f;
    public float height = 0.5f;
    public float amplitude = 0.1f;

    private GameObject obj;
    private Vector3 tempPos;
    private float yPos;

    private void Start()
    {
        if (item != null)
        {
            var alt = new Vector3(0, height, 0);
            obj = Instantiate(item.transform.gameObject, transform.position + alt, transform.rotation);
            tempPos = obj.transform.position;
            yPos = obj.transform.position.y;

            EventManager.onItemCollected += Deactivate;
        }
    }

    private void Update()
    {
        if (obj != null)
        {
            obj.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            tempPos.y = yPos + amplitude * Mathf.Sin(wobbleSpeed * Time.time);
            obj.transform.position = tempPos;
        }
    }

    private void Deactivate()
    {
        Item _item = obj?.GetComponent<Item>();
        if (_item != null && _item.HasOwner)
        {
            gameObject.SetActive(false);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Item _item = obj?.GetComponent<Item>();
    //    if (_item != null && _item.HasOwner)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
