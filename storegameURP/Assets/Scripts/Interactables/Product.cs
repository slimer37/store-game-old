using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : Pickuppable
{
    [field: SerializeField] public ProductInfo Info { get; set; }

    [SerializeField] private Material transparentMat;

    public static List<Product> AllProducts { get; private set; } = new List<Product>();

    private Renderer rend;
    private bool marked;

    public static List<Product> ProductsAvailable
    {
        get
        {
            List<Product> available = new List<Product>();
            foreach (var product in AllProducts)
            {
                if (!product.marked)
                { available.Add(product); }
            }
            return available;
        }
    }

    public static Product GetRandomProduct()
    {
        if (ProductsAvailable.Count == 0)
        { throw new System.InvalidOperationException("Cannot select random product when none are available. Try checking for ProductsAvailable first."); }

        List<Product> available = ProductsAvailable;

        int choice = Random.Range(0, available.Count);
        available[choice].marked = true;
        return available[choice];
    }

    void Start()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        Material temp = new Material(transparentMat);
        temp.color = new Color(rend.material.color.a, rend.material.color.g, rend.material.color.b, 1);
        temp.mainTexture = rend.material.mainTexture;
        rend.material = temp;
        AllProducts.Add(this);
    }

    public IEnumerator FadeAndMove(Vector3 from, Vector3 to, bool value)
    {
        ResetRb();
        SetInteractable(false);

        Color originalColor = rend.material.color;
        float finalAlpha = value ? 0 : 1;

        if (value)
        { to.y = transform.position.y; }

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, finalAlpha, t));
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }

        rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, finalAlpha);
        transform.position = to;

        ResetRb();
        SetInteractable(true);

        void ResetRb()
        {
            Rb.velocity = Vector3.zero;
            Rb.angularVelocity = Vector3.zero;
        }

        void SetInteractable(bool condition)
        {
            if (value == condition)
            { gameObject.SetActive(!condition); }
            else
            { interactable = condition; }
        }
    }
}