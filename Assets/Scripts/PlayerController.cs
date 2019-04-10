using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ItemHolder))]
public class PlayerController : MonoBehaviour
{
    public bool mouseLook = true;
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public LayerMask ground;

    private float groundDistance = 0.2f;
    private bool isGrounded = true;
    private Vector3 inputs = Vector3.zero;
    private float airControl = 0.35f;

    private Quaternion viewRotation;
    private Rigidbody body;
    private ItemHolder itemHolder;

    private void Start()
    {
        viewRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        body = GetComponent<Rigidbody>();
        itemHolder = GetComponent<ItemHolder>();
    }

    private void Update()
    {
        if (body != null)
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
            float _airControl = 1f;

            if (!isGrounded) _airControl = airControl;
            inputs = Vector3.zero;
            inputs.x = Input.GetAxis("Horizontal");
            inputs.z = Input.GetAxis("Vertical");
            body.MovePosition(body.position + viewRotation * inputs * moveSpeed * _airControl * Time.fixedDeltaTime);
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

            if (Input.GetButtonDown("Next Item"))
            {
                itemHolder.CicleItems(1);
            }

            if (Input.GetButtonDown("Last Item"))
            {
                itemHolder.CicleItems(-1);
            }

            if (Input.GetButton("Fire1"))
            {
                var usable = itemHolder.Object?.GetComponent<IUsable>();
                if (usable != null)
                {
                    usable.Use();
                }
            }
        }
    }
}
