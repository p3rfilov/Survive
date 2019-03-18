using System.Collections;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public bool isAlive;
    public LayerMask ground;

    protected float health;
    protected float moveSpeed;
    protected float jumpHeight;
    protected float groundDistance = 0.2f;
    protected float timeUntilFade = 3f;
    protected float fadeTime = 3f;
    protected Rigidbody body;

    public Creature()
    {
        isAlive = true;
        health = 5f;
        moveSpeed = 3f;
        jumpHeight = 0f;
    }

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        if (health <= 0f && isAlive)
        {
            Kill();
        }
    }

    protected virtual void Attack()
    {
        Weapon weapon = GetComponent<Weapon>();
        if (weapon != null)
        {
            weapon.Use();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
    }

    protected virtual void Kill()
    {
        Transform[] allParts;
        Vector3 velocity = body.velocity;
        float mass = body.mass;
        isAlive = false;

        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());

        allParts = GetComponentsInChildren<Transform>();
        foreach (var item in allParts)
        {
            if (item.parent)
            {
                Material material = item.GetComponent<Renderer>().material;

                item.gameObject.AddComponent<BoxCollider>();
                var bodyPart = item.gameObject.AddComponent<Rigidbody>();
                bodyPart.mass = mass;
                bodyPart.AddForce(velocity, ForceMode.VelocityChange);
                bodyPart.angularVelocity = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                StartCoroutine(FadeOut(material));
            }
        }
    }

    public virtual IEnumerator FadeOut(Material material)
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
