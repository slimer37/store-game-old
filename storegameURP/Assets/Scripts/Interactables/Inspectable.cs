using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] private string header;
    [SerializeField, TextArea(8, 100)] private string body;
    [SerializeField] private Color headerColor = Color.black;
    [SerializeField] private Color bodyColor = Color.black;

    protected override Hover.Icon HoverIcon { get => Hover.Icon.View; }

    public override void Interact() => MenuManager.Current.CustomInspect.ShowCustomText(header, body, headerColor, bodyColor);
}
