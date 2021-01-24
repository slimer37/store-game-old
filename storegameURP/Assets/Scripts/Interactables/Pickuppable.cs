using UnityEngine;

public class Pickuppable : Interactable
{
    protected override Hover.Icon HoverIcon { get => isHeld ? Hover.Icon.None : Hover.Icon.Pickup; }

    protected Rigidbody rb;
    protected Collider col;

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
        TryGetComponent(out rb);
        TryGetComponent(out col);
    }

    protected override void OnHover()
    {
        if (!IsHeld)
        { base.OnHover(); }
    }

    public override void Interact()
    {
        if (!isHeld)
        { Pickup(true); }
    }

    protected virtual void Pickup(bool pickup)
    {
        Interaction.Current.Grab(pickup ? this : null);
        rb.useGravity = !pickup;
        isHeld = pickup;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void IgnoreCollision(Collider other, bool ignore) => Physics.IgnoreCollision(col, other, ignore);
    public void Drop() => Pickup(false);

    public void Throw(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
        Drop();
    }

    public void Freeze(bool kinematic, RigidbodyConstraints constraints = RigidbodyConstraints.None)
    {
        if (!kinematic) { rb.isKinematic = kinematic; }
        rb.collisionDetectionMode = kinematic ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Continuous;
        if (kinematic) { rb.isKinematic = kinematic; }
        rb.detectCollisions = !kinematic;

        rb.constraints = constraints;
    }

    public void PullTowards(Vector3 targetPoint, float correctionDist, float correctionForce)
    {
        if (Vector3.Distance(transform.position, targetPoint) < correctionDist)
        {
            rb.velocity /= 2;
            return;
        }

        Vector3 force = targetPoint - transform.position;
        force = force.normalized * Mathf.Sqrt(force.magnitude);

        rb.velocity = force.normalized * rb.velocity.magnitude;
        rb.AddForce(force * correctionForce);

        rb.velocity *= Mathf.Min(1.0f, force.magnitude / 2);
    }
}
