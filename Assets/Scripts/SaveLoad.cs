using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SaveLoad : MonoBehaviour {

    public Inventory invScript;

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }

        /* Player */
        PlayerPrefsX.SetVector3("PlayerPosition", transform.position);
        //PlayerPrefsX.SetVector3("PlayerRotation", transform.eulerAngles);

        /* Items */
        foreach (Inventory.ItemsClass item in invScript.items)
        {
            PlayerPrefs.SetInt("Item-" + item.name, item.item);
        }

        StartCoroutine(Notifications.Call("Saved."));
    }
    
    public void Load()
    {
        //Player
        transform.position = PlayerPrefsX.GetVector3("PlayerPosition");
        //transform.eulerAngles = PlayerPrefsX.GetVector3("PlayerRotation");

        //Inventory
        /*
        invScript.logs = PlayerPrefs.GetInt("Logs");
        invScript.logs = PlayerPrefs.GetInt("Stone");
        invScript.logs = PlayerPrefs.GetInt("Flint");
        invScript.logs = PlayerPrefs.GetInt("Metal");
        */

        foreach (Inventory.ItemsClass item in invScript.items)
        {
            PlayerPrefs.GetInt("Item-" + item.name);
            Debug.Log("Load Item-" + item.name + " - " + item.item);
        }

        StartCoroutine(Notifications.Call("Loaded."));
    }
}
