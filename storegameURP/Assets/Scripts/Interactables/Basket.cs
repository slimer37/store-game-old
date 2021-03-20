using UnityEngine;

[RequireComponent(typeof(Container))]
public class Basket : Tool
{
    protected override string Tooltip => container.Info;

    Container container;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out container);
    }

    protected override void Pickup(bool pickup)
    {
        if (!CheckDropPos) return;
        container.FreezeItems(pickup);
        base.Pickup(pickup);
        container.Active = !pickup;
    }
}
