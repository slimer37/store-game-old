using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    Controls controls;
    protected virtual bool Open { get; private set; } = false;
    protected Controls.MenuActions MenuActions { get; private set; }

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Enable();
        MenuActions = controls.Menu;
        MenuActions.Exit.performed += Exit;
    }

    protected void Exit(UnityEngine.InputSystem.InputAction.CallbackContext _) => OnAttemptOpen(false);

    void OnDestroy() => controls.Disable();

    public virtual void OnAttemptOpen(bool value)
    {
        if (Open != value && (!MenuManager.Current || MenuManager.Current.MenuOpen != value))
        {
            if (MenuManager.Current)
            { MenuManager.Current.OpenMenu(value); }
            Open = value;
            OnOpen(value);
        }
    }

    protected virtual void OnOpen(bool value) => gameObject.SetActive(value);
}
