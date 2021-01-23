using UnityEngine;

public class Tool : Pickuppable
{
    [SerializeField] private bool anchorToCamera;
    [SerializeField] private Vector3 holdPosition;
    [SerializeField] private Vector3 holdRotation;
    [SerializeField] private float cameraAngleMultiplier = 0.005f;

    protected Controls Controls { get; private set; }
    protected Vector3 HoldRotation => holdRotation;

    private Collider[] detected = new Collider[1];

    protected override void Awake()
    {
        base.Awake();
        Controls = new Controls();
        Controls.Player.Use.performed += OnUse;
        Controls.Player.SecondaryUse.performed += OnSecondaryUse;
    }

    void OnDisable() => Controls.Disable();
    protected virtual void OnUse(UnityEngine.InputSystem.InputAction.CallbackContext ctx) { }
    protected virtual void OnSecondaryUse(UnityEngine.InputSystem.InputAction.CallbackContext ctx) { }

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
        if (pickup)
        {
            transform.parent = anchorToCamera ? Interaction.CamTransform : Interaction.PlayerTransform;
            transform.localRotation = Quaternion.Euler(holdRotation);
            Controls.Enable();
        }
        else
        {
            if (!CheckDropPos) return;

            transform.position = Interaction.PlayerTransform.position + Interaction.PlayerTransform.forward;
            transform.parent = null;
            transform.LookAt(Interaction.PlayerTransform);
            transform.eulerAngles += Vector3.up * 180;
            Controls.Disable();
        }

        //Rb.constraints = pickup ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
        base.Pickup(pickup);
        MakeKinematic(pickup);
    }

    protected bool CheckDropPos => Physics.OverlapSphereNonAlloc(Interaction.PlayerTransform.position + Interaction.PlayerTransform.forward, 0.25f, detected, ~LayerMask.GetMask("Pickuppables")) == 0;
}
