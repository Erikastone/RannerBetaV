using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
   public int coins;

   // [SerializeField] private int coins;
    [SerializeField] private Text coinsText;

    private void Start()
    {
       if (PlayerPrefs.HasKey("coins"))
            coins = LoadCoins();


        //coins = PlayerPrefs.GetInt("coins");
        coinsText.text = coins.ToString();
    }

   

    public void BuyItem(ItemInfo item)
    {
        if (coins >= item.ItemCost)
        {
            item.ItemCount++;
            item.ButtonText.text = "Bought";
            item.Button.interactable = false;
            item.Save();
            coins -= item.ItemCost;
            Debug.Log($"������� {item.ItemName} ������!");
            SaveCoins();
        }
        else
        {
            Debug.Log($"�� ������� {item.ItemCost - coins} �����.");
        }
    }
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", coins);
    }
    public int LoadCoins()
    {
        return PlayerPrefs.GetInt("coins");
    }
}
