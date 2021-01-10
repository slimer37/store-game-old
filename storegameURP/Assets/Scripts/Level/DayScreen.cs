using System.Collections;
using UnityEngine;
using TMPro;

public class DayScreen : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private float onScreenDuration;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TextMeshProUGUI dayNum;
    [SerializeField] private TextMeshProUGUI levelName;

    private static DayScreen current;
    public static float Duration => current.fadeDuration * 2 + current.onScreenDuration;
    void Awake() => current = this;

    void Start() => Level.Current.OnStoreOpen.AddListener(() => StartCoroutine(ShowDayScreen()));
    void OnDisable() => Level.Current.OnStoreOpen.RemoveListener(() => StartCoroutine(ShowDayScreen()));

    IEnumerator ShowDayScreen()
    {
        dayNum.CrossFadeAlpha(0, 0, false);
        levelName.CrossFadeAlpha(0, 0, false);
        yield return Tweens.CrossFadeGroup(group, 1, fadeDuration);
        dayNum.CrossFadeAlpha(1, fadeDuration, false);
        levelName.CrossFadeAlpha(1, fadeDuration, false);
        yield return new WaitForSeconds(onScreenDuration);
        yield return Tweens.CrossFadeGroup(group, 0, fadeDuration);
    }
}
