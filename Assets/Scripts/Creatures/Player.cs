using UnityEngine;

public class Player : Creature
{
    public bool mouseLook = true;

    private bool isGrounded = true;
    private Vector3 inputs = Vector3.zero;
    private Quaternion viewRotation;

    public Player()
    {
        health = 100f;
        moveSpeed = 5f;
        jumpHeight = 5f;
    }

    protected override void Start()
    {
        viewRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        base.Start();
    }

    protected override void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        body.MovePosition(body.position + viewRotation * inputs * moveSpeed * Time.fixedDeltaTime);
        body.angularVelocity = Vector3.zero;

        if (mouseLook)
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Ground"))
                {
                    Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    transform.LookAt(targetPosition);
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    Vector3 targetPosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                    transform.LookAt(targetPosition);
                }
            }
        }
        else if (inputs != Vector3.zero)
        {
            transform.forward = viewRotation * inputs;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
            isGrounded = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
}
