using System.Collections;
using UnityEngine;

public class PlayerStencilController : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask layerMask;
    public GameObject stencilObject;
    public float minScale;
    public float scaleTime;

    Collider playerCollider;
    float maxScale;
    string currentTag;
    bool scaling;

    void Start ()
    {
        playerCollider = transform.GetComponent<Collider>();
        maxScale = stencilObject.transform.localScale.x;
    }

    void Update()
    {
        if (playerCollider != null && playerCamera != null && stencilObject != null)
        {
            RaycastHit hit;

            Vector3 vector = (stencilObject.transform.position - playerCamera.transform.position).normalized;
            if (Physics.Raycast(playerCamera.transform.position, vector, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.tag != currentTag && !scaling)
                {
                    currentTag = hit.transform.tag;
                    if (hit.transform.tag == playerCollider.tag)
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
