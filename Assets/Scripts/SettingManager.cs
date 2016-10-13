using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;

public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureQualityDropdown;
    public Dropdown antiAliasingDropdown;
    public Dropdown vSyncDropdown;
    public Slider musicVolumeSlider;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    private 

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

        resolutions = Screen.resolutions;
    }

    public void OnFullscreenToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Resolution[] resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionDropdown.options[i].text = ResToString(resolutions[i]);
        }
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, true);
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntiAliasingChange()
    {
        //SMAA.QualityPreset quality = SMAA.QualityPreset.antiAliasingDropdown.value;

        SMAA.QualityPreset quality;
        if (antiAliasingDropdown.value == 0)
        {
            //antiAliasingToggle.isOn = false;
        }
        else if (antiAliasingDropdown.value == 1)
        {
            //antiAliasingToggle.isOn = false;
        }
        else if (antiAliasingDropdown.value == 2)
        {
            quality = SMAA.QualityPreset.Low;
        }
        else if (antiAliasingDropdown.value == 3)
        {
            quality = SMAA.QualityPreset.Medium;
        }
        else if (antiAliasingDropdown.value == 4)
        {
            quality = SMAA.QualityPreset.High;
        }
        else if (antiAliasingDropdown.value == 5)
        {
            quality = SMAA.QualityPreset.Ultra;
        }
    }

    public void OnVSyncChange()
    {

    }

    public void OnMusicVolumeChange()
    {

    }

    public void SaveSettings()
    {

    }

    public void LoadSettings()
    {

    }
}
