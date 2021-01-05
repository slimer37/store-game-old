using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    void Awake() => GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
}