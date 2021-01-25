using UnityEngine;
using UnityEngine.InputSystem;

public class Cart : Driveable
{
    protected override Hover.Icon HoverIcon => Driving ? Hover.Icon.Invalid : Hover.Icon.Pull;
    protected override string Tooltip => container.Info;

    [Header("Front Wheel Animation")]
    [SerializeField] private Transform[] frontWheels;
    [SerializeField] private float wheelTurnAmount;

    [SerializeField] private Container container;

    private float wheelTurnT = 0;

    protected override void OnMovement(InputAction.CallbackContext ctx)
    {
        base.OnMovement(ctx);
        container.FreezeItems(ctx.ReadValue<Vector2>().magnitude > 0);
        wheelTurnT = 0;
    }

    protected override void Update()
    {
        base.Update();

        var maxTurn = Quaternion.Euler(InputDirection.x * Vector3.up * wheelTurnAmount);
        foreach (var wheel in frontWheels)
        { wheel.localRotation = Quaternion.Lerp(wheel.localRotation, maxTurn, wheelTurnT += Time.deltaTime); }
    }

    void LateUpdate()
    {
        container.transform.position = transform.position;
        container.transform.rotation = transform.rotation;
    }
}
