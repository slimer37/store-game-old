using UnityEngine;
using UnityEngine.InputSystem;

public class Driveable : Interactable
{
    [Header("Speed")]
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float sprintAcceleration;
    [SerializeField] private float sprintMaxSpeed;
    [SerializeField] private float deceleration;

    [Header("Turning")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float turnDropoff;
    [SerializeField] private float maxTurnSpeed;
    [SerializeField] private float onTurnOffset;


    [Header("Other")]
    [SerializeField] private Vector3 playerPosition;
    [field: SerializeField] protected Rigidbody Rb { get; private set; }
    [SerializeField] private TMPro.TextMeshProUGUI exitInstructions;

    protected Vector2 InputDirection { get; set; }
    protected Vector3 MoveDirection { get; set; }
    protected bool Driving { get; private set; } = false;

    private Controls controls;
    private bool sprinting = false;

    private float offsetT;
    private float turnResetT;
    private float currentTurnSpeed;

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Player.Movement.performed += OnMovement;
        controls.Player.Sprint.performed += OnSprint;
        controls.Player.ExitVehicle.performed += _ => StartDriving(false);
    }

    protected virtual void StartDriving(bool value)
    {
        // Don't drive if the player area is obstructed.
        var playerPos = transform.position + transform.rotation * playerPosition;
        if (value && Physics.OverlapSphereNonAlloc(playerPos, 0.25f, new Collider[1], LayerMask.NameToLayer("Player")) > 0) return;

        Rb.constraints = value ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Movement.Enable(!value);
        Driving = value;
        offsetT = 0;
        InputDirection = Vector2.zero;

        var player = Interaction.Current.transform;
        player.parent = value ? transform : null;
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        player.rotation = transform.rotation;

        if (value)
        { controls.Enable(); }
        else
        {
            controls.Disable();
            player.position = playerPos;
        }

        if (value)
        { StartCoroutine(FadeText()); }
        else
        { exitInstructions.gameObject.SetActive(false); }
    }

    System.Collections.IEnumerator FadeText()
    {
        exitInstructions.gameObject.SetActive(true);
        exitInstructions.CrossFadeAlpha(1, 0, false);
        yield return new WaitForSeconds(2);
        exitInstructions.CrossFadeAlpha(0, 1, false);
    }

    public override void Interact() => StartDriving(!Driving);
    void OnSprint(InputAction.CallbackContext ctx) => sprinting = ctx.ReadValue<float>() > 0;
    protected virtual void OnMovement(InputAction.CallbackContext ctx)
    {
        InputDirection = ctx.ReadValue<Vector2>();

        // Always reset side-to-side animation var.
        offsetT = 0;

        // Begin resetting turn when turn stops.
        if (InputDirection.x == 0)
        { turnResetT = 0; }
    }

    void Update()
    {
        if (MenuManager.Current.MenuOpen || !Driving)
        {
            MoveDirection = Vector3.zero;
            return;
        }

        if (currentTurnSpeed < maxTurnSpeed)
        { currentTurnSpeed += Time.deltaTime * turnSpeed * InputDirection.x; }

        if (InputDirection.x == 0)
        { currentTurnSpeed = Mathf.Lerp(currentTurnSpeed, 0, turnResetT += Time.deltaTime * turnDropoff); }

        var turnDelta = Vector3.up * currentTurnSpeed;
        Rb.MoveRotation(Quaternion.Euler(Rb.rotation.eulerAngles + turnDelta));

        // Animate side to side--will also lerp the player position when first driving.
        var offsetPos = playerPosition + Vector3.right * InputDirection.x * onTurnOffset;
        Interaction.Current.transform.localPosition = Vector3.Lerp(Interaction.Current.transform.localPosition, offsetPos, offsetT += Time.deltaTime);

    }

    protected virtual void FixedUpdate()
    {
        if (MenuManager.Current.MenuOpen || !Driving) return;

        MoveDirection = InputDirection.y * transform.forward;
        float accel = sprinting ? sprintAcceleration : acceleration;
        float max = sprinting ? sprintMaxSpeed : maxSpeed;

        if (Rb.velocity.sqrMagnitude < max * max)
        { Rb.velocity += MoveDirection * accel * Time.fixedDeltaTime; }
        
        if (InputDirection.y == 0)
        { Rb.velocity -= MoveDirection * deceleration * Time.fixedDeltaTime; }
    }
}
