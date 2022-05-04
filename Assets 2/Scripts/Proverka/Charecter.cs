using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charecter : MonoBehaviour
{
    SelectCharecter.Data data = new SelectCharecter.Data();

    private int i;
    public GameObject[] AllCharacters;

    private void Start()
    {
        data = JsonUtility.FromJson<SelectCharecter.Data>(PlayerPrefs.GetString("SaveGame"));
        StartCoroutine(LoadCharacter());
    }

    public IEnumerator LoadCharacter()
    {
        i = 0;
        while (AllCharacters[i].name != data.currentCharacter)
        {
            i++;
        }
        AllCharacters[i].SetActive(true);
        yield return null;
    }
}
 