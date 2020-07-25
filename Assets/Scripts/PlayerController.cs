using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ItemHolder))]
public class PlayerController : MonoBehaviour
{
    public bool mouseLook = true;
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public LayerMask ground;
    public LayerMask dontAimAt;

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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~dontAimAt))
                {
                    Vector3 targetPosition;
                    if (hit.transform.CompareTag("Ground"))
                    {
                        targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        transform.LookAt(targetPosition);
                        itemHolder.holdingHand.rotation = transform.rotation;
                    }
                    else if (!hit.transform.CompareTag("Player") && hit.transform.GetComponent<Health>() != null)  // something killable, except self
                    {
                        targetPosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                        transform.LookAt(targetPosition);
                        var _collider = hit.transform.GetComponent<Collider>();
                        if (_collider != null)
                        {
                            itemHolder.holdingHand.LookAt(new Vector3(targetPosition.x, _collider.bounds.center.y, targetPosition.z));
                        }
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

            if (Input.GetButtonDown("Drop Item"))
            {
                itemHolder.DropCurrentItem();
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
