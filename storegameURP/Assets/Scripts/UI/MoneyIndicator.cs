using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyIndicator : MonoBehaviour
{
    [SerializeField] float downForDuration;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] TextMeshProUGUI differenceText;

    RectTransform rectTransform;
    bool lowered = false;

    float moneyRecorded = 0;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Level.Current.OnProfit.AddListener(OnProfit);
    }

    void OnDisable() => Level.Current.OnProfit.RemoveListener(OnProfit);

    void OnProfit()
    {
        amountText.text = Level.Current.Money.ToString("c");
        differenceText.text = $"+{Level.Current.Money - moneyRecorded:c}";
        moneyRecorded = Level.Current.Money;
        StopAllCoroutines();
        if (lowered)
        { StartCoroutine(DelayedRaise()); }
        else
        { StartCoroutine(Lower(true)); }
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
