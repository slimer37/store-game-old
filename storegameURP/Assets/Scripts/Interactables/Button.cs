using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    [SerializeField] private Hover.Icon hoverIcon;
    protected override Hover.Icon HoverIcon => hoverIcon;

    public UnityEvent OnPress;

    public override void Interact() => OnPress?.Invoke();
}