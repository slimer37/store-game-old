using UnityEngine;
using System.Collections;
using TMPro;

public class CustomInspect : Menu
{
    [SerializeField] private float inspectFadeDuration;
    [SerializeField] private CanvasGroup textGroup;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI bodyText;

    protected override bool Open => textGroup.alpha == 1;

    protected override void Awake()
    {
        base.Awake();
        MenuActions.Exit.performed -= Exit;
        MenuActions.Exit.performed += _ => Hide();
    }

    public void ShowCustomText(string header, string body, Color headerColor, Color bodyColor)
    {
        if (Open || MenuManager.Current.MenuOpen) return;

        headerText.text = header;
        headerText.color = headerColor;
        bodyText.text = body;
        bodyText.color = bodyColor;
        StartCoroutine(FadeText(true));

        // Immediately bar other menus from opening when fade starts.
        MenuManager.Current.OpenMenu(true);
    }

    IEnumerator FadeText(bool value)
    {
        yield return Tweens.CrossFadeGroup(textGroup, value ? 1 : 0, inspectFadeDuration);

        // Allow other menus when completely faded out.
        if (!value)
        { MenuManager.Current.OpenMenu(false); }
    }

    public void Hide()
    {
        if (!Open || !MenuManager.Current.MenuOpen) return;

        StopAllCoroutines();
        StartCoroutine(FadeText(false));
    }
}
