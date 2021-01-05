using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private float reach;

    [Header("Items")]
    [SerializeField] private float minHoldDist;
    [SerializeField] private float throwForce;
    [SerializeField] private float correctionForce;
    [SerializeField] private float dropDist;

    private static Interaction current;
    private Camera cam;
    private Transform hoveredTransform;
    private float heldDistance;
    private Pickuppable held = null;

    public static Pickuppable Held
    {
        get => current.held;
        set
        {
            if (value)
            {
                float newDist = Vector3.Distance(current.transform.position, value.Rb.position);
                current.heldDistance = Mathf.Clamp(newDist, current.minHoldDist, current.reach);
            }
            Physics.IgnoreCollision(current.col, value ? value.Col : Held.Col, value);

            current.held = value;
        }
    }
    public static Transform HoveredTransform => current.hoveredTransform;

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        current = this;
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, reach))
        {
            if (!hit.transform.CompareTag("Interactable"))
            { CursorIcon.Reset(); }

            hit.transform.SendMessage("Hover", SendMessageOptions.DontRequireReceiver);

            if (hoveredTransform && hit.transform != hoveredTransform)
            { hoveredTransform.SendMessage("HoverExit", SendMessageOptions.DontRequireReceiver); }

            hoveredTransform = hit.transform;
        }
        else
        {
            CursorIcon.Reset();
            if (hoveredTransform)
            {
                hoveredTransform.SendMessage("HoverExit", SendMessageOptions.DontRequireReceiver);
                hoveredTransform = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (held)
        {
            Vector3 targetPoint = cam.transform.position + cam.transform.forward * heldDistance;
            Vector3 force = targetPoint - held.transform.position;

            // Thx myx - https://forum.unity.com/threads/half-life-2-object-grabber.79352/
            force = force.normalized * Mathf.Sqrt(force.magnitude);

            held.Rb.velocity = force.normalized * held.Rb.velocity.magnitude;
            held.Rb.AddForce(force * correctionForce);

            held.Rb.velocity *= Mathf.Min(1.0f, force.magnitude / 2);

            Vector3 direction = held.transform.position - cam.transform.position;
            if (Vector3.Distance(held.transform.position, targetPoint) > dropDist ||
                Physics.Raycast(new Ray(cam.transform.position, direction), out RaycastHit hit) && hit.transform != held.transform)
            { held.Drop(); }
        }
    }

    void OnShiftItem(InputValue value)
    {
        if (held)
        {
            float newDist = heldDistance + value.Get<float>() / 120 * 0.5f;
            heldDistance = Mathf.Clamp(newDist, minHoldDist, reach);
        }
    }

    void OnThrow()
    {
        if (held)
        {
            held.Rb.AddForce(throwForce * cam.transform.forward, ForceMode.Impulse);
            held.Drop();
        }
    }

    void OnInteract()
    {
        if (held)
        { held.Drop(); }
        else if (hoveredTransform)
        { hoveredTransform.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver); }
    }

    void OnSecondaryInteract()
    {
        if (hoveredTransform)
        { hoveredTransform.SendMessage("OnSecondaryInteract", SendMessageOptions.DontRequireReceiver); }
    }
}
