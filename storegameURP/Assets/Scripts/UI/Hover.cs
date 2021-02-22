using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hover : MonoBehaviour
{
    public enum Icon
    {
        None = -1,
        Invalid,
        Access,
        Pickup,
        Pull,
        Push,
        View
    }

    [Tooltip("Access, Pickup, Pull, Push, Invalid")]
    [SerializeField] Sprite[] iconSprites;
    [SerializeField] Image cursorImage;
    [SerializeField] Image dotImage;
    [SerializeField] TextMeshProUGUI tooltipText;

    Transform hoveredTransform;

    public static Hover Current { get; private set; }

    void Awake() => Current = this;

    void OnDisable() => ResetIcon();

    public void ShowIcon(Icon iconChoice, string tooltip)
    {
        if (iconChoice == Icon.None)
        {
            ResetIcon(false);
            return;
        }

        cursorImage.enabled = true;
        cursorImage.sprite = Current.iconSprites[(int)iconChoice];
        dotImage.enabled = false;
        tooltipText.text = tooltip;
    }

    public void SendMessageToHovered(string message)
    {
        if (hoveredTransform)
        { hoveredTransform.SendMessage(message); }
    }

    public void Cast(Ray ray, float distance, LayerMask mask)
    {
        if (!enabled) return;

        if (Physics.Raycast(ray, out RaycastHit hit, distance, mask))
        { Over(hit.transform); }
        else if (hoveredTransform)
        { ResetIcon(); }
    }

    void Over(Transform hoveredObj)
    {
        // Do nothing if the hovered object hasn't changed.
        if (hoveredTransform && hoveredObj == hoveredTransform) return;

        hoveredTransform = hoveredObj;
        SendMessageToHovered("OnHover");
    }

    void ResetIcon(bool setHoveredNull = true)
    {
        cursorImage.enabled = false;
        dotImage.enabled = true;
        tooltipText.text = "";

        if (setHoveredNull && hoveredTransform)
        { hoveredTransform = null; }
    }
}
