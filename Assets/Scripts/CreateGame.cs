using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{
    public Text loadingText;
    public Dropdown mapDropdown;
    public Dropdown gamemodeDropdown;

    private int gamemode;
    private int scene;

    public void Load()
    {
        PlayerPrefs.SetInt("gamemode", gamemodeDropdown.value);
        scene = mapDropdown.value + 1;
        loadingText.text = "LOADING...";
        SceneManager.LoadScene(scene);
    }
}
