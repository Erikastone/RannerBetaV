using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Menu : MonoBehaviour
{
    public Text test_text;

    void Start()
    {
        if (PlayerPrefs.HasKey("Language") == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetInt("Language", 1);
            else if (Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional) PlayerPrefs.SetInt("Language", 2
                );
            else PlayerPrefs.SetInt("Language", 0); //angl

        }
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
    }

    public void Language_change(int LanguageID) //cmena Language
    {
        PlayerPrefs.SetInt("Language", LanguageID);
        Translator.Select_language(PlayerPrefs.GetInt("Language"));
    }

    public void Show_text() //vivod po najatiy knopki
    {
        test_text.enabled = true;
        test_text.text = Translator.Get_text(4);
    }
}
