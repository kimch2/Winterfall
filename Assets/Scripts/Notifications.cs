using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{

    private static Text theText;

    void Start()
    {
        theText = GetComponent<Text>();
    }

    public static IEnumerator Call(string call)
    {
        theText.text = call;
        theText.CrossFadeAlpha(1, .3f, false);
        yield return new WaitForSeconds(5);
        theText.CrossFadeAlpha(0, 2, false);
    }
}
