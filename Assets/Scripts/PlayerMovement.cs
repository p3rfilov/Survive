using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 5f;
    public float groundDistance = 0.2f;
    public LayerMask ground;

    private Rigidbody body;
    private Vector3 inputs = Vector3.zero;
    private bool isGrounded = true;
    private Quaternion viewRotation = Quaternion.AngleAxis(45, Vector3.up);

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        if (inputs != Vector3.zero)
            transform.forward = viewRotation * inputs;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(Vector3.up * jumpStrength, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + viewRotation * inputs * speed * Time.fixedDeltaTime);
        body.angularVelocity = Vector3.zero;
    }
}
