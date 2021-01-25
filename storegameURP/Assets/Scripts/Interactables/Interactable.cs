using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool interactable = true;
    protected virtual Hover.Icon HoverIcon { get; set; } = Hover.Icon.Invalid;
    protected virtual string Tooltip { get; set; } = "";

    protected virtual void OnValidate()
    {
        if (gameObject.layer != 3)
        {
            gameObject.layer = 3;
            Debug.LogWarning($"Set layer of {name} to '{LayerMask.LayerToName(3)}' layer.");
        }
    }

    protected void OnHover() => Hover.Current.ShowIcon(interactable ? HoverIcon : Hover.Icon.Invalid, Tooltip);

    protected void OnInteract()
    {
        if (interactable)
        { Interact(); }
    }

    public abstract void Interact();

    // Optional:
    protected virtual void OnHoverExit() { }

    protected void OnSecondaryInteract()
    {
        if (interactable)
        { SecondaryInteract(); }
    }
    public virtual void SecondaryInteract() { }
}
