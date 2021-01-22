using UnityEngine;
using TMPro;

public class Monitor : Interactable
{
    protected override CursorIcon.Icon HoverIcon => CursorIcon.Icon.Access;

    [SerializeField] private Renderer rend;
    [SerializeField] private TextMeshPro channelNumText;
    [SerializeField] private TextMeshPro angleText;
    [SerializeField] private bool enableNightVision;

    private bool useNightVision = false;
    private int channel = 0;

    private Surveil currentCam => Surveil.AllCameras[channel];

    void Start()
    {
        channelNumText.gameObject.SetActive(interactable);
        angleText.gameObject.SetActive(interactable);
        UpdateScreen();
        UpdateText();
    }

    void ChangeChannel(int amount)
    {
        channel = (Surveil.AllCameras.Count + channel + amount) % Surveil.AllCameras.Count;
        currentCam.UseNightVision(useNightVision);
        UpdateScreen();
        UpdateText();
    }

    public override void Interact() => ChangeChannel(1);
    public override void SecondaryInteract() => ChangeChannel(-1);
    void Update() => angleText.text = $"{currentCam.transform.localEulerAngles.y:000}�";

    void UpdateText() => channelNumText.text = $"CH {channel} <b>" + (enableNightVision ?
        $"<size=50%>NV:<color={(useNightVision ? "green>ON" : "yellow>OFF")}" : "");

    void UpdateScreen()
    {
        Texture texture = interactable ? currentCam.Texture : null;
        rend.material.SetTexture("_BaseMap", texture);
        rend.material.SetTexture("_EmissionMap", texture);

        Color color = interactable ? Color.white : Color.black;
        rend.material.SetColor("_BaseColor", color);
        rend.material.SetColor("_EmissionColor", color);
    }

    public void ToggleNightVision()
    {
        if (!enableNightVision) return;

        useNightVision = !useNightVision;
        currentCam.UseNightVision(useNightVision);
        UpdateText();
    }

    public void ToggleOn()
    {
        interactable = !interactable;
        channelNumText.gameObject.SetActive(interactable);
        angleText.gameObject.SetActive(interactable);
        UpdateScreen();
    }
}
