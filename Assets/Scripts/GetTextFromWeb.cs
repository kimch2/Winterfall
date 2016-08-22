using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetTextFromWeb : MonoBehaviour {

    public Text networkText;

    public string url;

    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        //networkText = GetComponent<Text>();
        networkText.text = www.text;
    }
}
