using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TitleElement : MonoBehaviour
{
    public bool Focused { get; private set; } = false;

    public UnityEvent OnChosen;

    [SerializeField] string openState;
    [SerializeField] string closeState;
    [SerializeField] bool callbackOnSelect;
    [SerializeField] float overrideHoverDist;

    Vector3 firstPos;
    Quaternion firstRot;

    Renderer[] renderers;
    Shader[][] originalShaders;
    Shader hoverShader;
    Animator anim;
    bool animating = false;
    Coroutine hoverRoutine = null;

    float hoverHeight;

    void Awake()
    {
        TryGetComponent(out anim);
        firstPos = transform.position;
        firstRot = transform.rotation;

        var allRenderers = GetComponentsInChildren<Renderer>();
        renderers = new Renderer[allRenderers.Length];
        originalShaders = new Shader[allRenderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = allRenderers[i];
            if (renderers[i].GetComponent<TMPro.TextMeshPro>()) continue;
            originalShaders[i] = new Shader[renderers[i].materials.Length];
            for (int j = 0; j < renderers[i].materials.Length; j++)
            { originalShaders[i][j] = renderers[i].materials[j].shader; }
        }
        hoverShader = Shader.Find("Shader Graphs/Shine");
    }

    void Start() => hoverHeight = overrideHoverDist == 0 ? TitleScreen.HoverHeight : overrideHoverDist;

    public void Hover(bool value)
    {
        if (hoverRoutine != null)
        { StopCoroutine(hoverRoutine); }
        hoverRoutine = StartCoroutine(Raise(value));

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].GetComponent<TMPro.TextMeshPro>()) continue;
            for (int j = 0; j < renderers[i].materials.Length; j++)
            { renderers[i].materials[j].shader = value ? hoverShader : originalShaders[i][j]; }
        }
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
