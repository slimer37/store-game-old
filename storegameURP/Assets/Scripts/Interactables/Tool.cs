using UnityEngine;
using UnityEngine.Events;

public class Tool : Pickuppable
{
    [SerializeField] private Vector3 holdPosition;
    [SerializeField] private Vector3 holdRotation;
    [SerializeField] private float cameraAngleMultiplier = 0.005f;

    protected Controls Controls { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Controls = new Controls();
    }

    public override void Interact()
    {
        if (!IsHeld)
        { Pickup(true); }
    }

    void Update()
    {
        if (!IsHeld) return;

        float angle = Interaction.CamTransform.eulerAngles.x;
        if (angle > 180)
        { angle -= 360; }
        float delta = -angle * cameraAngleMultiplier;
        transform.localPosition = holdPosition + Vector3.up * delta;
    }

    protected override void FixedUpdate() { }

    protected override void Pickup(bool pickup)
    {
        base.Pickup(pickup);

        if (pickup)
        {
            transform.parent = Interaction.PlayerTransform;
            transform.localRotation = Quaternion.Euler(holdRotation);
            Controls.Enable();
        }
        else
        {
            transform.localPosition = Vector3.forward;
            transform.LookAt(Interaction.PlayerTransform);
            transform.parent = null;
            Controls.Disable();
        }

        Rb.constraints = pickup ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }
}
