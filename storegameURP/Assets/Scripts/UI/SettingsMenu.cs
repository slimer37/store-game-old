using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu
{
    [System.Serializable]
    struct SliderSetting
    {
        public string setting;
        public Slider slider;
    }

    [System.Serializable]
    struct ToggleSetting
    {
        public string setting;
        public Toggle toggle;
    }

    [SerializeField] SliderSetting[] sliderSettings;
    [SerializeField] ToggleSetting[] toggleSettings;

    protected override void Awake()
    {
        foreach (var setting in sliderSettings)
        {
            setting.slider.value = PlayerPrefs.GetFloat(setting.setting);
            setting.slider.onValueChanged.AddListener(value => PlayerPrefs.SetFloat(setting.setting, value));
        }
        foreach (var setting in toggleSettings)
        {
            setting.toggle.isOn = PlayerPrefs.GetInt(setting.setting) == 1;
            setting.toggle.onValueChanged.AddListener(value => PlayerPrefs.SetInt(setting.setting, value ? 1 : 0));
        }

        base.Awake();
    }
}
