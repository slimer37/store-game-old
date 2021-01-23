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

    public void Drop() => Pickup(false);

    public void MakeKinematic(bool value)
    {
        if (!value) { Rb.isKinematic = value; }
        Rb.collisionDetectionMode = value ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Continuous;
        if (value) { Rb.isKinematic = value; }
        Rb.detectCollisions = !value;
    }
}
