using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
[RequireComponent(typeof(ItemHolder))]
public class PlayerController : MonoBehaviour
{
    public bool mouseLook = true;
    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public LayerMask ground;
    public LayerMask aimable;

    public Texture2D cursorIdle;
    public Texture2D cursorKillable;
    public Vector2 cursorHotSpot;
    public CursorMode cursorMode = CursorMode.ForceSoftware;

    float groundDistance = 0.2f;
    bool isGrounded = true;
    Vector3 inputs = Vector3.zero;
    float airControl = 0.35f;

    Quaternion viewRotation;
    Rigidbody body;
    ItemHolder itemHolder;

    Vector3 bodyVelocity;
    Vector3 bodyAngularVelocity;

    void Awake ()
    {
        EventManager.OnGamePaused += PausePlayerMotion;
    }

    void Start()
    {
        viewRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        body = GetComponent<Rigidbody>();
        itemHolder = GetComponent<ItemHolder>();
    }

    void Update()
    {
        if (body != null)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                isGrounded = Physics.CheckSphere(transform.position, groundDistance, ground, QueryTriggerInteraction.Ignore);
                float _airControl = 1f;

                if (!isGrounded) _airControl = airControl;
                inputs = Vector3.zero;
                inputs.x = Input.GetAxis("Horizontal");
                inputs.z = Input.GetAxis("Vertical");
                body.MovePosition(body.position + viewRotation * inputs * moveSpeed * _airControl * Time.fixedDeltaTime);
                body.angularVelocity = Vector3.zero;
            }

            if (mouseLook)
            {
                Ray ray;
                RaycastHit hit;

                Cursor.SetCursor(cursorIdle, cursorHotSpot, cursorMode);
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, aimable))
                {
                    Vector3 targetPosition;
                    if (hit.transform.CompareTag("Ground"))
                    {
                        targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        transform.LookAt(targetPosition);
                        itemHolder.holdingHand.rotation = transform.rotation;
                    }
                    else if (hit.transform.GetComponent<Health>() != null)  // something killable, except self
                    {
                        Cursor.SetCursor(cursorKillable, cursorHotSpot, cursorMode);
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
            else if (inputs != Vector3.zero && !EventSystem.current.IsPointerOverGameObject())
            {
                transform.forward = viewRotation * inputs;
            }

            if (Input.GetButtonDown("Jump") && isGrounded && !EventSystem.current.IsPointerOverGameObject())
            {
                body.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
                isGrounded = false;
            }

            if (Input.GetButtonDown("Next Item") || Input.GetAxis("Mouse ScrollWheel") > 0f && !EventSystem.current.IsPointerOverGameObject())
            {
                itemHolder.CicleItems(1);
            }

            if (Input.GetButtonDown("Last Item") || Input.GetAxis("Mouse ScrollWheel") < 0f && !EventSystem.current.IsPointerOverGameObject())
            {
                itemHolder.CicleItems(-1);
            }

            if (Input.GetButtonDown("Drop Item") && !EventSystem.current.IsPointerOverGameObject())
            {
                itemHolder.DropCurrentItem();
            }

            if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {
                var usable = itemHolder.Object?.GetComponent<IUsable>();
                if (usable != null)
                {
                    usable.Use();
                }
            }

            if (Input.GetButton("Fire2") && !EventSystem.current.IsPointerOverGameObject())
            {
                var ammo = itemHolder.Object?.GetComponent<Ammo>();
                if (ammo != null && ammo.AllAmmo > 0 && !ammo.Reloading)
                {
                    StartCoroutine(ammo.Reload());
                }
            }
        }
    }

    void PausePlayerMotion (bool state)
    {
        if (body != null)
        {
            if (state)
            {
                bodyVelocity = body.velocity;
                bodyAngularVelocity = body.angularVelocity;
            }
            else
            {
                body.velocity = bodyVelocity;
                body.angularVelocity = bodyAngularVelocity;
            }
        }
    }
}
