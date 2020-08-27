﻿using System.Collections;
using UnityEngine;

public class PlayerStencilController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject stencilObject;
    public float minScale;
    public float scaleTime;

    Collider playerCollider;
    Health health;
    float maxScale;
    bool scaling;
    string currentTag;
    bool active = true;

    void Start ()
    {
        playerCollider = transform.GetComponent<Collider>();
        health = transform.GetComponent<Health>();
        maxScale = stencilObject.transform.localScale.x;
        StartCoroutine(ScaleDownAnimation(true));
    }

    void Update()
    {
        if (active)
        {
            if (health?.health <= 0)
            {
                StartCoroutine(ScaleDownAnimation(true));
                active = false;
            }
            else if (playerCollider != null && playerCamera != null && stencilObject != null)
            {
                RaycastHit hit;

                Vector3 vector = (stencilObject.transform.position - playerCamera.transform.position).normalized;
                if (Physics.Raycast(playerCamera.transform.position, vector, out hit, Mathf.Infinity))
                {
                    if (!scaling && currentTag != hit.transform.tag)
                    {
                        currentTag = hit.transform.tag;
                        if (hit.collider.tag == playerCollider.tag)
                        {
                            StartCoroutine(ScaleDownAnimation(true));
                        }
                        else
                        {
                            StartCoroutine(ScaleDownAnimation(false));
                        }
                    }
                }
            }
        }
    }

    IEnumerator ScaleDownAnimation (bool state)
    {
        scaling = true;
        Vector3 fromScale = stencilObject.transform.localScale;
        Vector3 toScale;
        float i = 0;
        float rate = 1 / scaleTime;

        if (state)
        {
            toScale = Vector3.one * minScale;
        }
        else
        {
            toScale = Vector3.one * maxScale;
        }

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            stencilObject.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }
        scaling = false;
    }
}
