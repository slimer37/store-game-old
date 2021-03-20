using UnityEngine;

public class Tool : Pickuppable
{
    [SerializeField] bool anchorToCamera;
    [SerializeField] Vector3 holdPosition;
    [SerializeField] Vector3 holdRotation;
    [SerializeField] float cameraAngleMultiplier = 0.005f;

    protected Controls Controls { get; private set; }
    protected Vector3 HoldRotation => holdRotation;

    Collider[] detected = new Collider[1];

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

        float angle = Interaction.Current.Cam.transform.eulerAngles.x;
        if (angle > 180)
        { angle -= 360; }
        float delta = -angle * cameraAngleMultiplier;
        transform.localPosition = holdPosition + Vector3.up * delta;
    }

    protected override void Pickup(bool pickup)
    {
        if (pickup)
        {
            transform.parent = anchorToCamera ? Interaction.Current.Cam.transform : Interaction.Current.transform;
            transform.localRotation = Quaternion.Euler(holdRotation);
            transform.localPosition = holdPosition;
            Controls.Enable();
        }
        else
        {
            if (!CheckDropPos) return;

            transform.position = Interaction.Current.transform.position + Interaction.Current.transform.forward;
            transform.parent = null;
            transform.LookAt(Interaction.Current.transform);
            transform.eulerAngles += Vector3.up * 180;
            Controls.Disable();
        }

        base.Pickup(pickup);
        Freeze(pickup);
    }

    protected bool CheckDropPos => Physics.OverlapSphereNonAlloc(Interaction.Current.transform.position + Interaction.Current.transform.forward, 0.25f, detected, ~LayerMask.GetMask("Pickuppables")) == 0;
}
