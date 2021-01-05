using System.Collections;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    private CanvasGroup group;

    void Awake() => group = menuObject.GetComponent<CanvasGroup>();

    public void StartCredits() => StartCoroutine(OpenMenu());

    IEnumerator OpenMenu()
    {
        yield return null;
        TitleScreen.Enable(false);
        yield return Fade(0, 1);
    }

    void OnSelect()
    {
        if (group.alpha == 1)
        {
            StopAllCoroutines();
            StartCoroutine(CloseMenu());
        }
    }

    IEnumerator CloseMenu()
    {
        TitleScreen.Enable(true);
        yield return Fade(1, 0);
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