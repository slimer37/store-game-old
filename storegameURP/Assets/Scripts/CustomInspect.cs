using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomInspect : MonoBehaviour
{
    [SerializeField] private float inspectFadeDuration;
    [SerializeField] private CanvasGroup textGroup;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Image image;

    [ContextMenu("Reset Alphas")]
    void ResetAlphas()
    {
        textGroup.alpha = 0;
        Color imageColor = image.color;
        imageColor.a = 0;
        image.color = imageColor;
    }

    public void ShowCustomText(string header, string body, Color headerColor, Color bodyColor)
    {
        headerText.text = header;
        headerText.color = headerColor;
        bodyText.text = body;
        bodyText.color = bodyColor;
        StartCoroutine(FadeText(true));
    }

    public void ShowSprite(Sprite sprite, Vector2? preferredDimensions)
    {
        if (preferredDimensions != null)
        { image.rectTransform.sizeDelta = (Vector2)preferredDimensions; }
        image.sprite = sprite;
        StartCoroutine(FadeImage(true));
    }

    public bool Hide()
    {
        if (textGroup.alpha == 1 || image.color.a == 1)
        {
            StopAllCoroutines();
            StartCoroutine(FadeText(false));
            StartCoroutine(FadeImage(false));
            return true;
        }
        return false;
    }

    IEnumerator FadeText(bool value)
    {
        float from = textGroup.alpha;
        float to = value ? 1 : 0;
        for (float t = 0; t < 1; t += Time.deltaTime / inspectFadeDuration)
        {
            textGroup.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }
        textGroup.alpha = to;
    }

    IEnumerator FadeImage(bool value)
    {
        float to = value ? 1 : 0;
        image.CrossFadeAlpha(to, inspectFadeDuration, false);
        yield return new WaitForSeconds(inspectFadeDuration);
    }
}
