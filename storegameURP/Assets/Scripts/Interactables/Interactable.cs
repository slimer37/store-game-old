using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected bool interactable = true;
    protected virtual CursorIcon.Icon HoverIcon { get; set; } = CursorIcon.Icon.Invalid;
    protected virtual string Tooltip { get; set; } = "";

    protected virtual void OnValidate()
    {
        if (!CompareTag("Interactable"))
        {
            tag = "Interactable";
            Debug.LogWarning($"Tagged '{name}' as 'Interactable.'");
        }
    }

    public void Hover() => CursorIcon.ShowIcon(interactable ? HoverIcon : CursorIcon.Icon.Invalid, Tooltip);

    public void OnInteract()
    {
        if (interactable)
        { Interact(); }
    }

    public abstract void Interact();

    // Optional:
    public virtual void HoverExit() { }

    public void OnSecondaryInteract()
    {
        if (interactable)
        { SecondaryInteract(); }
    }
    public virtual void SecondaryInteract() { }
}
