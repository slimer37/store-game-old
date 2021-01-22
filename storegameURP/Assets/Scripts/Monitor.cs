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
        UpdateScreen();
        UpdateText();
    }

    public override void Interact() => ChangeChannel(1);
    public override void SecondaryInteract() => ChangeChannel(-1);

    public void ToggleNightVision()
    {
        if (!enableNightVision) return;

        useNightVision = !useNightVision;
        currentCam.UseNightVision(useNightVision);
        UpdateText();
    }

    void ChangeChannel(int amount)
    {
        channel = (Surveil.AllCameras.Count + channel + amount) % Surveil.AllCameras.Count;
        currentCam.UseNightVision(useNightVision);
        UpdateScreen();
        UpdateText();
    }

    void UpdateText() => channelNumText.text = $"CH {channel} <b>" + (enableNightVision ?
        $"<size=50%>NV:<color={(useNightVision ? "green>ON" : "yellow>OFF")}" : "");

    void UpdateScreen()
    {
        rend.material.SetTexture("_BaseMap", currentCam.Texture);
        rend.material.SetTexture("_EmissionMap", currentCam.Texture);
    }

    void Update() => angleText.text = $"{currentCam.transform.localEulerAngles.y:000}°";
}
