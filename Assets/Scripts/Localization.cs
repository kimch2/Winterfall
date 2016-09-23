using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    [System.Serializable]
    public class LocalizationClass
    {
        public string name;
        public string key;
        public Text text;
    }

    public LocalizationClass[] localizations;

    void Start()
    {
        Reload();
    }

    void Reload()
    {
        for (int i = 0; i < localizations.Length; i++)
        {
            localizations[i].text.text = LanguageManager.Instance.GetTextValue(localizations[i].key);
        }
    }

    public void ChangeLanguage(string lang)
    {
        LanguageManager.Instance.ChangeLanguage(lang);
        Reload();
    }
}