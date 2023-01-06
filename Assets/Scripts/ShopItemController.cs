using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    public string Icon;
    public string Name;
    public int Price;
    public bool SellItem;

    private int ItemCount = 1;

    private void Start()
    {
        CheckParamsCount();

        transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(Icon);
        transform.Find("Name").GetComponent<Text>().text = Name;
        transform.Find("Price").GetComponent<Text>().text = Price.ToString();
    }

    private void CheckParamsCount()
    {
        if (SellItem)
        {
            if (ParamsManager.Instance.InventoryItemByName(Name).Count < ItemCount + 1)
            {
                transform.Find("ButtonIncrease").GetComponent<Button>().interactable = false;
            }
            else
            {
                if (!transform.Find("ButtonIncrease").GetComponent<Button>().interactable) { transform.Find("ButtonIncrease").GetComponent<Button>().interactable = true; }
            }

            if (ParamsManager.Instance.InventoryItemByName(Name).Count < ItemCount)
            {
                transform.Find("ButtonItemSell").GetComponent<Button>().interactable = false;
                Destroy(transform.gameObject);
            }
            else
            {
                if (!transform.Find("ButtonItemSell").GetComponent<Button>().interactable) { transform.Find("ButtonItemSell").GetComponent<Button>().interactable = true; }
            }
        }
        else
        {
            if (ParamsManager.Instance.Money < (ItemCount + 1) * Price)
            {
                transform.Find("ButtonIncrease").GetComponent<Button>().interactable = false;
            }
            else
            {
                if (!transform.Find("ButtonIncrease").GetComponent<Button>().interactable) { transform.Find("ButtonIncrease").GetComponent<Button>().interactable = true; }
            }
            
            if (ParamsManager.Instance.Money < ItemCount * Price)
            {
                transform.Find("ButtonItemBuy").GetComponent<Button>().interactable = false;
            }
            else 
            {
                if (!transform.Find("ButtonItemBuy").GetComponent<Button>().interactable) { transform.Find("ButtonItemBuy").GetComponent<Button>().interactable = true; } 
            }
        }
    }

    public void IncreaseItemCount()
    {
        ItemCount++;

        CheckParamsCount();
        if (!transform.Find("ButtonDecrease").GetComponent<Button>().interactable) { transform.Find("ButtonDecrease").GetComponent<Button>().interactable = true; }
        
        transform.Find("CountField").GetComponent<Text>().text = ItemCount.ToString();
    }

    public void DecreaseItemCount()
    {
        ItemCount--;

        CheckParamsCount();
        if (ItemCount == 1) { transform.Find("ButtonDecrease").GetComponent<Button>().interactable = false; }

        transform.Find("CountField").GetComponent<Text>().text = ItemCount.ToString();
    }

    public void SellItems()
    {
        ParamsManager.Instance.Money = ParamsManager.Instance.Money + ItemCount * Price;
        ParamsManager.Instance.InventoryItemByName(Name).Count = ParamsManager.Instance.InventoryItemByName(Name).Count - ItemCount;

        ShopMenuManager.Instance.ResetAllItems();
    }

    public void BuyItems()
    {
        ParamsManager.Instance.Money = ParamsManager.Instance.Money - ItemCount * Price;
        ParamsManager.Instance.InventoryItemByName(Name).Count = ParamsManager.Instance.InventoryItemByName(Name).Count + ItemCount;

        ShopMenuManager.Instance.ResetAllItems();
    }

    public void ResetItem()
    {
        ItemCount = 1;
        transform.Find("CountField").GetComponent<Text>().text = ItemCount.ToString();
        transform.Find("ButtonDecrease").GetComponent<Button>().interactable = false;
        CheckParamsCount();
    }
}
