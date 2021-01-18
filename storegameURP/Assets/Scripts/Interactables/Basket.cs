using UnityEngine;

[RequireComponent(typeof(Container))]
public class Basket : Tool
{
    protected override string Tooltip => container.Info;

    private Container container;

    protected override void Awake()
    {
        base.Awake();
        verifyWithRay = false;
        TryGetComponent(out container);
    }

    protected override void Pickup(bool pickup)
    {
        container.FreezeItems(pickup);
        base.Pickup(pickup);
        container.Active = !pickup;
    }
}
