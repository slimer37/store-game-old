using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private CustomInspect customInspect;

    private Controls controls;
    public static MenuManager Current { get; private set; }
    public static bool MenuOpen { get; private set; } = false;

    void Awake()
    {
        Current = this;
        controls = new Controls();
        controls.Menu.Exit.performed += _ => Close();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    public void Inspect(Sprite sprite, Vector2? preferredDimensions = null)
    {
        if (!MenuOpen)
        {
            customInspect.ShowSprite(sprite, preferredDimensions);
            MenuOpen = true;
        }
    }

    public void InspectCustomText(string header, string body,
        Color headerColor, Color bodyColor)
    {
        if (!MenuOpen)
        {
            customInspect.ShowCustomText(header, body, headerColor, bodyColor);
            MenuOpen = true;
        }
    }

    void Close()
    {
        if (customInspect.Hide())
        {
            MenuOpen = false;
        }
    }
}
