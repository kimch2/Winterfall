using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private string healthKey = "Health";
    public Text healthText;

    private string hungerKey = "Hunger";
    public Text hungerText;

    private string thirstKey = "Thirst";
    public Text thirstText;

    private string energyKey = "Energy";
    public Text energyText;

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
        healthText.text = LanguageManager.Instance.GetTextValue(healthKey);
        hungerText.text = LanguageManager.Instance.GetTextValue(hungerKey);
        thirstText.text = LanguageManager.Instance.GetTextValue(thirstKey);
        energyText.text = LanguageManager.Instance.GetTextValue(energyKey);
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