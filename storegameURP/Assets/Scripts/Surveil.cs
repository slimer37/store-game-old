using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Surveil : MonoBehaviour
{
    [SerializeField] private float turnInterval;
    [SerializeField] private float turnDegrees;
    [SerializeField] private float turnTime;

    [Header("Rendering")]
    [SerializeField] private Camera cam;
    [SerializeField] private Volume volume;

    [field: Header("Night Vision")]
    [field: SerializeField] public VolumeProfile NightVisionProfile { get; private set; }

    private VolumeProfile defaultProfile;

    public RenderTexture Texture { get; private set; }
    public static List<Surveil> AllCameras { get; private set; } = new List<Surveil>();

    void OnDestroy() => AllCameras.Remove(this);

    void Awake()
    {
        AllCameras.Add(this);
        defaultProfile = volume.sharedProfile;
        Texture = new RenderTexture(512, 256, 24);
        cam.targetTexture = Texture;
    }

    public void UseNightVision(bool value) => volume.sharedProfile = value ? NightVisionProfile : defaultProfile;

    IEnumerator Start()
    {
        Vector3 original = transform.localEulerAngles;
        Vector3 spin = Vector3.up * turnDegrees;

        while (true)
        {
            yield return Turn(original + spin);
            yield return Turn(original - spin);
        }

        IEnumerator Turn(Vector3 endEuler)
        {
            yield return Tweens.LerpRotationEuler(transform, endEuler, turnTime, true);
            yield return new WaitForSeconds(turnInterval);
        }
    }
}
