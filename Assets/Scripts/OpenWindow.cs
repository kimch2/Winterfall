using UnityEngine;
using System.Collections;

public class OpenWindow : MonoBehaviour {

    public GameObject[] windows;

    public void Switch (int window)
    {
        if (window == 1)
        {
            foreach (GameObject panel in windows)
            {
                panel.SetActive(false);
            }
            windows[0].SetActive(true);
        }
        else if (window == 2)
        {
            foreach (GameObject panel in windows)
            {
                panel.SetActive(false);
            }
            windows[1].SetActive(true);
        }
        else if (window == 3)
        {
            foreach (GameObject panel in windows)
            {
                panel.SetActive(false);
            }
            windows[2].SetActive(true);
        }
    }
}
