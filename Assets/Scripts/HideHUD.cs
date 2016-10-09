using UnityEngine;
using System.Collections;

public class HideHUD : MonoBehaviour {

    static private GameObject[] GO;
    public GameObject[] GOs;

    static bool hud = true;

    void Start()
    {
        GO = GOs;
    }

	void Update () {
        if (Input.GetButtonDown("HideHUD"))
        {
            if(hud)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public static void Hide ()
    {
        foreach (GameObject go in GO)
        {
            go.SetActive(false);
        }
        hud = false;
    }

    public static void Show()
    {
        foreach (GameObject go in GO)
        {
            go.SetActive(true);
        }
        hud = true;
    }
}
