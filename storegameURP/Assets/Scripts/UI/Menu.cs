using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    private Controls controls;
    protected bool open = false;
    protected Controls.MenuActions MenuActions { get; private set; }

    protected virtual void Awake()
    {
        controls = new Controls();
        controls.Enable();
        MenuActions = controls.Menu;
        MenuActions.Exit.performed += Exit;
    }

    protected void Exit(UnityEngine.InputSystem.InputAction.CallbackContext _) => Open(false);

    void OnDestroy() => controls.Disable();

    public virtual void Open(bool value) => gameObject.SetActive(open = value);
}
