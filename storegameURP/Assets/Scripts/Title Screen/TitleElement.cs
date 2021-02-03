using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TitleElement : MonoBehaviour
{
    public bool Focused { get; private set; } = false;

    public UnityEvent OnChosen;

    [SerializeField] private string openState;
    [SerializeField] private string closeState;
    [SerializeField] private bool callbackOnSelect;
    [SerializeField] private float overrideHoverDist;

    private Vector3 firstPos;
    private Quaternion firstRot;

    private Animator anim;
    private bool animating = false;
    private Coroutine hoverRoutine = null;

    private float hoverHeight;

    void Awake()
    {
        if (GetComponent<Animator>())
        { anim = GetComponent<Animator>(); }        
        firstPos = transform.position;
        firstRot = transform.rotation;
    }

    void Start() => hoverHeight = overrideHoverDist == 0 ? TitleScreen.HoverHeight : overrideHoverDist;

    public void Hover(bool value)
    {
        if (hoverRoutine != null)
        { StopCoroutine(hoverRoutine); }
        hoverRoutine = StartCoroutine(Raise(value));
    }

    public void Select(bool value)
    {
        if (!animating)
        {
            StopCoroutine(hoverRoutine);

            if (value && !Focused)
            {
                if (callbackOnSelect)
                { OnChosen.Invoke(); }
                else
                { StartCoroutine(FocusObject()); }
            }
            else if (!callbackOnSelect && !value && Focused)
            { StartCoroutine(UnfocusObject()); }
        }
    }

    IEnumerator Raise(bool raise, bool force = false)
    {
        if (force || !Focused && !animating)
        {
            Vector3 end = firstPos + Vector3.up * hoverHeight * (raise ? 1 : 0);
            yield return Tweens.LerpLocation(transform, end, 1 / TitleScreen.AnimSpeed);
        }
    }

    IEnumerator FocusObject()
    {
        StopCoroutine(UnfocusObject());
        animating = true;
        Focused = true;

        yield return Tweens.LerpTransform(transform, TitleScreen.Front, TitleScreen.ElementRot, 1 / TitleScreen.AnimSpeed);

        if (anim)
        { anim.Play(openState); }
        animating = false;
    }

    IEnumerator UnfocusObject()
    {
        StopCoroutine(FocusObject());
        animating = true;
        Focused = false;

        if (anim)
        {
            anim.Play(closeState);
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);
        }

        yield return Tweens.LerpTransform(transform, firstPos + (anim ? Vector3.up * hoverHeight : Vector3.zero), firstRot, 1 / TitleScreen.AnimSpeed);
        yield return Raise(false, anim);

        animating = false;
    }
}
