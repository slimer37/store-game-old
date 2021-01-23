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
    [SerializeField] private float correctionDist;
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
            CursorIcon.Reset();

            if (value is Tool || Held is Tool)
            {
                current.held = value;
                return;
            }

            Physics.IgnoreCollision(current.col, value ? value.Col : Held.Col, value);

            current.held = value;

            if (value)
            {
                float newDist = Vector3.Distance(current.transform.position, value.Rb.position);
                current.heldDistance = Mathf.Clamp(newDist, current.minHoldDist, current.reach);
            }
        }
    }
    public static Transform PlayerTransform => current.transform;
    public static Transform CamTransform => current.cam.transform;
    public static Transform HoveredTransform => current.hoveredTransform;

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        current = this;
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, reach) && hit.transform.CompareTag("Interactable"))
        {
            hoveredTransform = hit.transform;

            if (!Held || hit.transform != Held.transform)
            { hit.transform.SendMessage("Hover", SendMessageOptions.DontRequireReceiver); }

            if (hoveredTransform && hit.transform != hoveredTransform)
            { hoveredTransform.SendMessage("HoverExit", SendMessageOptions.DontRequireReceiver); }
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
        if (!Held || Held is Tool) return;

        Vector3 targetPoint = cam.transform.position + cam.transform.forward * heldDistance;

        if (Vector3.Distance(Held.transform.position, targetPoint) < correctionDist)
        {
            Held.Rb.velocity = Held.Rb.velocity / 2;
            return;
        }

        Vector3 force = targetPoint - Held.transform.position;
        force = force.normalized * Mathf.Sqrt(force.magnitude);

        Held.Rb.velocity = force.normalized * Held.Rb.velocity.magnitude;
        Held.Rb.AddForce(force * correctionForce);

        Held.Rb.velocity *= Mathf.Min(1.0f, force.magnitude / 2);

        Vector3 direction = Held.transform.position - cam.transform.position;
        if (Vector3.Distance(Held.transform.position, targetPoint) > dropDist
            || Physics.Raycast(new Ray(cam.transform.position, direction), out RaycastHit hit) && hit.transform != Held.transform)
        { Held.Drop(); }
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
        if (held && !(held is Tool))
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
