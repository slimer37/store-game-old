using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private int capacity;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float scaleAnchor;
    [SerializeField] private float triggerHeight;

    private List<Product> Contents = new List<Product>();

    public bool Active { get; set; } = true;
    public int count => Contents.Count;
    public string Info => $"{Contents.Count}/{capacity} items";

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Product item))
        { AddItem(item, true); }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Product item))
        { AddItem(item, false); }
    }

    void AddItem(Product item, bool add)
    {
        if (!Active || add && Contents.Count == capacity) return;

        if (add)
        { Contents.Add(item); }
        else
        {
            Contents.Remove(item);
            item.transform.localScale = item.OriginalScale;
        }
        item.transform.parent = add ? transform : null;
    }

    void FixedUpdate()
    {
        if (!Active || Contents.Count == 0) return;

        foreach (var item in Contents)
        {
            float height = Mathf.Max(0, item.transform.localPosition.y - scaleAnchor);
            item.transform.localScale = item.OriginalScale * Mathf.Lerp(scaleFactor, 1, height / triggerHeight);
        }
    }

    public void FreezeItems(bool freeze, bool clear = true)
    {
        foreach (var item in Contents)
        { item.Freeze(false, freeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None); }

        if (clear && !freeze)
        { Contents.Clear(); }
    }
}
