using UnityEngine;
using System.Collections;

public class OpenPage : MonoBehaviour {

    public string url = "http://intercell.winterfall.dx.am/wiki/index.php?title=Winterfall";

	public void Open () {
        Application.OpenURL(url);
    }
}
