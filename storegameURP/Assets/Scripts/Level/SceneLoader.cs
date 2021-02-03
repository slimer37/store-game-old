using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Current { get; private set; }

    [SerializeField] private int baseSceneIndex;
    [SerializeField] private int titleScreenIndex;
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

    IEnumerator LoadAsync(int sceneIndex, float fromPercent = 0, float toPercent = 1, LoadSceneMode mode = LoadSceneMode.Single)
    {
        DontDestroyOnLoad(gameObject);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, mode);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = fromPercent + Mathf.Clamp01(operation.progress / 0.9f) * (toPercent - fromPercent);
            loadingBar.value = progress;
            progressText.text = Mathf.RoundToInt(progress * 100 * toPercent) + "%";

            var captionIndex = Mathf.RoundToInt(progress * progressCaptions.Length);
            progressCaption.text = progressCaptions[Mathf.Clamp(captionIndex, 0, progressCaptions.Length - 1)];

            yield return null;
        }

        if (sceneIndex != titleScreenIndex && sceneIndex != baseSceneIndex)
        { yield return LoadAsync(baseSceneIndex, 0.5f, 1, LoadSceneMode.Additive); }

        Destroy(gameObject);
    }
}