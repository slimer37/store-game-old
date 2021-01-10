using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public UnityEvent OnStoreOpen;
    public UnityEvent OnProfit;

    public static Level Current { get; private set; }
    public bool StoreOpen { get; private set; }
    [field: SerializeField, Range(1, 100)] public int Capacity { get; private set; }

    public float Money
    {
        get => money;
        set
        {
            float temp = money;
            money = value;
            if (money > temp)
            { OnProfit?.Invoke(); }
        }
    }
    private float money;

    void Awake() => Current = this;

    public void OpenStore()
    {
        StoreOpen = true;
        OnStoreOpen?.Invoke();
    }
}
