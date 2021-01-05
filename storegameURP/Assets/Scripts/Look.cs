using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    [SerializeField] private float verticalClamp;
    [SerializeField] private float sensitivity;

    private Camera cam;
    private Vector2 inputRot;
    private Vector2 camRot;

    void Awake() => cam = GetComponent<PlayerInput>().camera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camRot = cam.transform.localEulerAngles;
    }

    void OnLook(InputValue value) => inputRot = value.Get<Vector2>() * sensitivity * Time.deltaTime;

    void Update()
    {
        if (!MenuManager.MenuOpen)
        {
            transform.eulerAngles += inputRot.x * Vector3.up;

            camRot.x -= inputRot.y;
            camRot.x = Mathf.Clamp(camRot.x, -verticalClamp, verticalClamp);
            cam.transform.localRotation = Quaternion.Euler(camRot);
        }
    }
}
