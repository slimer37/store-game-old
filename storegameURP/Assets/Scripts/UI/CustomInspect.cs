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
        StartCoroutine(Tweens.CrossFadeGroup(textGroup, 1, inspectFadeDuration));
    }

    public void ShowSprite(Sprite sprite, Vector2? preferredDimensions)
    {
        if (preferredDimensions != null)
        { image.rectTransform.sizeDelta = (Vector2)preferredDimensions; }
        image.sprite = sprite;
        StartCoroutine(Tweens.CrossFadeImage(image, 1, inspectFadeDuration));
    }

    public bool Hide()
    {
        if (textGroup.alpha == 1 || image.color.a == 1)
        {
            StopAllCoroutines();
            StartCoroutine(Tweens.CrossFadeGroup(textGroup, 0, inspectFadeDuration));
            StartCoroutine(Tweens.CrossFadeImage(image, 0, inspectFadeDuration));
            return true;
        }
        return false;
    }
}
