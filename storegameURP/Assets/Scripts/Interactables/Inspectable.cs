using UnityEngine;

public class Inspectable : Interactable
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string header;
    [SerializeField, TextArea(8, 100)] private string body;
    [SerializeField] private Color headerColor = Color.black;
    [SerializeField] private Color bodyColor = Color.black;

    protected override CursorIcon.Icon HoverIcon { get => CursorIcon.Icon.View; }

    public override void Interact()
    {
        if (sprite)
        { MenuManager.Current.Inspect(sprite); }
        else
        { MenuManager.Current.InspectCustomText(header, body, headerColor, bodyColor); }
    }

    public override void SecondaryInteract()
    {
        base.SecondaryInteract();
    }
}
