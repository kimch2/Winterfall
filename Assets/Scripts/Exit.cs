using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Exit : MonoBehaviour {

    public GameObject menu;
    public bool isMenu;

	public void Quit () {
        Application.Quit();
    }

    public void CloseWindow()
    {
        menu.SetActive(false);
    }

    public void Ask()
    {
        menu.SetActive(true);
    }

    void Update () {
	    if(Input.GetButtonDown("Cancel") && isMenu)
        {
            if(menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
	}
}
