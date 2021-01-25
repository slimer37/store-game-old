using UnityEngine;
using UnityEngine.InputSystem;

public class Driveable : Interactable
{
    [SerializeField] private float driveSpeed;
    [SerializeField] private float driveSprintSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float onTurnOffset;
    [SerializeField] private TMPro.TextMeshProUGUI exitInstructions;
    [field: SerializeField] protected Rigidbody Rb { get; private set; }

    [Header("Positioning")]
    [SerializeField] private Vector3 playerPosition;

    protected Vector2 InputDirection { get; private set; }
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
        offsetT = 0;
    }

    protected virtual void Update()
    {
        if (!MenuManager.Current.MenuOpen && Driving)
        {
            MoveDirection = InputDirection.y * transform.forward;
            float speed = sprinting ? driveSprintSpeed : driveSpeed;

            if (Rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
            { Rb.AddForce(MoveDirection * speed, ForceMode.Acceleration); }

            if (InputDirection.y != 0)
            {
                var turnDelta = Vector3.up * InputDirection.x * turnSpeed;
                Rb.MoveRotation(Quaternion.Euler(Rb.rotation.eulerAngles + turnDelta));
            }

            // Animate side to side--will also lerp the player position when first driving.
            var offsetPos = playerPosition + Vector3.right * InputDirection.x * onTurnOffset;
            Interaction.Current.transform.localPosition = Vector3.Lerp(Interaction.Current.transform.localPosition, offsetPos, offsetT += Time.deltaTime);
        }
        else
        { MoveDirection = Vector3.zero; }
    }
}
