using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public GameObject target;
    public float yOffset;

    void Update()
    {
        var pos = new Vector3();

        if (target != null)
            pos = target.transform.position;
            transform.position = new Vector3(pos.x, pos.y + yOffset, pos.z);
    }
}
