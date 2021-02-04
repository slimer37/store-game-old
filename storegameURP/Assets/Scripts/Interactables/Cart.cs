using UnityEngine;
using UnityEngine.InputSystem;

public class Cart : Driveable
{
    protected override Hover.Icon HoverIcon => Driving ? Hover.Icon.Invalid : Hover.Icon.Pull;
    protected override string Tooltip => container.Info;

    [Header("Front Wheel Animation")]
    [SerializeField] private Transform[] frontWheels;
    [SerializeField] private float wheelTurnAmount;
    [SerializeField] private float itemVelocityLimitWhileDriving;

    [SerializeField] private Container container;

    private float wheelTurnT = 0;

    protected override void OnMovement(InputAction.CallbackContext ctx)
    {
        base.OnMovement(ctx);
        wheelTurnT = 0;
        container.FreezeItems(InputDirection.y != 0, false);
    }

    protected override void StartDriving(bool value)
    {
        base.StartDriving(value);
        container.FreezeItems(false, false);
    }

    void LateUpdate()
    {
        var maxTurn = Quaternion.Euler(InputDirection.x * Vector3.up * wheelTurnAmount);
        foreach (var wheel in frontWheels)
        { wheel.localRotation = Quaternion.Lerp(wheel.localRotation, maxTurn, wheelTurnT += Time.deltaTime); }

        container.transform.position = transform.position;
        container.transform.rotation = transform.rotation;
    }
}
