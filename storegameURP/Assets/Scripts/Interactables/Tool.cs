using UnityEngine;

public class Tool : Pickuppable
{
    [SerializeField] private Vector3 holdPosition;
    [SerializeField] private Vector3 holdRotation;
    [SerializeField] private float cameraAngleMultiplier = 0.005f;

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

    protected override void Pickup(bool pickup)
    {
        base.Pickup(pickup);

        if (pickup)
        { transform.localRotation = Quaternion.Euler(holdRotation); }
        else
        {
            transform.localPosition = Vector3.forward;
            transform.LookAt(Interaction.PlayerTransform);
        }

        transform.parent = pickup ? Interaction.PlayerTransform : null;

        if (pickup)
        { Rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; }
        Rb.isKinematic = pickup;
        if (!pickup)
        { Rb.collisionDetectionMode = CollisionDetectionMode.Continuous; }
    }
}
