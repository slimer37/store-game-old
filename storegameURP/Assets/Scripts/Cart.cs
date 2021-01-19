using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Cart : Driveable
{
    protected override CursorIcon.Icon HoverIcon => Rb.isKinematic ? CursorIcon.Icon.Invalid : CursorIcon.Icon.Pull;
    protected override string Tooltip => container.Info;

    [Header("Front Wheel Animation")]
    [SerializeField] private Transform[] frontWheels;
    [SerializeField] private float wheelTurnAmount;

    [SerializeField] private TextMeshProUGUI exitInstructions;

    private Container container;
    private float wheelTurnT = 0;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out container);
    }

    protected override void OnMovement(InputAction.CallbackContext ctx)
    {
        base.OnMovement(ctx);
        container.FreezeItems(ctx.ReadValue<Vector2>().magnitude > 0);
        wheelTurnT = 0;
    }

    protected override void BeginDriving(bool value)
    {
        base.BeginDriving(value);

        if (value)
        { StartCoroutine(FadeText()); }
        else
        { exitInstructions.gameObject.SetActive(false); }
    }

    System.Collections.IEnumerator FadeText()
    {
        exitInstructions.gameObject.SetActive(true);
        exitInstructions.CrossFadeAlpha(1, 0, false);
        yield return new WaitForSeconds(2);
        exitInstructions.CrossFadeAlpha(0, 1, false);
    }

    protected override void Update()
    {
        base.Update();

        var maxTurn = Quaternion.Euler(InputDirection.x * Vector3.up * wheelTurnAmount);
        foreach (var wheel in frontWheels)
        { wheel.localRotation = Quaternion.Lerp(wheel.localRotation, maxTurn, wheelTurnT += Time.deltaTime); }
    }
}
