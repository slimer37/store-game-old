using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [field: SerializeField] public CustomInspect CustomInspect { get; private set; }

    public static MenuManager Current { get; private set; }
    public bool MenuOpen { get; private set; } = false;

    void Awake() => Current = this;

    public void OpenMenu(bool value)
    {
        if (MenuOpen == value)
        { throw new System.InvalidOperationException(value ? "Menu cannot open when another is already open or hasn't explicitly closed."
            : "Menu cannot close when none are open and the menu hasn't explicitly opened."); }
        MenuOpen = value;
    }
}
