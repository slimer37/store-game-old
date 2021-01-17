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
            if (value)
            {
                float newDist = Vector3.Distance(current.transform.position, value.Rb.position);
                current.heldDistance = Mathf.Clamp(newDist, current.minHoldDist, current.reach);
            }
            Physics.IgnoreCollision(current.col, value ? value.Col : Held.Col, value);

            CursorIcon.Reset();
            current.held = value;
        }
    }
    public static Transform PlayerTransform => current.transform;
    public static Transform CamTransform => current.cam.transform;
    public static Vector3 TargetHoldPoint => CamTransform.position + CamTransform.forward * current.heldDistance;

    public static float CorrectionForce => current.correctionForce;
    public static float CorrectionDist => current.correctionDist;
    public static float DropDist => current.dropDist;

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

            if (!Held || hit.transform != Held.transform)
            { hit.transform.SendMessage("Hover", SendMessageOptions.DontRequireReceiver); }
            else if (hit.transform == Held.transform)
            { CursorIcon.Reset(); }

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
