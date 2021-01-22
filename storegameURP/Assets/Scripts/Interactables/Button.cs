using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    [SerializeField] private CursorIcon.Icon hoverIcon;
    protected override CursorIcon.Icon HoverIcon => hoverIcon;

    public UnityEvent OnPress;

    public override void Interact() => OnPress?.Invoke();
}