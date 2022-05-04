using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharecter : MonoBehaviour
{
    SelectCharecter.Data data = new SelectCharecter.Data();

    private int i;
    
    public GameObject[] AllCharacters;

    public GameObject ArrowToLeft;
    public GameObject ArrowToRight;

    public GameObject ButtonCharacter;
    public GameObject ButtonSelectCharacter;
    public GameObject TextSelectCharacter;

    private string statusCheck;
    private int check;

    public Text TextPrice;

    [System.Serializable]
    public class Data
    {
        public string currentCharacter = "TT_demo_female";
        public List<string> haveCharecters = new List<string> { "TT_demo_female" };
        public int money;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            data = JsonUtility.FromJson<SelectCharecter.Data>(PlayerPrefs.GetString("SaveGame"));
        }
        else
        {
            data.money = 1000;
            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));
        }

        AllCharacters[i].SetActive(true);

        if (data.currentCharacter == AllCharacters[i].name)
        {
            ButtonCharacter.SetActive(false);
            ButtonSelectCharacter.SetActive(false);
            TextSelectCharacter.SetActive(true);
        }
        else if (data.currentCharacter != AllCharacters[i].name)
        {
            StartCoroutine(CheckHaveCharacter());
        }


        if (i > 0)
        {
            ArrowToLeft.SetActive(true);
        }
        if(i +1== AllCharacters.Length)
        {
            ArrowToRight.SetActive(false);
        }
    }

    public IEnumerator CheckHaveCharacter()
    {
        while (statusCheck != "Check")
        {
            if (data.haveCharecters.Count != check)
            {
                if (AllCharacters[i].name != data.haveCharecters[check])
                {
                    check++;
                }
                else if (AllCharacters[i].name == data.haveCharecters[check])
                {
                    TextSelectCharacter.SetActive(false);
                    ButtonCharacter.SetActive(false);
                    ButtonSelectCharacter.SetActive(true);
                    check = 0;
                    statusCheck = "Check";
                }

            }
            else if (data.haveCharecters.Count == check)
            {
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(false);
                ButtonCharacter.SetActive(true);
                TextPrice.text = AllCharacters[i].GetComponent<Item>().priceCharacter.ToString();
                check = 0;
                statusCheck = "Check";
            }

        }
        statusCheck = "";

        yield return null;

    }

    public void ArrowRight()
    {
        if(i< AllCharacters.Length)
        {
            if (i == 0)
            {
                ArrowToLeft.SetActive(true);
            }

            AllCharacters[i].SetActive(false);
            i++;
            AllCharacters[i].SetActive(true);

            if (data.currentCharacter == AllCharacters[i].name)
            {
                ButtonCharacter.SetActive(false);
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(true);
            }
            else if (data.currentCharacter != AllCharacters[i].name)
            {
                StartCoroutine(CheckHaveCharacter());
            }

            if (i +1==AllCharacters.Length)
            {
                ArrowToRight.SetActive(false);
            }
        }
    }

    public void ArrowLeft()
    {
        if (i < AllCharacters.Length)
        {
            AllCharacters[i].SetActive(false);
            i--;
            AllCharacters[i].SetActive(true);
            ArrowToRight.SetActive(true);

            if (data.currentCharacter == AllCharacters[i].name)
            {
                ButtonCharacter.SetActive(false);
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(true);
            }
            else if (data.currentCharacter != AllCharacters[i].name)
            {
                StartCoroutine(CheckHaveCharacter());
            }
            if (i == 0)
            {
                ArrowToLeft.SetActive(false);
            }                    
        }
    }

    public void SelectCharacter()
    {
        data = JsonUtility.FromJson<SelectCharecter.Data>(PlayerPrefs.GetString("SaveGame"));
        data.currentCharacter = AllCharacters[i].name;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));
        ButtonSelectCharacter.SetActive(false);
        TextSelectCharacter.SetActive(true);
    }

    public void BuyCharacter()
    {
        if(data.money >= AllCharacters[i].GetComponent<Item>().priceCharacter)
        {
            data = JsonUtility.FromJson<SelectCharecter.Data>(PlayerPrefs.GetString("SaveGame"));
            data.money = data.money - AllCharacters[i].GetComponent<Item>().priceCharacter;
            data.haveCharecters.Add( AllCharacters[i].name);

            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));

            ButtonCharacter.SetActive(false);
            ButtonSelectCharacter.SetActive(true);
        }
        
    } 

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }

}
