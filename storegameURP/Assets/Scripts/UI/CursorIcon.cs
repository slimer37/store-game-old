using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CursorIcon : MonoBehaviour
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

    private static CursorIcon current;

    void Awake() => current = this;

    public static void ShowIcon(Icon iconChoice, string tooltip)
    {
        if (iconChoice == Icon.None)
        { Reset(); }
        else
        {
            current.cursorImage.enabled = true;
            current.cursorImage.sprite = current.iconSprites[(int)iconChoice];
            current.dotImage.enabled = false;
        }

        current.tooltipText.text = tooltip;
    }

    public static void Reset()
    {
        current.cursorImage.enabled = false;
        current.dotImage.enabled = true;
        current.tooltipText.text = "";
    }
}
