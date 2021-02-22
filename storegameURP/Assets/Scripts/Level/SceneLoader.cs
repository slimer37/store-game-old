using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Current { get; private set; }

    [Header("Scenes")]
    [SerializeField] List<int> loadAlone;
    [SerializeField] int baseSceneIndex;

    [Header("Loading Screen")]
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] TextMeshProUGUI progressCaption;
    [SerializeField] string[] progressCaptions;

    void Awake() => Current = this;

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(DynamicLoadAsync(sceneIndex));
    }

    IEnumerator DynamicLoadAsync(int sceneIndex)
    {
        DontDestroyOnLoad(gameObject);

        loadingScreen.SetActive(true);
        loadingBar.value = 0;

        if (loadAlone.Contains(sceneIndex))
        { yield return LoadAsync(sceneIndex, 1); }
        else
        {
            yield return LoadAsync(sceneIndex, 0.5f);
            yield return LoadAsync(baseSceneIndex, 1, LoadSceneMode.Additive);
        }

        Destroy(gameObject);
    }

    IEnumerator LoadAsync(int sceneIndex, float toPercent, LoadSceneMode mode = LoadSceneMode.Single)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, mode);
        float fromPercent = loadingBar.value;

        while (!operation.isDone)
        {
            float progress = fromPercent + Mathf.Clamp01(operation.progress / 0.9f) * (toPercent - fromPercent);
            loadingBar.value = progress;
            progressText.text = Mathf.RoundToInt(progress * 100 * toPercent) + "%";

            var captionIndex = Mathf.RoundToInt(progress * progressCaptions.Length);
            progressCaption.text = progressCaptions[Mathf.Clamp(captionIndex, 0, progressCaptions.Length - 1)];

            yield return null;
        }
    }
}