using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour {

    public Inventory inv;
    public Stats statsScript;
    public GameObject inputField;
    private InputField input;

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.T) && !input.isFocused)
        {
            if (Cursor.visible)
            {
                CursorControll.LockCursor();
                inputField.SetActive(false);
                input.DeactivateInputField();
            }
            else
            {
                CursorControll.UnlockCursor();
                inputField.SetActive(true);
                input.Select();
                input.ActivateInputField();
            }
        }
    }

    void Start ()
    {
        input = inputField.GetComponent<InputField>();

        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName (string arg0)
    {
        if (arg0.ToLower() == "give health")
        {
            StartCoroutine(Notifications.Call("+100 Health."));
            statsScript.health += 100;
        }
        else if (arg0.ToLower() == "give hunger")
        {
            StartCoroutine(Notifications.Call("+100 Hunger."));
            statsScript.hunger += 100;
        }
        else if (arg0.ToLower() == "give thirst")
        {
            StartCoroutine(Notifications.Call("+100 Thirst."));
            statsScript.thirst += 100;
        }
        else if (arg0.ToLower() == "give energy")
        {
            StartCoroutine(Notifications.Call("+100 Energy."));
            statsScript.energy += 100;
        }
        else if (arg0.ToLower() == "give money")
        {
            StartCoroutine(Notifications.Call("+100€."));
            statsScript.money += 100;
        }
        /*
        else if (arg0.ToLower() == "give buildings")
        {
            StartCoroutine(Notifications.Call("+100 of each Building Part."));
            inv.foundation += 100;
            inv.pillar += 100;
            inv.wall += 100;
            inv.doorway += 100;
            inv.door += 100;
            inv.stairs += 100;
            inv.ceiling += 100;
            inv.window += 100;
            inv.craftingTable += 100;
            inv.campfire += 100;
            inv.bed += 100;
        }
        */
        else if (arg0.ToLower() == "give items")
        {
            StartCoroutine(Notifications.Call("+100 of each Item."));
            foreach (Inventory.ItemsClass item in inv.items)
            {
                item.item += 100;
            }
        }
        else if (arg0.ToLower() == "hello")
        {
            StartCoroutine(Notifications.Call("Hello!"));
        }
        else if (arg0.ToLower() == "multiplayer")
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            StartCoroutine(Notifications.Call("Unknown Command!"));
        }
    }
}
