using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health;
    public float timeUntilFade = 3f;
    public float fadeTime = 3f;
    public bool allowDrops;

    Rigidbody body;
    Collider coll;
    NavMeshAgent agent;
    float mass;
    bool isAlive = true;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (transform.tag == "Player")
        {
            EventManager.RaiseOnPlayerHealthChanged();
        }
        if (health <= 0)
        {
            if (agent != null)
            {
                agent.enabled = false;
            }
            if (isAlive)
            {
                Segment();
                StartCoroutine(RaiseOnSomethingDiedDelayed());
            }
            isAlive = false;
        }
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        if (body != null)
        {
            mass = body.mass;
        }
    }

    void Segment ()
    {
        if ((timeUntilFade > 0 && fadeTime > 0) || (body != null && coll != null))
        {
            body.detectCollisions = false;
            coll.enabled = false;
            Destroy(body);
            Destroy(coll);

            Transform[] allParts = GetComponentsInChildren<Transform>();
            foreach (var item in allParts)
            {
                Renderer rend = item.GetComponent<Renderer>();
                if (rend != null)
                {
                    item.gameObject.AddComponent<BoxCollider>();
                    var bodyPart = item.gameObject.AddComponent<Rigidbody>();
                    bodyPart.isKinematic = false;
                    bodyPart.mass = mass;

                    Material material = rend.material;
                    StartCoroutine(FadeOut(material));
                }
            }
        }
        else
        {
            PoolingManager.Remove(gameObject);
        }
    }

    IEnumerator FadeOut(Material material)
    {
        if (material.HasProperty("_Color") && transform.tag != "Player")  // keep player object in the scene
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
                PoolingManager.Remove(gameObject);
            }
        }
    }

    IEnumerator RaiseOnSomethingDiedDelayed ()
    {
        yield return new WaitForSeconds(timeUntilFade);
        EventManager.RaiseOnSomethingDied(transform);
    }
}
