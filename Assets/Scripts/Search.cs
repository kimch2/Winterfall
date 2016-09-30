using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Search : MonoBehaviour
{
    public Transform inventory;
    InputField input;

    void Start()
    {
        input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string arg0)
    {
        GameObject item = inventory.transform.Find(arg0.ToLower()).gameObject;

        if(item != null)
        {
            foreach (Transform child in inventory)
            {
                child.gameObject.SetActive(false);
            }
            item.gameObject.SetActive(true);
        } 
        else
        {
            if(arg0.ToLower() == "water canteen")
            {
                //GameObject itm = inventory.transform.Find("canteen_water").gameObject;
            }
            item.gameObject.SetActive(true);
        }
    }

    public void Clear ()
    {
        foreach (Transform child in inventory)
        {
            child.gameObject.SetActive(true);
            input.text = "";
            Debug.Log("Search Cleared.");
        }
    }
}
