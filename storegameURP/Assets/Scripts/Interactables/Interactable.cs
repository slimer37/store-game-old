using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool interactable = true;
    protected virtual CursorIcon.Icon HoverIcon { get; set; }

    protected virtual void OnValidate()
    {
        if (!CompareTag("Interactable"))
        {
            tag = "Interactable";
            Debug.LogWarning($"Tagged '{name}' as 'Interactable.'");
        }
    }

    public void Hover() => CursorIcon.ShowIcon(HoverIcon);

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