using System.Collections;
using UnityEngine;
using TMPro;

public class DayScreen : MonoBehaviour
{
    [SerializeField] float fadeDuration;
    [SerializeField] float onScreenDuration;
    [SerializeField] CanvasGroup group;
    [SerializeField] TextMeshProUGUI dayNum;
    [SerializeField] TextMeshProUGUI levelName;

    static DayScreen current;
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
