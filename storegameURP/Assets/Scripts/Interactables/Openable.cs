using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Openable : Interactable
{
    protected override CursorIcon.Icon HoverIcon
    {
        get => animating ? CursorIcon.Icon.Invalid :
            open ? CursorIcon.Icon.Push : CursorIcon.Icon.Pull;
    }

    [SerializeField] private EntranceTrigger trigger;
    [SerializeField] private string openState;
    [SerializeField] private string closeState;

    private NavMeshObstacle obstacle;
    private Animator anim;
    private bool open = false;
    private bool animating = false;

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (GetComponent<NavMeshObstacle>())
        { obstacle = GetComponent<NavMeshObstacle>(); }

        if (trigger)
        {
            trigger.OnCustomerTrigger += trigger =>
            {
                // Open door and prevent player from closing while customer is in trigger.
                if (!animating)
                {
                    interactable = !trigger;
                    if (trigger && !open)
                    { StartCoroutine(Animate()); }
                }
            };
        }
    }

    public override void Interact()
    {
        if (animating) return;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        if (obstacle && open)
        { obstacle.enabled = false; }

        animating = true;

        anim.Play(open ? closeState : openState);
        yield return null;

        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);

        open = !open;
        animating = false;

        if (obstacle && open)
        { obstacle.enabled = true; }
    }
}
