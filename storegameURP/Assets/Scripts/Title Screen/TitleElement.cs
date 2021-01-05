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

    void OnValidate()
    {
        if (!CompareTag("Interactable"))
        { Debug.LogWarning($"'{name}' needs to be tagged 'Interactable.'"); }
    }

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
            Vector3 start = transform.position;
            Vector3 end = firstPos + Vector3.up * hoverHeight * (raise ? 1 : 0);

            for (float t = 0; t < 1; t += TitleScreen.AnimSpeed * Time.deltaTime)
            {
                transform.position = Vector3.Lerp(start, end, t);
                yield return null;
            }
        }
    }

    IEnumerator FocusObject()
    {
        StopCoroutine(UnfocusObject());
        animating = true;
        Focused = true;

        yield return AnimateTransform(TitleScreen.Front, TitleScreen.ElementRot);
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

        yield return AnimateTransform(firstPos + (anim ? Vector3.up * hoverHeight : Vector3.zero), firstRot);
        yield return Raise(false, anim);

        animating = false;
    }

    IEnumerator AnimateTransform(Vector3 end, Quaternion endRot)
    {
        Vector3 start = transform.position;
        Quaternion startRot = transform.rotation;
        for (float t = 0; t < 1; t += TitleScreen.AnimSpeed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(start, end, t);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }
        transform.position = end;
        transform.rotation = endRot;
    }
}
