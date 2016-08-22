using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Settings : MonoBehaviour {

    [Header("Scripts")]
    public CameraMotionBlur motionBlurScript;
    public SSAOPro ssaoScript;
    public TOD_Rays raysScript;
    public TOD_Shadows shadowsScript;
    public Antialiasing aa;

    [Header("Objects")]
    public Dropdown qualitySettingsDropdown;
    public Dropdown renderDistanceDropdown;
    public GameObject settingsMenu;
    public Slider volumeSlider;
    public GameObject fps;
    public GameObject menu;
    public GameObject videoPanel;
    public GameObject audioPanel;
    public GameObject controlsPanel;
    public GameObject listPanel;

    public Toggle motionBlurToggle;
    public Toggle showFPSToggle;
    public Toggle SSAOToggle;
    public Toggle sunRaysToggle;
    public Toggle cloudShadowsToggle;
    public Toggle antiAliasingToggle;
    public Toggle fullscreenToggle;

    Camera mainCam;

    public void VideoSettings ()
    {
        videoPanel.SetActive(true);
        listPanel.SetActive(false);
    }

    public void AudioSettings()
    {
        audioPanel.SetActive(true);
        listPanel.SetActive(false);
    }

    public void ControlsSettings()
    {
        controlsPanel.SetActive(true);
        listPanel.SetActive(false);
    }

    public void UpdateVolume ()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void Close ()
    {
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
        controlsPanel.SetActive(false);
        listPanel.SetActive(true);
        settingsMenu.SetActive(false);
        menu.SetActive(true);
    }

	public void MotionBlur ()
    {
	    if(motionBlurScript.enabled)
        {
            motionBlurScript.enabled = false;
        }
        else
        {
            motionBlurScript.enabled = true;
        }
	}

    public void FPS()
    {
        if (fps.activeSelf)
        {
            fps.SetActive(false);
        }
        else
        {
            fps.SetActive(true);
        }
    }

    public void SSAO ()
    {
        if (ssaoScript.enabled)
        {
            ssaoScript.enabled = false;
        }
        else
        {
            ssaoScript.enabled = true;
        }
    }

    public void SunRays()
    {
        if (raysScript.enabled)
        {
            raysScript.enabled = false;
        }
        else
        {
            raysScript.enabled = true;
        }
    }

    public void CloudShadows()
    {

        if (shadowsScript.enabled)
        {
            shadowsScript.enabled = false;
        }
        else
        {
            shadowsScript.enabled = true;
        }
    }

    public void AntiAliasing()
    {
        if (aa.enabled)
        {
            aa.enabled = false;
        }
        else
        {
            aa.enabled = true;
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

    void Start ()
    {
        mainCam = Camera.main;
        //DontDestroyOnLoad(mainCam);
        qualitySettingsDropdown.value = QualitySettings.GetQualityLevel();

        /*
        if (motionBlurToggle.isOn)
            motionBlurScript.enabled = true;
        else
            motionBlurScript.enabled = false;

        if (showFPSToggle.isOn)
            fps.SetActive(true);
        else
            fps.SetActive(false);

        if (SSAOToggle.isOn)
            ssaoScript.enabled = true;
        else
            ssaoScript.enabled = false;

        if (sunRaysToggle.isOn)
            raysScript.enabled = true;
        else
            raysScript.enabled = false;

        if (cloudShadowsToggle.isOn)
            shadowsScript.enabled = true;
        else
            cloudShadowsToggle.enabled = false;

        if (antiAliasingToggle.isOn)
            aa.enabled = true;
        else
            aa.enabled = false;

    */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Fullscreen()
    {
        if (fullscreenToggle.isOn)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }

    public void SetQualityLevel()
    {
        int qualityLevel = qualitySettingsDropdown.value;
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void SetRenderDistance()
    {
        if (renderDistanceDropdown.value == 0)
        {
            mainCam.farClipPlane = 80;
        }
        else if (renderDistanceDropdown.value == 1)
        {
            mainCam.farClipPlane = 150;
        }
        else if(renderDistanceDropdown.value == 2)
        {
            mainCam.farClipPlane = 250;
        }
        else if(renderDistanceDropdown.value == 3)
        {
            mainCam.farClipPlane = 450;
        }
        else if (renderDistanceDropdown.value == 4)
        {
            mainCam.farClipPlane = 600;
        }
        else if (renderDistanceDropdown.value == 5)
        {
            mainCam.farClipPlane = 800;
        }
        else if (renderDistanceDropdown.value == 6)
        {
            mainCam.farClipPlane = 1000;
        }
        else if (renderDistanceDropdown.value == 7)
        {
            mainCam.farClipPlane = 1500;
        }
        else if (renderDistanceDropdown.value == 8)
        {
            mainCam.farClipPlane = 2000;
        }
    }
}
