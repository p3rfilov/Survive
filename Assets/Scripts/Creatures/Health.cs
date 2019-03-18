using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health;

    private float timeUntilFade = 3f;
    private float fadeTime = 3f;
    private Rigidbody body;
    private CapsuleCollider coll;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Transform[] allParts;
        Vector3 velocity = body.velocity;
        float mass = body.mass;

        Destroy(body);
        Destroy(coll);

        allParts = GetComponentsInChildren<Transform>();
        foreach (var item in allParts)
        {
            Renderer rend = item.GetComponent<Renderer>();
            if (rend != null)
            {
                item.gameObject.AddComponent<BoxCollider>();
                var bodyPart = item.gameObject.AddComponent<Rigidbody>();
                bodyPart.mass = mass;
                bodyPart.AddForce(Vector3.Normalize(velocity), ForceMode.VelocityChange);
                bodyPart.angularVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

                Material material = rend.material;
                StartCoroutine(FadeOut(material));
            }
        }
    }

    private IEnumerator FadeOut(Material material)
    {
        Color color = material.color;
        float startOpacity = color.a;
        float t = 0;

        while (t < timeUntilFade)
        {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / fadeTime);
            color.a = Mathf.Lerp(startOpacity, 0, blend);
            material.color = color;
            yield return null;
        }
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
