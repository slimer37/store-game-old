using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : Menu
{
    public UnityEvent OnPause;
    public UnityEvent OnResume;

    protected override void Awake()
    {
        base.Awake();
        MenuActions.Pause.performed += _ => Open(!transform.GetChild(0).gameObject.activeSelf);
    }

    public override void Open(bool value)
    {
        (value ? OnPause : OnResume).Invoke();
        transform.GetChild(0).gameObject.SetActive(value);
        Time.timeScale = value ? 0 : 1;
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
