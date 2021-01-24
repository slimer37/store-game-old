using UnityEngine;

public class OpenSign : Interactable
{
    protected override Hover.Icon HoverIcon { get => Hover.Icon.Access; }

    [SerializeField] private string openState;

    private Animator anim;
    private bool open = false;

    void Awake() => anim = GetComponent<Animator>();

    public override void Interact()
    {
        if (!open)
        {
            anim.Play(openState);
            interactable = false;
            Level.Current.OpenStore();
            open = true;
        }
    }
}
