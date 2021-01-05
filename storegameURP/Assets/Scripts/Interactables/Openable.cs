using System.Collections;
using UnityEngine;

public class Openable : Interactable
{
    protected override CursorIcon.Icon HoverIcon
    {
        get => animating ? CursorIcon.Icon.Invalid :
            open ? CursorIcon.Icon.Push : CursorIcon.Icon.Pull;
    }

    [SerializeField] private string openState;
    [SerializeField] private string closeState;

    private Animator anim;
    private bool open = false;
    private bool animating = false;

    void Awake() => anim = GetComponent<Animator>();

    public override void Interact()
    {
        if (animating) return;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        animating = true;

        anim.Play(open ? closeState : openState);
        yield return null;

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);

        open = !open;
        animating = false;
    }
}
