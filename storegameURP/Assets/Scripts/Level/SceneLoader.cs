using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // Thanks, Brackeys!
    public static SceneLoader Current { get; private set; }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI progressCaption;
    [SerializeField] private string[] progressCaptions;

    void Awake() => Current = this;

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";

            var captionIndex = Mathf.RoundToInt(progress * progressCaptions.Length);
            progressCaption.text = progressCaptions[Mathf.Clamp(captionIndex, 0, progressCaptions.Length - 1)];

            yield return null;
        }
    }
}