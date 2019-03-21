﻿using System.Collections;
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
            StartCoroutine(Kill());
        }
    }

    public IEnumerator Kill()
    {
        yield return new WaitForFixedUpdate();

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
