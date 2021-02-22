using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] Collider col;
    [SerializeField] float reach;
    [SerializeField] LayerMask interactablesMask;

    [Header("Items")]
    [SerializeField] float minHoldDist;
    [SerializeField] float dropDist;
    [SerializeField] float throwForce;

    [Header("Correction")]
    [SerializeField] float correctionForce;
    [SerializeField] float correctionDist;

    float heldDistance;
    Pickuppable held = null;

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
        Hover.Current.Cast(ray, reach, interactablesMask);
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

    public void Grab(Pickuppable toPickup)
    {
        Hover.Current.enabled = toPickup != null;

        var temp = held;
        held = toPickup;

        // Don't do special stuff with tools.
        if (toPickup is Tool || held is Tool) return;

        // If we were holding an item before, re-enable collisions.
        if (temp)
        { temp.IgnoreCollision(col, false); }

        // If we picked up an item, set the distance and ignore collisions.
        if (held)
        {
            held.IgnoreCollision(col, true);
            float newDist = Vector3.Distance(Current.transform.position, toPickup.transform.position);
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
        else
        { Hover.Current.SendMessageToHovered("OnInteract"); }
    }

    void OnSecondaryInteract() => Hover.Current.SendMessageToHovered("OnInteract");
}
