using UnityEngine;

public class PauseMenu : Menu
{
    protected override void Awake()
    {
        base.Awake();
        MenuActions.Pause.performed += _ => Open(true);
    }

    public override void Open(bool value)
    {
        transform.GetChild(0).gameObject.SetActive(value);
        Time.timeScale = value ? 0 : 1;
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
