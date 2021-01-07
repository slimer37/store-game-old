using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] private Image dayPanel;
    [SerializeField] private TextMeshProUGUI levelText;

    public Action OnStoreOpen;

    public static LevelProgression CurrentLevel { get; private set; }
    public bool StoreOpen { get; private set; }

    void Awake() => CurrentLevel = this;

    public void OpenStore()
    {
        StoreOpen = true;
        OnStoreOpen?.Invoke();
    }
}
