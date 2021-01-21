using UnityEngine;
using TMPro;

[RequireComponent(typeof(Renderer))]
public class FeatureRandomizer : MonoBehaviour
{
    [System.Serializable]
    private struct ColorRandomizer
    {
        public int matIndex;
        public Color[] colorSet;
        public ExtraRenderer[] extraRenderers;
    }

    [System.Serializable]
    private struct ExtraRenderer
    {
        public Renderer renderer;
        public int matIndex;
    }

    [System.Serializable]
    private struct TextRandomizer
    {
        public TextMeshPro[] textObjects;
        [TextArea(0, 4)] public string[] textSet;
        public string prefix;
        public string suffix;
    }

    [SerializeField] private ColorRandomizer[] colorRandomizers = new ColorRandomizer[] { };
    [SerializeField] private TextRandomizer[] textRandomizers = new TextRandomizer[] { };
    [SerializeField, Range(0, 100)] private int appearanceChance = 100;

    private Renderer rend;

    void Awake()
    {
        TryGetComponent(out rend);
        Randomize();
    }

    public void Randomize()
    {
        if (Random.Range(0, 101) > appearanceChance)
        {
            gameObject.SetActive(false);
            return;
        }

        foreach (var colorRand in colorRandomizers)
        {
            Color color = colorRand.colorSet[Random.Range(0, colorRand.colorSet.Length)];
            rend.materials[colorRand.matIndex].color = color;

            foreach (var extraRend in colorRand.extraRenderers)
            { extraRend.renderer.materials[extraRend.matIndex].color = color; }
        }

        foreach (var textRand in textRandomizers)
        {
            var chosenText = textRand.textSet[Random.Range(0, textRand.textSet.Length)];
            foreach (var text in textRand.textObjects)
            { text.text = textRand.prefix + chosenText + textRand.suffix; }
        }
    }
}