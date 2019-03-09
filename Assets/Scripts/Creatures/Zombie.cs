using UnityEngine;

public class Zombie : Creature
{
    public Zombie()
    {
        health = 10f;
    }

    protected override void Kill()
    {
        Transform[] allParts;
        Vector3 velocity = body.velocity;
        isAlive = false;

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());

        allParts = GetComponentsInChildren<Transform>();
        foreach (var item in allParts)
        {
            if (item.parent)
            {
                Vector3 randomDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                Material material = item.GetComponent<Renderer>().material;

                item.gameObject.AddComponent<BoxCollider>();
                var bodyPart = item.gameObject.AddComponent<Rigidbody>();
                bodyPart.AddForce(velocity + randomDir, ForceMode.Impulse);
                bodyPart.angularVelocity = randomDir;
                StartCoroutine(FadeOut(material));
            }
        }
    }
}
