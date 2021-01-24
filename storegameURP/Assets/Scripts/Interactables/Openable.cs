using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Openable : Interactable
{
    protected override Hover.Icon HoverIcon
    {
        get => animating ? Hover.Icon.Invalid :
            Open ? Hover.Icon.Push : Hover.Icon.Pull;
    }

    [SerializeField] private string openState;
    [SerializeField] private string closeState;

    private NavMeshObstacle obstacle;
    private Animator anim;
    private bool animating = false;

    public bool Open { get; private set; } = false;

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (GetComponent<NavMeshObstacle>())
        { obstacle = GetComponent<NavMeshObstacle>(); }
    }

    public override void Interact()
    {
        if (animating) return;
        StartCoroutine(Animate());
    }

    public void SetInteractable(bool value) => interactable = value;

    IEnumerator Animate()
    {
        if (obstacle && Open)
        { obstacle.enabled = false; }

        animating = true;

        anim.Play(Open ? closeState : openState);
        yield return null;

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);

        Open = !Open;
        animating = false;

        if (obstacle && Open)
        { obstacle.enabled = true; }
    }
}
