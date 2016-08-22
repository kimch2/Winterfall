using UnityEngine;
using System.Collections;

public class HideHUD : MonoBehaviour {

    public GameObject[] GO;

    bool hud = true;

	void Update () {
        if (Input.GetButtonDown("HideHUD"))
        {
            if(hud)
            {
                foreach (GameObject go in GO)
                {
                    go.SetActive(false);
                }
                hud = false;
            }
            else
            {
                foreach (GameObject go in GO)
                {
                    go.SetActive(true);
                }
                hud = true;
            }
        }
    }
}
