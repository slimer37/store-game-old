using UnityEngine;
using TMPro;

public class Monitor : Interactable
{
    protected override Hover.Icon HoverIcon => Hover.Icon.Access;

    [SerializeField] Renderer rend;
    [SerializeField] TextMeshPro channelNumText;
    [SerializeField] TextMeshPro angleText;
    [SerializeField] bool enableNightVision;

    int channel = 0;

    Surveil CurrentCam => Surveil.AllCameras[channel];

    void Start()
    {
        channelNumText.gameObject.SetActive(interactable);
        angleText.gameObject.SetActive(interactable);
        UpdateScreen();
    }

    void ChangeChannel(int amount)
    {
        channel = (Surveil.AllCameras.Count + channel + amount) % Surveil.AllCameras.Count;
        UpdateScreen();
    }

    public override void Interact() => ChangeChannel(1);
    public override void SecondaryInteract() => ChangeChannel(-1);

    void Update()
    {
        angleText.text = $"{CurrentCam.transform.localEulerAngles.y:000}°";
        channelNumText.text = $"CH {channel + 1}" + (enableNightVision ? $"<b><size=50%>NV:<color={(CurrentCam.NightVision ? "green>ON" : "yellow>OFF")}" : "");
    }

    void UpdateScreen()
    {
        Texture texture = interactable ? CurrentCam.Texture : null;
        rend.material.SetTexture("_BaseMap", texture);
        rend.material.SetTexture("_EmissionMap", texture);

        Color color = interactable ? Color.white : Color.black;
        rend.material.SetColor("_BaseColor", color);
        rend.material.SetColor("_EmissionColor", color);
    }

    public void ToggleNightVision()
    {
        if (enableNightVision)
        { CurrentCam.ToggleNightVision(); }
    }

    public void ToggleOn()
    {
        interactable = !interactable;
        channelNumText.gameObject.SetActive(interactable);
        angleText.gameObject.SetActive(interactable);
        UpdateScreen();
    }
}
