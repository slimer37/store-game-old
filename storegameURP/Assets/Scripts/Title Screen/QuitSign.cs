using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuitSign : MonoBehaviour
{
    [SerializeField] string activatedState;
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;

    Animator anim;

    void Awake() => anim = GetComponent<Animator>();

    public void OnSelected() => StartCoroutine(Quit());

    IEnumerator Quit()
    {
        GetComponent<TitleElement>().enabled = false;

        anim.Play(activatedState);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        fadePanel.gameObject.SetActive(true);
        yield return Tweens.CrossFadeImage(fadePanel, 1, fadeDuration);

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}