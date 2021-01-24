using UnityEngine;
using UnityEngine.InputSystem;

public class Driveable : Interactable
{
    [SerializeField] private float driveSpeed;
    [SerializeField] private float driveSprintSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float onTurnOffset;
    [field: SerializeField] protected Rigidbody Rb { get; private set; }

    [Header("Positioning")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 cameraPosition;

    protected Vector3 InputDirection { get; private set; }
    protected Vector3 MoveDirection { get; private set; }
    protected bool Driving { get; private set; } = false;

    private Controls controls;
    private bool sprinting = false;

    private float offsetT;

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Player.Movement.performed += OnMovement;
        controls.Player.Sprint.performed += OnSprint;
        controls.Player.ExitVehicle.performed += _ => BeginDriving(false);
    }

    protected virtual void BeginDriving(bool value)
    {
        Driving = value;
        Movement.Enable(!value);

        transform.parent.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Interaction.Current.transform.parent = value ? transform : null;

        Rb.constraints = value ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;

        if (value)
        {
            Interaction.Current.transform.localRotation = Quaternion.identity;
            Interaction.Current.transform.localPosition = playerPosition;
            controls.Enable();
        }
        else
        { controls.Disable(); }
    }

    public override void Interact() => BeginDriving(!Driving);
    void OnSprint(InputAction.CallbackContext ctx) => sprinting = ctx.ReadValue<float>() > 0;
    protected virtual void OnMovement(InputAction.CallbackContext ctx)
    {
        InputDirection = ctx.ReadValue<Vector2>();
        offsetT = 0;
    }

    protected virtual void Update()
    {
        if (!MenuManager.Current.MenuOpen && Driving)
        {
            MoveDirection = InputDirection.y * transform.parent.forward;
            float speed = sprinting ? driveSprintSpeed : driveSpeed;
            transform.parent.Translate(MoveDirection * speed * Time.deltaTime);

            if (InputDirection.y != 0)
            {
                var turnDelta = InputDirection.x * turnSpeed;
                transform.parent.Rotate(Vector3.up, turnDelta);
            }

            // Animate weaving side to side
            var offsetPos = playerPosition + Vector3.right * InputDirection.x * onTurnOffset;
            Interaction.Current.transform.localPosition = Vector3.Lerp(Interaction.Current.transform.localPosition, offsetPos, offsetT += Time.deltaTime);
        }
        else
        { MoveDirection = Vector3.zero; }
    }
}
