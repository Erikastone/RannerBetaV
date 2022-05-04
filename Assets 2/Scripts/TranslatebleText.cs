using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TranslatebleText : MonoBehaviour
{
    public int textID;

    //[HideInInspector] public TextMeshProUGUI UIText;
    [HideInInspector] public Text UIText;

    private void Awake()
    {
        UIText = GetComponent<Text>();
        Translator.Add(this);
    }

    private void OnEnable()
    {
        Translator.Update_texts();
    }

    private void OnDestroy()
    {
        Translator.Delete(this);
    }
}
