using UnityEngine;

public static class ItemDropper
{
    private static float dropDistance = 2f;

    public static void Drop(Item item, bool active = false, bool random = false)
    {
        if (item != null)
        {
            Transform parent = item.transform.parent?.root;
            if (parent == null)
                parent = item.transform;

            item.gameObject.SetActive(active);
            if (active)
            {
                var dir = parent.forward;
                if (random)
                    dir = new Vector3(0, Random.Range(-1f, 1f), 0);
                item.transform.position = parent.position + dir * dropDistance;
            }
            EventManager.RaiseOnItemDropped();
            item.transform.SetParent(null);
            item.HasOwner = !active;
        }
    }
}
