using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    [SerializeField] private float sprintAnimSpeed;

    [Header("Sprinting")]
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float sprintFOV;
    [SerializeField] private float FOVChangeRate;

    private CharacterController controller;
    private Vector3 inputDirection;
    private Vector3 moveDirection;
    private bool sprinting = false;
    private float originalFOV;
    private Camera cam;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponent<PlayerInput>().camera;
        originalFOV = cam.fieldOfView;
    }

    void OnMovement(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        inputDirection = new Vector3(input.x, inputDirection.y, input.y);
    }

    void OnJump()
    {
        if (!MenuManager.MenuOpen && controller.isGrounded)
        { inputDirection.y = jumpForce; }
    }

    void Update()
    {
        if (!controller.isGrounded)
        { inputDirection.y -= 9.8f * Time.deltaTime; }

        if (!MenuManager.MenuOpen)
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

    void OnSprint(InputValue value)
    {
        // Sprinting is true when shift is down and reset when up.
        sprinting = value.isPressed;
        StopAllCoroutines();
        StartCoroutine(AnimateFOV());
    }

    IEnumerator AnimateFOV()
    {
        float finalFOV = sprinting ? sprintFOV : originalFOV;
        yield return Tweens.LerpValue(1 / FOVChangeRate, t =>
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, finalFOV, FOVChangeRate * Time.deltaTime));
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody hitRb;
        if (hitRb = hit.collider.GetComponent<Rigidbody>())
        { hitRb.AddForce(hit.moveDirection * (sprinting ? sprintSpeed : walkSpeed)); }
    }
}
