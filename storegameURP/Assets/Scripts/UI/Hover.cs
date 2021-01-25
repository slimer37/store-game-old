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
    [SerializeField] private Sprite[] iconSprites;
    [SerializeField] private Image cursorImage;
    [SerializeField] private Image dotImage;
    [SerializeField] private TextMeshProUGUI tooltipText;

    private Transform hoveredTransform;

    public static Hover Current { get; private set; }

    void Awake() => Current = this;

    public void ShowIcon(Icon iconChoice, string tooltip)
    {
        if (iconChoice == Icon.None)
        { Reset(); }
        else
        {
            cursorImage.enabled = true;
            cursorImage.sprite = Current.iconSprites[(int)iconChoice];
            dotImage.enabled = false;
        }

        tooltipText.text = tooltip;
    }

    public Transform Cast(Ray ray, float distance, LayerMask mask = new LayerMask())
    {
        if (Physics.Raycast(ray, out RaycastHit hit, distance))
        { Over(hit.transform); }
        else
        { Reset(); }
        return hoveredTransform;
    }

    void Over(Transform hovered)
    {
        if (!hoveredTransform || hovered != hoveredTransform)
        {
            if (hoveredTransform)
            { hoveredTransform.SendMessage("OnHoverExit", SendMessageOptions.DontRequireReceiver); }
            hovered.SendMessage("OnHover", SendMessageOptions.DontRequireReceiver);
            hoveredTransform = hovered;
        }
    }

    public void Reset()
    {
        cursorImage.enabled = false;
        dotImage.enabled = true;
        tooltipText.text = "";

        if (hoveredTransform)
        {
            hoveredTransform.SendMessage("OnHoverExit", SendMessageOptions.DontRequireReceiver);
            hoveredTransform = null;
        }
    }
}
