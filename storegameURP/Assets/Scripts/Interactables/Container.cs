using System.Collections.Generic;
using UnityEngine;

public class Container : Pickuppable
{
    protected override CursorIcon.Icon HoverIcon => Interaction.Held
            ? contents.Count < capacity ? CursorIcon.Icon.Access : CursorIcon.Icon.Invalid
            : isHeld ? CursorIcon.Icon.None : CursorIcon.Icon.Pickup;

    protected override string Tooltip => $"{contents.Count}/{capacity} items\n"
        + (contents.Count > 0 ? $"including {contents[0].Info.DisplayName}" : "");

    [SerializeField] private Vector3 holdPosition;
    [SerializeField] private Vector3 holdRotation;
    [SerializeField] private int capacity;
    [Tooltip("If checked, the list is a whitelist. Otherwise, it is a blacklist.")]
    [SerializeField] private bool exclusive;
    [SerializeField] private ProductInfo[] list;

    private List<Product> contents = new List<Product>();
    private const float cameraAngleMultiplier = 0.005f;

    void Update()
    {
        if (isHeld)
        {
            float angle = Interaction.CamTransform.eulerAngles.x;
            if (angle > 180)
            { angle -= 360; }
            float delta = -angle * cameraAngleMultiplier;
            transform.localPosition = holdPosition + Vector3.up * delta;
        }
    }

    protected override void Pickup(bool pickup)
    {
        base.Pickup(pickup);

        if (pickup)
        { Rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; }
        Rb.isKinematic = pickup;
        if (!pickup)
        { Rb.collisionDetectionMode = CollisionDetectionMode.Continuous; }

        if (!pickup)
        {
            transform.localPosition = Vector3.forward;
            transform.LookAt(Interaction.PlayerTransform);
        }

        transform.parent = pickup ? Interaction.PlayerTransform : null;

        if (pickup)
        { transform.localRotation = Quaternion.Euler(holdRotation); }
    }

    public override void SecondaryInteract()
    {
        Product item;
        if (Interaction.Held is Product && contents.Count < capacity)
        {
            item = Interaction.Held as Product;

            if (CheckAgainstList(item.Info))
            {
                item.Drop();
                contents.Add(item);
                item.gameObject.SetActive(false);
            }
        }
        else if (!Interaction.Held && contents.Count > 0)
        {
            item = contents[contents.Count - 1];
            contents.Remove(item);
            item.gameObject.SetActive(true);
            item.transform.position = transform.position + Vector3.up * 0.5f - Interaction.CamTransform.forward * 0.5f;
            item.Interact();
        }
    }

    private bool CheckAgainstList(ProductInfo check)
    {
        foreach (var product in list)
        { if (check == product) return exclusive; }
        return !exclusive;
    }
}
