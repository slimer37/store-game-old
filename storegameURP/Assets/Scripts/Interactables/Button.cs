using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    [field: SerializeField] protected override CursorIcon.Icon HoverIcon { get; set; }

    public UnityEvent OnPress;

    public override void Interact() => OnPress?.Invoke();
}