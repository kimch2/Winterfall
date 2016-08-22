using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    public Text loadingText;

	public void Load (int scene) {
        loadingText.text = "<b>Loading...</b>";
        SceneManager.LoadScene(scene);
	}
}
