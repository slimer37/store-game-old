using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyIndicator : MonoBehaviour
{
    [SerializeField] private float downForDuration;
    [SerializeField] private TextMeshProUGUI amountText;

    private RectTransform rectTransform;
    private bool lowered = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Level.Current.OnProfit += () =>
        {
            amountText.text = Level.Current.Money.ToString("c");
            if (lowered)
            {
                StopAllCoroutines();
                StartCoroutine(DelayedRaise());
            }
            else
            { StartCoroutine(Lower(true)); }
        };
    }

    IEnumerator Lower(bool value)
    {
        lowered = value;
        Vector2 newPivot = new Vector2(rectTransform.pivot.x, value ? 1 : 0);
        yield return Tweens.LerpValue(1.0f, t =>
        {
            rectTransform.pivot = Vector2.Lerp(rectTransform.pivot, newPivot, t);
        });

        if (value)
        { StartCoroutine(DelayedRaise()); }
    }

    IEnumerator DelayedRaise()
    {
        yield return new WaitForSeconds(downForDuration);
        yield return Lower(false);
    }
}
