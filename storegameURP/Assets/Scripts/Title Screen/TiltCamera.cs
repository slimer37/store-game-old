using UnityEngine;
using UnityEngine.InputSystem;

public class TiltCamera : MonoBehaviour
{
    [SerializeField] Vector2 maxTilt;

    Quaternion originalRot;

    void Awake() => originalRot = transform.rotation;

    void Update()
    {
        var mouseAbsPos = Mouse.current.position.ReadValue();
        var mouseRelPos = new Vector2(mouseAbsPos.x / Screen.width, mouseAbsPos.y / Screen.height);
        mouseRelPos = (mouseRelPos - Vector2.one * 0.5f) * 2;

        Vector2 adjustedPos = new Vector2(Mathf.Abs(mouseRelPos.x), Mathf.Abs(mouseRelPos.y));

        var rotX = Mathf.Clamp(-mouseRelPos.y * maxTilt.x, -maxTilt.x, maxTilt.x);
        var rotY = Mathf.Clamp(mouseRelPos.x * maxTilt.y, -maxTilt.y, maxTilt.y);
        var newRot = originalRot.eulerAngles + new Vector3(rotX, rotY, 0);
        transform.rotation = Quaternion.Euler(newRot);
    }
}
