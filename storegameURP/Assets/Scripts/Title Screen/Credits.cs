using System.Collections;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject creditsObject;

    private Vector2 originalPivot;
    private Animator creditsAnim;
    private CanvasGroup group;

    void Awake()
    {
        originalPivot = creditsObject.transform.GetChild(0).GetComponent<RectTransform>().pivot;
        creditsAnim = creditsObject.GetComponent<Animator>();
        group = creditsObject.GetComponent<CanvasGroup>();
    }

    public void StartCredits()
    {
        StopAllCoroutines();
        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        yield return null;
        TitleScreen.Enable(false);

        creditsObject.transform.GetChild(0).GetComponent<RectTransform>().pivot = originalPivot;
        yield return Fade(0, 1);

        AnimatorStateInfo stateInfo = creditsAnim.GetCurrentAnimatorStateInfo(0);
        creditsAnim.Play(stateInfo.fullPathHash, 0, 0);
        creditsAnim.enabled = true;

        yield return new WaitForSeconds(stateInfo.length);
        yield return StopCredits();
    }

    void OnSelect()
    {
        if (group.alpha == 1)
        {
            StopAllCoroutines();
            StartCoroutine(StopCredits());
        }
    }

    IEnumerator StopCredits()
    {
        TitleScreen.Enable(true);
        yield return Fade(1, 0);
        creditsAnim.StopPlayback();
        creditsAnim.enabled = false;
    }

    IEnumerator Fade(float start, float end)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            group.alpha = Mathf.Lerp(start, end, i);
            yield return null;
        }
        group.alpha = end;
    }
}