using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private float verticalClamp;
    [SerializeField] private float sensitivity;

    [Header("Sprinting")]
    [SerializeField] private float sprintFOV;
    [SerializeField] private float FOVChangeRate;

    private Camera cam;
    private Vector2 inputRot;
    private Vector2 camRot;
    private float originalFOV;

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        originalFOV = cam.fieldOfView;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camRot = cam.transform.localEulerAngles;
    }

    void OnLook(InputValue value) => inputRot = value.Get<Vector2>() * sensitivity * Time.deltaTime;

    void Update()
    {
        if (!MenuManager.Current.MenuOpen)
        {
            transform.localEulerAngles += inputRot.x * Vector3.up;

            camRot.x -= inputRot.y;
            camRot.x = Mathf.Clamp(camRot.x, -verticalClamp, verticalClamp);
            cam.transform.localRotation = Quaternion.Euler(camRot);
        }
    }

    void OnSprint(InputValue value)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateFOV(value.isPressed));
    }

    IEnumerator AnimateFOV(bool sprinting)
    {
        float finalFOV = sprinting ? sprintFOV : originalFOV;
        yield return Tweens.LerpValue(1 / FOVChangeRate, t =>
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, finalFOV, FOVChangeRate * Time.deltaTime));
    }
}
