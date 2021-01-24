using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : Pickuppable
{
    [field: SerializeField] public ProductInfo Info { get; set; }

    [SerializeField] private Material transparentMat;

    public static List<Product> AllProducts { get; private set; } = new List<Product>();

    private Renderer rend;
    public bool Marked;

    public static List<Product> ProductsAvailable
    {
        get
        {
            List<Product> available = new List<Product>();
            foreach (var product in AllProducts)
            {
                if (!product.Marked && !product.isHeld)
                { available.Add(product); }
            }
            return available;
        }
    }

    public static Product GetRandomProduct()
    {
        if (ProductsAvailable.Count == 0)
        { return null; }

        List<Product> available = ProductsAvailable;

        int choice = Random.Range(0, available.Count);
        available[choice].Marked = true;
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

    void OnDestroy() => AllProducts.Remove(this);

    public IEnumerator FadeAndMove(Vector3 from, Vector3 to, bool value)
    {
        ResetRb();
        SetInteractable(false);

        Color originalColor = rend.material.color;
        float finalAlpha = value ? 0 : 1;

        if (value)
        { to.y = transform.position.y; }

        yield return Tweens.LerpValue(1, t =>
        {
            rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, finalAlpha, t));
            transform.position = Vector3.Lerp(from, to, t);
        });

        ResetRb();
        SetInteractable(true);

        void ResetRb()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
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
