using UnityEngine;

public class OpenSign : Interactable
{
    protected override CursorIcon.Icon HoverIcon { get => CursorIcon.Icon.Access; }

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
