using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuitSign : MonoBehaviour
{
    [SerializeField] private string activatedState;
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration;

    private Animator anim;

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