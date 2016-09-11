using UnityEngine;
using System.Collections;

public class OpenPage : MonoBehaviour {

	public void Open (string url) {
        Application.OpenURL(url);
    }
}
