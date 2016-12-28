using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Water;
using UnityStandardAssets.CinematicEffects;

public class Settings : MonoBehaviour
{
    [Header("Volume")]
    public Slider volumeSlider;

    [Header("Presets")]
    public Dropdown presetsDropdown;

    [Header("Resolution")]
    public Dropdown resolutionDropdown;

    [Header("Panels")]
    public GameObject[] panels;

    [Header("Fullscreen")]
    public Toggle fullscreenToggle;

    [Header("Bloom")]
    public Toggle bloomHighQualityToggle;
    public Toggle bloomAntiFlickerToggle;
    public Toggle bloomToggle;
    private Bloom bloomScript;

    [Header("Motion Blur")]
    private MotionBlur motionBlurScript;
    public Toggle motionBlurToggle;

    [Header("Frames Per Second Counter")]
    public GameObject framesPerSecondCounter;
    public Toggle framesPerSecondCounterToggle;

    [Header("Ambient Occlusion")]
    public Toggle ambientOcclusionToggle;
    private AmbientOcclusion ambientOcclusionScript;
    public Toggle ambientOcclusionDownsamplingToggle;
    public Dropdown ambientOcclusionQualityDropdown;

    [Header("Anti-Aliasing")]
    public Toggle antiAliasingToggle;
    public Dropdown antiAliasingDropdown;
    private AntiAliasing antiAliasingScript;

    [Header("Water")]
    public Dropdown waterQualityDropdown;
    public WaterBase waterScript;

    [Header("Lens Aberrations")]
    public Toggle lensAberrationsToggle;
    private LensAberrations lensAberrationsScript;
    public Toggle lensAberrationsDistortionToggle;
    public Toggle lensAberrationsVignetteToggle;
    public Toggle lensAberrationsChromaticAberrationToggle;

    [Header("Sun Rays")]
    private TOD_Rays sunRaysScript;
    public Toggle sunRaysToggle;

    [Header("Cloud Shadows")]
    public TOD_Shadows cloudShadowsScript;
    public Toggle cloudShadowsToggle;

    [Header("Render Distance")]
    public Dropdown renderDistanceDropdown;
    public Camera mainCam;

    //Saving
    private bool fullscreen;

    void Start()
    {
        mainCam = Camera.main;
        presetsDropdown.value = QualitySettings.GetQualityLevel();
        Refresh();

        bloomScript = mainCam.GetComponent<Bloom>();
        motionBlurScript = mainCam.GetComponent<MotionBlur>();
        ambientOcclusionScript = mainCam.GetComponent<AmbientOcclusion>();
        antiAliasingScript = mainCam.GetComponent<AntiAliasing>();
        sunRaysScript = mainCam.GetComponent<TOD_Rays>();
        cloudShadowsScript = mainCam.GetComponent<TOD_Shadows>();
        lensAberrationsScript = mainCam.GetComponent<LensAberrations>();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
    }

