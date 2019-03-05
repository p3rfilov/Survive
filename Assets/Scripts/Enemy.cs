using UnityEngine;

public class Enemy : Creature
{
    public float explosionForce = 3.5f;

    public Enemy()
    {
        health = 10f;
    }

    public override void Kill()
    {
        Transform[] allParts;
        Rigidbody body;
        isAlive = false;

        Destroy(transform.GetComponent<Rigidbody>());
        Destroy(transform.GetComponent<CapsuleCollider>());

        allParts = transform.GetComponentsInChildren<Transform>();
        foreach (var item in allParts)
        {
            if (item.parent)
            {
                Vector3 dir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

                item.gameObject.AddComponent<BoxCollider>();
                item.gameObject.AddComponent<Rigidbody>();
                body = item.GetComponent<Rigidbody>();
                body.AddForce(dir * explosionForce, ForceMode.Impulse);
                body.angularVelocity = dir;
            }
        }
    }
}
