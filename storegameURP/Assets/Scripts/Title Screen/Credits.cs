using System.Collections;
using UnityEngine;

public class Credits : Menu
{
    Vector2 originalPivot;
    Animator creditsAnim;
    CanvasGroup group;

    protected override void Awake()
    {
        originalPivot = transform.GetChild(0).GetComponent<RectTransform>().pivot;
        creditsAnim = GetComponent<Animator>();
        group = GetComponent<CanvasGroup>();

        base.Awake();
        MenuActions.Select.performed += Exit;
    }

    public override void OnAttemptOpen(bool value) => OnOpen(value);

    protected override void OnOpen(bool value)
    {
        if (value)
        {
            StopAllCoroutines();
            StartCoroutine(ScrollCredits());
        }
        else if (group.alpha == 1)
        {
            StopAllCoroutines();
            StartCoroutine(StopCredits());
        }
    }

    IEnumerator ScrollCredits()
    {
        yield return null;
        TitleScreen.Enable(false);

        transform.GetChild(0).GetComponent<RectTransform>().pivot = originalPivot;
        yield return Tweens.CrossFadeGroup(group, 1, 1);

        AnimatorStateInfo stateInfo = creditsAnim.GetCurrentAnimatorStateInfo(0);
        creditsAnim.Play(stateInfo.fullPathHash, 0, 0);
        creditsAnim.enabled = true;

        yield return new WaitForSeconds(stateInfo.length);
        yield return StopCredits();
    }

    IEnumerator StopCredits()
    {
        TitleScreen.Enable(true);
        yield return Tweens.CrossFadeGroup(group, 0, 1);
        creditsAnim.StopPlayback();
        creditsAnim.enabled = false;
    }
}