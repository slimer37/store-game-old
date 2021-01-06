using UnityEngine;

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

    [SerializeField] private ColorRandomizer[] colorRandomizers = new ColorRandomizer[] { };
    [SerializeField, Range(0, 100)] private int appearanceChance = 100;

    void Awake()
    {
        if (Random.Range(0, 101) > appearanceChance)
        { Destroy(gameObject); }

        var rend = GetComponent<Renderer>();
        foreach (var colorRand in colorRandomizers)
        {
            Color color = colorRand.colorSet[Random.Range(0, colorRand.colorSet.Length)];
            rend.materials[colorRand.matIndex].color = color;

            foreach (var extraRend in colorRand.extraRenderers)
            { extraRend.renderer.materials[extraRend.matIndex].color = color; }
        }
    }
}