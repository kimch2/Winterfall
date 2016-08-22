using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    public GameObject crosshair;
    public Settings settingsScript;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menu.activeSelf)
            {
                CursorControll.LockCursor();
                Time.timeScale = 1;
                menu.SetActive(false);
                crosshair.SetActive(true);
            }
            else
            {
                CursorControll.UnlockCursor();
                Time.timeScale = 0;
                menu.SetActive(true);
                crosshair.SetActive(false);
                if (settingsScript.settingsMenu.activeSelf)
                {
                    settingsScript.settingsMenu.SetActive(false);
                    settingsScript.videoPanel.SetActive(false);
                    settingsScript.audioPanel.SetActive(false);
                    settingsScript.controlsPanel.SetActive(false);
                    settingsScript.listPanel.SetActive(true);
                }
            }
        }
    }

    public void Settings () {
        settingsScript.settingsMenu.SetActive(true);
        menu.SetActive(false);
	}

    public void Resume()
    {
        menu.SetActive(false);
        settingsScript.settingsMenu.SetActive(false);
        crosshair.SetActive(true);
        CursorControll.LockCursor();
        Time.timeScale = 1;
    }
}
