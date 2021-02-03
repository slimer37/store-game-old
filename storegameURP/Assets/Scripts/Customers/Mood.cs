using UnityEngine;
using TMPro;

public class Mood : MonoBehaviour
{
    [SerializeField] private Renderer faceRend;
    [SerializeField] private Texture2D[] expressions;
    [SerializeField] private TextMeshPro remarkBubble;
    [SerializeField] private string[] painRemarks;

    private int MoodStage
    {
        get => stage;
        set
        {
            stage = Mathf.Clamp(value, 0, expressions.Length);
            faceRend.material.mainTexture = expressions[stage];
        }
    }

    private int stage;

    void DecreaseMood()
    {
        MoodStage--;
    }
}
