using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    [SerializeField] Animator anim;

    [Header("Sprinting")]
    [SerializeField] float sprintSpeed;
    [SerializeField] float sprintAnimSpeed;

    CharacterController controller;
    Vector3 inputDirection;
    Vector3 moveDirection;
    bool sprinting = false;

    static Movement current;

    public static void Enable(bool value)
    {
        current.controller.enabled = value;
        current.enabled = value;
    }

    void Awake()
    {
        current = this;
        TryGetComponent(out controller);
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
        { moveDirection = Vector3.up * inputDirection.y; }

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
