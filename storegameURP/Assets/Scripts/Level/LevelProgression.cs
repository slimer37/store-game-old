using System;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public Action OnStoreOpen;

    public static LevelProgression CurrentLevel { get; private set; }
    public bool StoreOpen { get; private set; }

    private float money;

    void Awake() => CurrentLevel = this;

    public void OpenStore()
    {
        StoreOpen = true;
        OnStoreOpen?.Invoke();
    }
}
