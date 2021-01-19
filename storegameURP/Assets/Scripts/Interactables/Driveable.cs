using UnityEngine;
using UnityEngine.InputSystem;

public class Driveable : Interactable
{
    [SerializeField] private float driveSpeed;
    [SerializeField] private float driveSprintSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float onTurnOffset;

    [Header("Positioning")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 cameraPosition;

    protected Rigidbody Rb { get; private set; }
    protected Vector3 InputDirection { get; private set; }
    protected Vector3 MoveDirection { get; private set; }

    private Controls controls;
    private bool sprinting = false;

    private float offsetT;

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Player.Movement.performed += OnMovement;
        controls.Player.Sprint.performed += OnSprint;
        controls.Player.ExitVehicle.performed += _ => BeginDriving(false);
        Rb = GetComponent<Rigidbody>();
    }

    protected virtual void BeginDriving(bool value)
    {
        Movement.Enable(!value);

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Interaction.PlayerTransform.parent = value ? transform : null;

        if (value)
        { Rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; }
        Rb.isKinematic = value;
        if (!value)
        { Rb.collisionDetectionMode = CollisionDetectionMode.Continuous; }

        if (value)
        {
            Interaction.PlayerTransform.localRotation = Quaternion.identity;
            Interaction.PlayerTransform.localPosition = playerPosition;
            controls.Enable();
        }
        else
        { controls.Disable(); }
    }

    public override void Interact() => BeginDriving(!Rb.isKinematic);
    void OnSprint(InputAction.CallbackContext ctx) => sprinting = ctx.ReadValue<float>() > 0;
    protected virtual void OnMovement(InputAction.CallbackContext ctx)
    {
        InputDirection = ctx.ReadValue<Vector2>();
        offsetT = 0;
    }

    protected virtual void Update()
    {
        if (!MenuManager.Current.MenuOpen && Rb.isKinematic)
        {
            MoveDirection = InputDirection.y * transform.forward;
            float speed = sprinting ? driveSprintSpeed : driveSpeed;
            Rb.MovePosition(transform.position + MoveDirection * speed * Time.deltaTime);

            if (InputDirection.y != 0)
            {
                var turnDelta = Vector3.up * InputDirection.x * turnSpeed;
                Rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + turnDelta));
            }

            var offsetPos = playerPosition + Vector3.right * InputDirection.x * onTurnOffset;
            Interaction.PlayerTransform.localPosition = Vector3.Lerp(Interaction.PlayerTransform.localPosition, offsetPos, offsetT += Time.deltaTime);
        }
        else
        { MoveDirection = Vector3.zero; }
    }
}
