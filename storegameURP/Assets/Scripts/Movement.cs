using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Animator anim;

    [Header("Sprinting")]
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sprintAnimSpeed;

    private CharacterController controller;
    private Vector3 inputDirection;
    private Vector3 moveDirection;
    private bool sprinting = false;

    private static Movement current;

    public static void Enable(bool value) => current.enabled = value;

    void Awake()
    {
        current = this;
        controller = GetComponent<CharacterController>();
    }

    void OnMovement(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        inputDirection = new Vector3(input.x, inputDirection.y, input.y);
    }

    void OnJump()
    {
        if (!MenuManager.Current.MenuOpen && controller.isGrounded)
        { inputDirection.y = jumpForce; }
    }

    void Update()
    {
        if (!controller.isGrounded)
        { inputDirection.y -= gravity * Time.deltaTime; }

        if (!MenuManager.Current.MenuOpen)
        {
            moveDirection = transform.TransformDirection(inputDirection);
            moveDirection.x *= sprinting ? sprintSpeed : walkSpeed;
            moveDirection.z *= sprinting ? sprintSpeed : walkSpeed;
        }
        else
        { moveDirection = Vector3.zero + Vector3.up * inputDirection.y; }

        anim.SetFloat("Speed", controller.velocity.sqrMagnitude);
        anim.speed = sprinting ? sprintAnimSpeed : 1;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnSprint(InputValue value) => sprinting = value.isPressed;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitRb;
        if (hitRb = hit.collider.GetComponent<Rigidbody>())
        { hitRb.AddForce(hit.moveDirection * (sprinting ? sprintSpeed : walkSpeed)); }
    }
}
