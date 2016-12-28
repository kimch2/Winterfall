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
                if (settingsScript.panels[5].activeSelf)
                {
                    foreach(GameObject panel in settingsScript.panels)
                    {
                        panel.SetActive(false);
                    }
                    settingsScript.panels[4].SetActive(true);
                }
            }
        }
    }

    public void Settings () {
        settingsScript.panels[5].SetActive(true);
        menu.SetActive(false);
	}

    public void Resume()
    {
        menu.SetActive(false);
        settingsScript.panels[5].SetActive(false);
        crosshair.SetActive(true);
        CursorControll.LockCursor();
        Time.timeScale = 1;
    }
}
