using UnityEngine;
using System.Collections;

public class SaveLoad : MonoBehaviour {

    public Inventory invScript;

    public void Save()
    {
        //Player
        PlayerPrefsX.SetVector3("PlayerPosition", transform.position);
        //PlayerPrefsX.SetVector3("PlayerRotation", transform.eulerAngles);

        //Inventory
        /*
        PlayerPrefs.SetInt("Logs", invScript.logs);
        PlayerPrefs.SetInt("Stone", invScript.stone);
        PlayerPrefs.SetInt("Flint", invScript.flint);
        PlayerPrefs.SetInt("Metal", invScript.metal);
        PlayerPrefs.SetInt("DuctTape", invScript.ductTape);
        PlayerPrefs.SetInt("Planks", invScript.planks);
        PlayerPrefs.SetInt("Can", invScript.can);
        PlayerPrefs.SetInt("Canteen", invScript.canteen);
        PlayerPrefs.SetInt("IronOre", invScript.ironOre);
        PlayerPrefs.SetInt("Apple", invScript.apple);
        PlayerPrefs.SetInt("Pills", invScript.pills);
        PlayerPrefs.SetInt("CannedBeans", invScript.cannedBeans);
        PlayerPrefs.SetInt("CanteenWater", invScript.canteenWater);
        */

        foreach (Inventory.ItemsClass item in invScript.items)
        {
            PlayerPrefs.SetInt("Item-" + item.name, item.item);
            Debug.Log("Save Item-" + item.name + " - " + item.item);
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
