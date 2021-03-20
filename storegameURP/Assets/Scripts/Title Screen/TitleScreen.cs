using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] float front;
    [SerializeField] float animSpeed;
    [SerializeField] float hoverHeight;
    [SerializeField] LayerMask elementMask;

    Camera cam;
    Vector3 firstFront;
    TitleElement hoveredObj;

    static TitleScreen current;

    public static float AnimSpeed => current.animSpeed;
    public static float HoverHeight => current.hoverHeight;
    public static Vector3 Front => current.firstFront;
    public static Quaternion ElementRot => Quaternion.LookRotation(-current.cam.transform.forward);

    Ray CamRay => cam.ScreenPointToRay(Mouse.current.position.ReadValue());

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        firstFront = cam.transform.position + cam.transform.forward * front;
        current = this;
    }

    void OnSelect()
    {
        if (!hoveredObj) return;

        var rayHit = Physics.Raycast(CamRay, out RaycastHit hit, Mathf.Infinity, elementMask) && hit.transform == hoveredObj.transform;
        hoveredObj.Select(rayHit);
        if (!rayHit)
        { hoveredObj = null; }
    }

    void FixedUpdate()
    {
        if (hoveredObj && hoveredObj.IsFocused) { return; }

        if (Physics.Raycast(CamRay, out RaycastHit hit, Mathf.Infinity, elementMask))
        {
            if (!hoveredObj || hoveredObj.transform != hit.transform)
            {
                if (hoveredObj)
                { hoveredObj.Hover(false); }
                hoveredObj = hit.transform.GetComponent<TitleElement>();
                hoveredObj.Hover(true);
            }
        }
        else
        {
            if (hoveredObj)
            { hoveredObj.Hover(false); }
            hoveredObj = null;
        }
    }

    public static void Enable(bool value)
    {
        if (current.hoveredObj)
        {
            current.hoveredObj.Hover(false);
            current.hoveredObj = null;
        }
        current.enabled = value;
    }
}
