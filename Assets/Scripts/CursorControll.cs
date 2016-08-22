using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorControll : MonoBehaviour {

    private static FirstPersonController fpc;

    private void Start()
    {
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    public static void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        fpc.enabled = true;
    }

    public static void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        fpc.enabled = false;
    }
}
