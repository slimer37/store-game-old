using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TitleElement : MonoBehaviour
{
    public bool IsFocused { get; private set; } = false;

    public UnityEvent OnChosen;

    [SerializeField] string openState;
    [SerializeField] string closeState;
    [SerializeField] bool callbackOnSelect;
    [SerializeField] float overrideHoverDist;

    Vector3 firstPos;
    Quaternion firstRot;

    Animator anim;
    bool animating = false;
    Coroutine raiseRoutine = null;

    float hoverHeight;

    void Awake()
    {
        TryGetComponent(out anim);
        firstPos = transform.position;
        firstRot = transform.rotation;
    }

    void Start() => hoverHeight = overrideHoverDist == 0 ? TitleScreen.HoverHeight : overrideHoverDist;

    public void Hover(bool value)
    {
        if (raiseRoutine != null)
        { StopCoroutine(raiseRoutine); }
        raiseRoutine = StartCoroutine(Raise(value));
    }

    public void Select(bool rayHit)
    {
        if (rayHit && IsFocused)
        {
            OnChosen.Invoke();
            return;
        }

        StopAllCoroutines();
        if (rayHit && !IsFocused)
        {
            if (callbackOnSelect)
            { OnChosen.Invoke(); }
            else
            { StartCoroutine(FocusObject(true)); }
        }
        else if (!callbackOnSelect && !rayHit && IsFocused)
        { StartCoroutine(FocusObject(false)); }
    }

    IEnumerator Raise(bool raise, bool force = false)
    {
        if (force || !IsFocused && !animating)
        {
            Vector3 end = firstPos + Vector3.up * hoverHeight * (raise ? 1 : 0);
            yield return Tweens.LerpLocation(transform, end, 1 / TitleScreen.AnimSpeed);
        }
    }

    IEnumerator FocusObject(bool value)
    {
        animating = true;
        IsFocused = value;

        if (!value && anim)
        { anim.CrossFadeInFixedTime(closeState, 0.25f); }

        var targetPos = value ? TitleScreen.Front : firstPos + (anim ? Vector3.up * hoverHeight : Vector3.zero);
        var targetRot = value ? TitleScreen.ElementRot : firstRot;
        yield return Tweens.LerpTransform(transform, targetPos, targetRot, 1 / TitleScreen.AnimSpeed);

        if (!value)
        { yield return Raise(false, anim); }

        if (value && anim)
        { anim.CrossFadeInFixedTime(openState, 0.25f); }

        animating = false;
    }
}
