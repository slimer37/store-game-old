using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Action OnStoreOpen;

    public static Level Current { get; private set; }
    public bool StoreOpen { get; private set; }
    [field: SerializeField, Range(1, 100)] public int Capacity { get; private set; }

    private float money;

    void Awake() => Current = this;

    public void OpenStore()
    {
        StoreOpen = true;
        OnStoreOpen?.Invoke();
    }
}
