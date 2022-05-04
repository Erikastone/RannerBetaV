using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Translator : MonoBehaviour
{
    private static int LanguageId;
    private static List<TranslatebleText> listId = new List<TranslatebleText>();
    #region Весь текст [двумерный массив]
    private static string[,] LineText =
    {
#region Английский
        {
            "Lily", //0
            "John", //1
            "Christophe", //2
            "Mr. Smith", //3
            "Kevin", //4
            "Graphics quality", //5
            "Sound volume", //6
            "Sound on/off", //7
            "Unlock all the unique ZOMBIE icons!!!", //8
            "In the next update, you will find a zombie city map with new events!!!", //9
            "Language" //10
        },
        #endregion
#region   Русский
        {
          "Лилия", //0
          "Джон", //1
          "Кристоф", //2
          "Мистер Смит", //3
          "Кевин", //4
          "Качество графики", //5
          "Громкость звука", //6
          "Звук вкл/выкл", //7
          "Откройте все уникальные иконки ЗОМБИ!!!", //8
          "В следующем обновлении вас ждет карта города зомби с новыми ивентами!!!", //9
          "Язык" //10
        },
        #endregion
#region  Китайский урощенный
        {
            "莉莉", //0
            "约翰", //1
            "克里斯托夫", //2
            "史密斯先生", //3
            "凯文", //4
            "图形质量", //5
            "音量", //6
            "声音开/关", //7
            "解锁所有独特的僵尸图标！!!", //8
            "在下一次更新中，你会发现一个带有新事件的僵尸城市地图！!!", //9
            "语言" //10

        },
#endregion
    };
    #endregion
    static public void Select_language (int id)
    {
        LanguageId = id;
        Update_texts();
    }
    static public string Get_text (int textKey)
    {
        return LineText[LanguageId, textKey];
    }

    static public void Add(TranslatebleText idtext)
    {
        listId.Add(idtext);
    }

    static public void Delete(TranslatebleText idtext)
    {
        listId.Remove(idtext);
    }

    static public  void Update_texts()
    {
        for (int i = 0; i <listId.Count; i++)
        {
            listId[i].UIText.text = LineText[LanguageId, listId[i].textID];
            if (PlayerPrefs.GetInt("Language") == 1) listId[i].UIText.font = Resources.Load<Font>("Ru_font");
            else if (PlayerPrefs.GetInt("Language") == 2) listId[i].UIText.font = Resources.Load<Font>("CH_font");
            else listId[i].UIText.font = Resources.Load<Font>("EN_font");
        }
    }
}
