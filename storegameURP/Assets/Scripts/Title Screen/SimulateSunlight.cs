using System;
using UnityEngine;

public class SimulateSunlight : MonoBehaviour
{
    [Serializable]
    struct LightingSituation
    {
        public string name;
        public Vector2 hourRange;
        public float sunIntensity;
        public Vector3 sunAngle;
        public float indoorIntensity;
    }

    [SerializeField] LightingSituation[] lightingSituations;
    [SerializeField] Light indoorLight;
    [ContextMenuItem("Override Now", "Awake")]
    [SerializeField] int overrideHour;
    [SerializeField] bool enable;

    void Awake()
    {
        if (!enable) return;

        var hour = overrideHour == 0 ? DateTime.Now.Hour : overrideHour;
        foreach (var sunPos in lightingSituations)
        {
            if (hour >= sunPos.hourRange.x && hour <= sunPos.hourRange.y)
            {
                transform.localEulerAngles = sunPos.sunAngle;
                GetComponent<Light>().intensity = sunPos.sunIntensity;
                indoorLight.intensity = sunPos.indoorIntensity;
                return;
            }
        }
    }
}
