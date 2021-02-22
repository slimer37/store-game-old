using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] string header;
    [SerializeField, TextArea(8, 100)] string body;
    [SerializeField] Color headerColor = Color.black;
    [SerializeField] Color bodyColor = Color.black;

    protected override Hover.Icon HoverIcon { get => Hover.Icon.View; }

    public override void Interact() => MenuManager.Current.CustomInspect.ShowCustomText(header, body, headerColor, bodyColor);
}
