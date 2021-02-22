using UnityEngine;
using TMPro;

public class Mood : MonoBehaviour
{
    [SerializeField] Renderer faceRend;
    [SerializeField] Texture2D[] expressions;
    [SerializeField] TextMeshPro remarkBubble;
    [SerializeField] string[] painRemarks;

    int MoodStage
    {
        get => stage;
        set
        {
            stage = Mathf.Clamp(value, 0, expressions.Length);
            faceRend.material.mainTexture = expressions[stage];
        }
    }

    int stage;

    void DecreaseMood()
    {
        MoodStage--;
    }
}
