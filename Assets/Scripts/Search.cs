using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        foreach (Transform child in inventory)
        {
            child.gameObject.SetActive(false);
        }
        GameObject item = inventory.transform.Find(arg0.ToLower()).gameObject;
        item.gameObject.SetActive(true);
        Debug.Log("Found " + item.name + ".");
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