    void OnFullscreenToggle()
    {
        fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void Presets()
    {
        QualitySettings.SetQualityLevel(presetsDropdown.value);
        if (presetsDropdown.value == 0) //Very Low
        {
            Refresh();
        }
        else if (presetsDropdown.value == 1) //Low
        {
            Refresh();
        }
        else if (presetsDropdown.value == 2) //Medium
        {
            Refresh();
        }
        else if (presetsDropdown.value == 3) //High
        {
            Refresh();
        }
        else if (presetsDropdown.value == 4) //Very High
        {
            Refresh();
        }
        else if (presetsDropdown.value == 5) //Ultra
        {
            Refresh();
        }
        else //Other
        {
            Debug.LogError("Unknown Preset Value!");
        }
    }

    public void Resolutions()
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

    public void Refresh()
    {
        Resolutions();
        SetRenderDistance();
        SetWaterQuality();
        MotionBlur();
        FramesPerSecondCounter();
        AmbientOcclusion();
        SunRays();
        CloudShadows();
        AntiAliasing();
        LensAberrationsChromaticAberration();
        LensAberrationsDistortion();
        LensAberrations();
    }

    public void UpdateVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void MotionBlur()
    {
        motionBlurScript.enabled = motionBlurToggle.isOn;
    }

    public void FramesPerSecondCounter()
    {
        framesPerSecondCounter.SetActive(framesPerSecondCounterToggle.isOn);
    }

    public void AmbientOcclusion()
    {
        ambientOcclusionScript.enabled = ambientOcclusionToggle.isOn;
    }

    public void SunRays()
    {
        sunRaysScript.enabled = sunRaysToggle.isOn;
    }

    public void Bloom()
    {
        bloomScript.enabled = bloomToggle.isOn;
    }

    public void BloomHighQuality()
    {
        bloomScript.settings.highQuality = bloomHighQualityToggle.isOn;
    }

    public void BloomAntiFlicker()
    {
        if (bloomAntiFlickerToggle.isOn)
        {
            bloomScript.settings.antiFlicker = true;
        }
        else
        {
            bloomScript.settings.antiFlicker = false;
        }
    }

    public void LensAberrations()
    {
        if (lensAberrationsToggle.isOn)
        {
            lensAberrationsToggle.enabled = true;
        }
        else
        {
            lensAberrationsToggle.enabled = false;
        }
    }

    public void CloudShadows()
    {

        if (cloudShadowsToggle.isOn)
        {
            cloudShadowsScript.enabled = true;
        }
        else
        {
            cloudShadowsScript.enabled = false;
        }
    }

    public void LensAberrationsDistortion()
    {

        if (lensAberrationsDistortionToggle.isOn)
        {
            lensAberrationsScript.distortion.enabled = true;
            Debug.Log("Distortion false");
        }
        else
        {
            lensAberrationsScript.distortion.enabled = false;
            Debug.Log("Distortion true");
        }
    }

    public void LensAberrationsVegnette()
    {

        if (lensAberrationsVignetteToggle.isOn)
        {
            lensAberrationsScript.vignette.enabled = true;
        }
        else
        {
            lensAberrationsScript.vignette.enabled = false;
        }
    }

    public void LensAberrationsChromaticAberration()
    {

        if (lensAberrationsChromaticAberrationToggle.isOn)
        {
            lensAberrationsScript.chromaticAberration.enabled = true;
        }
        else
        {
            lensAberrationsScript.chromaticAberration.enabled = false;
        }
    }

    public void AntiAliasing()
    {
        antiAliasingDropdown.value = presetsDropdown.value;
        if (antiAliasingToggle.isOn)
        {
            antiAliasingScript.enabled = true;
        }
        else
        {
            antiAliasingScript.enabled = false;
        }

        SMAA.QualityPreset quality;
        if (antiAliasingDropdown.value == 0)
        {
            antiAliasingToggle.isOn = false;
        }
        else if (antiAliasingDropdown.value == 1)
        {
            antiAliasingToggle.isOn = false;
        }
        else if(antiAliasingDropdown.value == 2)
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

    public void AmbientOcclusionQuality()
    {
        ambientOcclusionQualityDropdown.value = presetsDropdown.value;

        if (ambientOcclusionQualityDropdown.value == 0)
        {
            ambientOcclusionScript.enabled = false;
        }
        else if (ambientOcclusionQualityDropdown.value == 1)
        {
            ambientOcclusionScript.enabled = false;
        }
        else if (ambientOcclusionQualityDropdown.value == 2)
        {
            ambientOcclusionScript.settings.sampleCountValue = 10;
        }
        else if (ambientOcclusionQualityDropdown.value == 3)
        {
            ambientOcclusionScript.settings.sampleCountValue = 18;
        }
        else if (ambientOcclusionQualityDropdown.value == 4)
        {
            ambientOcclusionScript.settings.sampleCountValue = 26;
        }
        else if (ambientOcclusionQualityDropdown.value == 5)
        {
            ambientOcclusionScript.settings.sampleCountValue = 34;
        }
    }

    /*
void OnEnable()
{
    //mainCam.farClipPlane = PlayerPrefs.GetFloat("Render Distance", 450);
    PlayerPrefsX.GetBool("Settings - Motion Blur Enabled");
    PlayerPrefsX.GetBool("Settings - Show FPS");
    PlayerPrefsX.GetBool("Settings - SSAO Enabled");
    PlayerPrefsX.GetBool("Settings - Sun Rays Enabled");
    PlayerPrefsX.GetBool("Settings - Cloud Shadows Enabled");
    PlayerPrefsX.GetBool("Settings - Anti-Aliasing Enabled");
    Debug.Log("Loaded Settings");
}

void OnDisable()
{
    //PlayerPrefs.SetFloat("Render Distance", mainCam.farClipPlane);
    PlayerPrefsX.SetBool("Settings - Motion Blur Enabled", motionBlurToggle.isOn);
    PlayerPrefsX.SetBool("Settings - Show FPS", showFPSToggle.isOn);
    PlayerPrefsX.SetBool("Settings - SSAO Enabled", SSAOToggle.isOn);
    PlayerPrefsX.SetBool("Settings - Sun Rays Enabled", sunRaysToggle.isOn);
    PlayerPrefsX.SetBool("Settings - Cloud Shadows Enabled", cloudShadowsToggle.isOn);
    PlayerPrefsX.SetBool("Settings - Anti-Aliasing Enabled", antiAliasingToggle.isOn);
    Debug.Log("Saved Settings");
}
*/

    public void Fullscreen()
    {
        if (fullscreenToggle.isOn)
            Screen.fullScreen = true; 
        else
            Screen.fullScreen = false;
    }

    public void SetRenderDistance()
    {
        renderDistanceDropdown.value = presetsDropdown.value;
        if (renderDistanceDropdown.value == 0)
            mainCam.farClipPlane = 100;

        else if (renderDistanceDropdown.value == 1)
            mainCam.farClipPlane = 200;

        else if (renderDistanceDropdown.value == 2)
            mainCam.farClipPlane = 500;

        else if (renderDistanceDropdown.value == 3)
            mainCam.farClipPlane = 800;

        else if (renderDistanceDropdown.value == 4)
            mainCam.farClipPlane = 1500;

        else
            mainCam.farClipPlane = 2000;
    }

    public void SetWaterQuality()
    {
        waterQualityDropdown.value = presetsDropdown.value;
        if (waterQualityDropdown.value == 0)
        {
            waterScript.waterQuality = WaterQuality.Low;
        }
        else if (waterQualityDropdown.value == 1)
        {
            waterScript.waterQuality = WaterQuality.Medium;
        }
        else
        {
            waterScript.waterQuality = WaterQuality.High;
        }
    }

    public void OpenPanel(int panel)
    {
        panels[panel].SetActive(true);
        CloseAllPanels();
    }

    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[5].SetActive(true);
    }
}
