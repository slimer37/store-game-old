using UnityEngine;

[CreateAssetMenu(menuName = "Game/Product Info", fileName = "New Product")]
public class ProductInfo : ScriptableObject
{
    [field: SerializeField, Multiline] public string DisplayName { get; private set; }
    [field: SerializeField, TextArea(8, 100)] public string Description { get; private set; }
    [field: SerializeField] public float Price { get; private set; }

    public string DisplayString => $"<size=150%>{DisplayName}</size> - {Price:c}\n{Description}";
    public string ReceiptPhrase => $"{DisplayName} - {Price:c}";
    public string DisplayPrice => Price.ToString("c");
}
