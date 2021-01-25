using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private float reach;
    [SerializeField] private LayerMask interactablesMask;

    [Header("Items")]
    [SerializeField] private float minHoldDist;
    [SerializeField] private float dropDist;
    [SerializeField] private float throwForce;

    [Header("Correction")]
    [SerializeField] private float correctionForce;
    [SerializeField] private float correctionDist;

    private Transform hoveredTransform;
    private float heldDistance;
    private Pickuppable held = null;

    public Camera Cam { get; private set; }
    public static Interaction Current { get; private set; }

    void Awake()
    {
        Cam = GetComponent<PlayerInput>().camera;
        Current = this;
    }

    void Update()
    {
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        hoveredTransform = Hover.Current.Cast(ray, reach, interactablesMask);
    }

    void FixedUpdate()
    {
        // Tools don't get moved like generic pickuppables.
        if (!held || held is Tool) return;

        Vector3 targetPoint = Cam.transform.position + Cam.transform.forward * heldDistance;
        held.PullTowards(targetPoint, correctionDist, correctionForce);

        Vector3 direction = held.transform.position - Cam.transform.position;
        if (Vector3.Distance(held.transform.position, targetPoint) > dropDist
            || Physics.Raycast(new Ray(Cam.transform.position, direction), out RaycastHit hit, ~LayerMask.NameToLayer("Pickuppables")) && hit.transform != held.transform)
        { held.Drop(); }
    }

    public void Grab(Pickuppable item)
    {
        Hover.Current.Reset();

        var temp = held;
        held = item;

        // Don't do special stuff with tools.
        if (item is Tool || held is Tool) return;

        // If we were holding an item before, re-enable collisions.
        if (temp)
        { temp.IgnoreCollision(col, false); }

        // If we picked up an item, set the distance and ignore collisions.
        if (held)
        {
            held.IgnoreCollision(col, true);
            float newDist = Vector3.Distance(Current.transform.position, item.transform.position);
            Current.heldDistance = Mathf.Clamp(newDist, Current.minHoldDist, Current.reach);
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
        if (held && !(held is Tool))
        { held.Throw(throwForce * Cam.transform.forward); }
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
