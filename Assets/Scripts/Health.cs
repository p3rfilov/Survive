using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int health;
    public float timeUntilFade = 3f;
    public float fadeTime = 3f;

    private Rigidbody body;
    private CapsuleCollider coll;
    private NavMeshAgent agent;
    private bool isAlive = true;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (transform.tag == "Player")
        {
            EventManager.RaiseOnPlayerHealthChanged();
        }
        if (health <= 0)
        {
            if (agent != null && body != null)
            {
                agent.enabled = false;
                body.isKinematic = false;
            }
            if (isAlive)
                StartCoroutine(Kill());
            isAlive = false;
        }
    }

    public IEnumerator Kill()
    {
        yield return new WaitForFixedUpdate();

        StartCoroutine(RaiseOnSomethingDiedDelayed());
        if ((timeUntilFade > 0 && fadeTime > 0) || (body != null && coll != null))
        {
            Transform[] allParts;
            Vector3 velocity = body.velocity;
            Vector3 angVelocity = body.angularVelocity;
            float mass = body.mass;

            Destroy(body);
            Destroy(coll);

            allParts = GetComponentsInChildren<Transform>();
            foreach (var item in allParts)
            {
                Renderer rend = item.GetComponent<Renderer>();
                Vector3 random = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                if (rend != null)
                {
                    item.gameObject.AddComponent<BoxCollider>();
                    var bodyPart = item.gameObject.AddComponent<Rigidbody>();
                    bodyPart.mass = mass;
                    bodyPart.velocity = velocity + random;
                    bodyPart.angularVelocity = angVelocity + random;

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

    private IEnumerator FadeOut(Material material)
    {
        if (material.HasProperty("_Color") && body.tag != "Player")  // keep player object in the scene
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

    private IEnumerator RaiseOnSomethingDiedDelayed ()
    {
        yield return new WaitForSeconds(timeUntilFade);
        EventManager.RaiseOnSomethingDied(transform);
    }
}
