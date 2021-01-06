using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    private Controls controls;
    protected Controls.MenuActions MenuActions { get; private set; }

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Enable();
        MenuActions = controls.Menu;
        MenuActions.Exit.performed += _ => Open(false);
    }

    public virtual void Open(bool value) => gameObject.SetActive(value);
}
