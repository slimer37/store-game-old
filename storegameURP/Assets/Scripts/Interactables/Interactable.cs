using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool interactable = true;
    protected virtual Hover.Icon HoverIcon { get; set; } = global::Hover.Icon.Invalid;
    protected virtual string Tooltip { get; set; } = "";

    protected virtual void OnValidate()
    {
        if (!CompareTag("Interactable"))
        {
            tag = "Interactable";
            Debug.LogWarning($"Tagged '{name}' as 'Interactable.'");
        }
    }

    protected virtual void OnHover() => Hover.ShowIcon(interactable ? HoverIcon : Hover.Icon.Invalid, Tooltip);

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
