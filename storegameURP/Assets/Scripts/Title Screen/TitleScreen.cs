using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private float front;
    [SerializeField] private float animSpeed;
    [SerializeField] private float hoverHeight;

    private Camera cam;
    private Vector3 firstFront;
    private TitleElement hoveredObj;

    private static TitleScreen current;

    public static float AnimSpeed => current.animSpeed;
    public static float HoverHeight => current.hoverHeight;
    public static Vector3 Front => current.firstFront;
    public static Quaternion ElementRot => Quaternion.LookRotation(-current.cam.transform.forward);

    private Ray CamRay => cam.ScreenPointToRay(Mouse.current.position.ReadValue());

    void Awake()
    {
        cam = GetComponent<PlayerInput>().camera;
        firstFront = cam.transform.position + cam.transform.forward * front;
        current = this;
    }

    void OnSelect()
    {
        if (hoveredObj && hoveredObj.Focused)
        {
            if (Physics.Raycast(CamRay, out RaycastHit hit) && hit.transform == hoveredObj.transform)
            { hoveredObj.OnChosen.Invoke(); }
            else
            {
                hoveredObj.Select(false);
                hoveredObj = null;
            }
        }
        else if (hoveredObj)
        { hoveredObj.Select(true); }
    }

    void Update()
    {
        if (hoveredObj && hoveredObj.Focused) { return; }

        if (Physics.Raycast(CamRay, out RaycastHit hit) && hit.transform.gameObject.layer == 3)
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
