using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] float verticalClamp;
    [SerializeField] float sensitivity;

    [Header("Sprinting")]
    [SerializeField] float sprintFOV;
    [SerializeField] float FOVChangeRate;

    Camera cam;
    Vector2 inputRot;
    Vector2 camRot;
    float originalFOV;

    float targetFOV;
    float t;

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        targetFOV = originalFOV = cam.fieldOfView;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camRot = cam.transform.localEulerAngles;
    }

    void OnLook(InputValue value) => inputRot = value.Get<Vector2>();

    void Update()
    {
        if (MenuManager.Current.MenuOpen) return;

        var delta = inputRot * sensitivity * Time.deltaTime;
        transform.localEulerAngles += delta.x * Vector3.up;

        camRot.x = Mathf.Clamp(camRot.x - delta.y, -verticalClamp, verticalClamp);
        cam.transform.localRotation = Quaternion.Euler(camRot);

        t += Time.deltaTime * FOVChangeRate;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, t);
    }

    void OnSprint(InputValue value)
    {
        targetFOV = value.isPressed ? sprintFOV : originalFOV;
        t = 0;
    }
}
