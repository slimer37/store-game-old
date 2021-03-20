using UnityEngine;
using TMPro;

public class Monitor : Interactable
{
    protected override Hover.Icon HoverIcon => Hover.Icon.Access;

    [SerializeField] Renderer rend;
    [SerializeField] TextMeshPro channelNumText;
    [SerializeField] TextMeshPro angleText;
    [SerializeField] bool enableNightVision;
    [SerializeField, Min(1)] int staticTextureCount;

    static Texture2D[] staticTextures;
    bool isShowingStatic = false;

    int channel = 0;

    Surveil CurrentCam => Surveil.AllCameras[channel];

    void Awake()
    {
        if (staticTextures != null) return;

        staticTextures = new Texture2D[staticTextureCount];
        for (int i = 0; i < staticTextureCount; i++)
        {
            staticTextures[i] = new Texture2D(200, 150);
            for (int x = 0; x < staticTextures[i].width; x++)
            {
                for (int y = 0; y < staticTextures[i].height; y++)
                {
                    var value = Random.value > 0.5f ? 0.75f : 0;
                    staticTextures[i].SetPixel(x, y, new Color(value, value, value));
                }
            }
            staticTextures[i].Apply();
        }
    }

    public override void Interact() => ChangeChannel(1);
    public override void SecondaryInteract() => ChangeChannel(-1);

    void Start()
    {
        TurnOnScreen(interactable);
        ChangeChannel(0);
    }

    void ChangeChannel(int amount)
    {
        CancelInvoke();
        channel = (Surveil.AllCameras.Count + channel + amount) % Surveil.AllCameras.Count;

        isShowingStatic = true;
        Invoke(nameof(UpdateScreen), 0.25f);
    }

    void UpdateScreen()
    {
        isShowingStatic = false;
        SetScreenTexture(interactable ? CurrentCam.Texture : null);
    }

    void TurnOnScreen(bool on)
    {
        var color = on ? Color.white : Color.black;
        rend.material.SetColor("_BaseColor", color);
        rend.material.SetColor("_EmissionColor", color);
        if (on) ChangeChannel(0);
    }

    void SetScreenTexture(Texture texture)
    {
        rend.material.SetTexture("_BaseMap", texture);
        rend.material.SetTexture("_EmissionMap", texture);
    }

    public void ToggleNightVision()
    {
        if (enableNightVision)
        { CurrentCam.ToggleNightVision(); }
    }

    public void ToggleOn()
    {
        interactable = !interactable;
        TurnOnScreen(interactable);
    }

    void Update()
    {
        if (isShowingStatic)
        { SetScreenTexture(staticTextures[Random.Range(0, staticTextureCount)]); }

        if (interactable)
        {
            angleText.text = $"{CurrentCam.transform.localEulerAngles.y:000}°";
            channelNumText.text = $"CH {channel + 1}" + (enableNightVision ? $"<b><size=50%> NV:<color={(CurrentCam.NightVision ? "green>ON" : "yellow>OFF")}" : "");
        }
        else
        {
            angleText.text = "";
            channelNumText.text = "";
        }
    }
}
