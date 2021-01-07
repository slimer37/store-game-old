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

    void Start() => LevelProgression.CurrentLevel.OnStoreOpen += () => StartCoroutine(ShowDayScreen());
    void OnDisable() => LevelProgression.CurrentLevel.OnStoreOpen -= () => StartCoroutine(ShowDayScreen());

    IEnumerator ShowDayScreen()
    {
        dayNum.CrossFadeAlpha(0, 0, false);
        levelName.CrossFadeAlpha(0, 0, false);
        yield return Tweens.CrossFadeGroup(group, 1, fadeDuration);
        dayNum.CrossFadeAlpha(1, fadeDuration, false);
        levelName.CrossFadeAlpha(1, fadeDuration, false);
        yield return new WaitForSeconds(onScreenDuration - fadeDuration);
        yield return Tweens.CrossFadeGroup(group, 0, fadeDuration);
    }
}
