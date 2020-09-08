using UnityEngine;

public class ItemFloater : MonoBehaviour
{
    static float rotationSpeed = 60f;
    static float wobbleSpeed = 2f;
    static float height = 0.2f;
    static float amplitude = 0.1f;

    private Item item;
    private Vector3 tempPos;
    private float yPos;

    private void Start()
    {
        item = GetComponent<Item>();
        if (item != null)
        {
            tempPos = transform.position;
            yPos = tempPos.y;
        }
        EventManager.OnItemDropped += RecordPosition;
    }

    private void Update()
    {
        if (item != null && !item.HasOwner)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            tempPos.y = yPos + height + amplitude * Mathf.Sin(wobbleSpeed * Time.time);
            transform.position = tempPos;
        }
    }

    private void RecordPosition()
    {
        if (item != null && item.HasOwner)
        {
            tempPos = transform.position;
            yPos = tempPos.y;
        }
    }
}
