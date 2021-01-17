using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Container : MonoBehaviour
{
    [SerializeField] private int capacity;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float scaleAnchor;
    [SerializeField] private float triggerHeight;
    
    private List<Product> Contents = new List<Product>();

    public bool Active { get; set; } = true;
    public string Info => $"{Contents.Count}/{capacity} items";

    void OnTriggerEnter(Collider other)
    {
        if (Active && other.TryGetComponent(out Product item))
        { AddItem(item, true); }
    }

    void OnTriggerExit(Collider other)
    {
        if (Active && other.TryGetComponent(out Product item))
        { AddItem(item, false); }
    }

    void AddItem(Product item, bool add)
    {
        Debug.Log((add ? "Gained " : "Lost ") + item.gameObject.name);
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

    public void FreezeItems(bool freeze)
    {
        foreach (var item in Contents)
        { SetRigidbody(item.Rb); }

        if (!freeze)
        { Contents.Clear(); }

        void SetRigidbody(Rigidbody rb)
        {
            if (freeze)
            { rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; }
            rb.isKinematic = freeze;
            if (!freeze)
            { rb.collisionDetectionMode = CollisionDetectionMode.Continuous; }
        }
    }
}
