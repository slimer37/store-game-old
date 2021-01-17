using UnityEngine;

public class Pickuppable : Interactable
{
    protected override CursorIcon.Icon HoverIcon { get => isHeld ? CursorIcon.Icon.None : CursorIcon.Icon.Pickup; }

    public Rigidbody Rb { get; private set; }
    public Collider Col { get; private set; }

    protected bool isHeld = false;
    public bool IsHeld => isHeld;
    public Vector3 OriginalScale { get; private set; }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (gameObject.layer != 3)
        {
            gameObject.layer = 3;
            Debug.LogWarning($"Set layer of pickuppable {name} to '{LayerMask.LayerToName(3)}' layer.");
        }
    }

    protected virtual void Awake()
    {
        OriginalScale = transform.localScale;
        Rb = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
    }

    public override void Interact()
    {
        if (!isHeld)
        { Pickup(true); }
    }

    protected virtual void Pickup(bool pickup)
    {
        Interaction.Held = pickup ? this : null;

        Rb.useGravity = !pickup;
        isHeld = pickup;

        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (!IsHeld) return;

        Vector3 targetPoint = Interaction.TargetHoldPoint;

        if (Vector3.Distance(transform.position, targetPoint) < Interaction.CorrectionDist)
        {
            Rb.velocity = Rb.velocity / 2;
            return;
        }

        Vector3 force = targetPoint - transform.position;
        force = force.normalized * Mathf.Sqrt(force.magnitude);

        Rb.velocity = force.normalized * Rb.velocity.magnitude;
        Rb.AddForce(force * Interaction.CorrectionForce);

        Rb.velocity *= Mathf.Min(1.0f, force.magnitude / 2);

        Vector3 direction = transform.position - Interaction.CamTransform.position;
        if (Vector3.Distance(transform.position, targetPoint) > Interaction.DropDist
            || Physics.Raycast(new Ray(Interaction.CamTransform.position, direction), out RaycastHit hit) && hit.transform != transform)
        { Drop(); }
    }

    public void Drop() => Pickup(false);

    public void MakeKinematic(bool value)
    {
        if (!value) { Rb.isKinematic = value; }
        Rb.collisionDetectionMode = value ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Continuous;
        if (value) { Rb.isKinematic = value; }
        Rb.detectCollisions = !value;
    }
}
