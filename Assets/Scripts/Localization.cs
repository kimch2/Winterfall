using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private string hungerKey = "Hunger";
    public Text hungerText;

    private string currentLanguageKey = "CurrentLanguage";
    public Text currentLanguageText;

    private string englishKey = "English";
    public Text englishText;

    private string slovenianKey = "Slovenian";
    public Text slovenianText;

    private string languageKey = "Language";
    public Text languageText;

    void Start()
    {
        Reload();
    }

    void Reload()
    {
        hungerText.text = LanguageManager.Instance.GetTextValue(hungerKey);
        currentLanguageText.text = LanguageManager.Instance.GetTextValue(currentLanguageKey);
        languageText.text = LanguageManager.Instance.GetTextValue(languageKey);
        slovenianText.text = LanguageManager.Instance.GetTextValue(slovenianKey);
        englishText.text = LanguageManager.Instance.GetTextValue(englishKey);
    }

    public void ChangeLanguage(string lang)
    {
        LanguageManager.Instance.ChangeLanguage(lang);
        Reload();
    }
}