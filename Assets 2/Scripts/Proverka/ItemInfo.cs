using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public string ItemName;
    public int ItemCount;
    public int ItemCost;
    [SerializeField] Text ImageText;
    public Text ButtonText;
    public Button Button;

    ShopManager shopManager;

    void Start()
    {
        ButtonText.text = $"Buy for {ItemCost}";
        if (PlayerPrefs.HasKey(ItemName))
            ItemCount = Load();
        ImageText.text = $"{ItemName}";
        if (ItemCount > 0)
        {
            Button.interactable = false;
            ButtonText.text = "Bought";
        }
        shopManager = FindObjectOfType<ShopManager>();
    }

    public int Load()
    {
        return PlayerPrefs.GetInt(ItemName);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ItemName, ItemCount);
        Debug.Log("Сохранение прошло успешно!");
    }

    public void OnClickBuy()
    {
        shopManager.BuyItem(this);
    }
}
