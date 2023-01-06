using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    public static ShopMenuManager Instance;

    public GameObject ItemPrefabSell;
    public GameObject ItemPrefabBuy;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        LoadSellData();
    }

    public void LoadSellData()
    {
        for (int i = 0; i < ItemsData.GetList.Count; i++)
        {
            //Sell
            if (ParamsManager.GetInventory[i].Count > 0)
            {
                ItemPrefabSell.GetComponent<ShopItemController>().Name = ItemsData.GetList[i].Name;
                ItemPrefabSell.GetComponent<ShopItemController>().Icon = ItemsData.GetList[i].Icon;
                ItemPrefabSell.GetComponent<ShopItemController>().Price = ItemsData.GetList[i].SellPrice;
                ItemPrefabSell.GetComponent<ShopItemController>().SellItem = true;
                if (ItemsData.GetList[i].Type == "Seed") { Instantiate(ItemPrefabSell, transform.Find("SellViewSeeds").Find("Viewport").Find("ContentSell")); }
                if (ItemsData.GetList[i].Type == "Plant") { Instantiate(ItemPrefabSell, transform.Find("SellViewPlants").Find("Viewport").Find("ContentSell")); }
                if (ItemsData.GetList[i].Type == "Potion") { Instantiate(ItemPrefabSell, transform.Find("SellViewPotions").Find("Viewport").Find("ContentSell")); }
            }
        }
    }

    public void LoadBuyData()
    {
        for (int i = 0; i < ItemsData.GetList.Count; i++)
        {
            //Buy
            if (ParamsManager.GetInventory[i].Avaliable)
            {
                ItemPrefabBuy.GetComponent<ShopItemController>().Name = ItemsData.GetList[i].Name;
                ItemPrefabBuy.GetComponent<ShopItemController>().Icon = ItemsData.GetList[i].Icon;
                ItemPrefabBuy.GetComponent<ShopItemController>().Price = ItemsData.GetList[i].BuyPrice;
                ItemPrefabBuy.GetComponent<ShopItemController>().SellItem = false;

                if (ItemsData.GetList[i].Type == "Seed") { Instantiate(ItemPrefabBuy, transform.Find("BuyViewSeeds").Find("Viewport").Find("ContentBuy")); }
                if (ItemsData.GetList[i].Type == "Plant") { Instantiate(ItemPrefabBuy, transform.Find("BuyViewPlants").Find("Viewport").Find("ContentBuy")); }
                if (ItemsData.GetList[i].Type == "Potion") { Instantiate(ItemPrefabBuy, transform.Find("BuyViewPotions").Find("Viewport").Find("ContentBuy")); }
            }
        }
    }

    private void DestroySellItems()
    {
        List<GameObject> itemsToRemove = new List<GameObject>();

        for (int i = 0; i < transform.Find("SellViewSeeds").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("SellViewSeeds").Find("Viewport").Find("ContentSell").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("SellViewPlants").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("SellViewPlants").Find("Viewport").Find("ContentSell").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("SellViewPotions").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("SellViewPotions").Find("Viewport").Find("ContentSell").GetChild(i).gameObject);
        }

        foreach (GameObject item in itemsToRemove)
        {
            Destroy(item);
        }
    }

    private void DestroyBuyItems()
    {
        List<GameObject> itemsToRemove = new List<GameObject>();

        for (int i = 0; i < transform.Find("BuyViewSeeds").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("BuyViewSeeds").Find("Viewport").Find("ContentBuy").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("BuyViewPlants").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("BuyViewPlants").Find("Viewport").Find("ContentBuy").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("BuyViewPotions").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("BuyViewPotions").Find("Viewport").Find("ContentBuy").GetChild(i).gameObject);
        }

        foreach (GameObject item in itemsToRemove)
        {
            Destroy(item);
        }
    }

    public void CloseShop()
    {
        if (transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable == true) 
        {
            DestroyBuyItems();

            transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable = false;
            transform.Find("SellOrBuy").Find("ButtonBuy").GetComponent<Button>().interactable = true;

            if (transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable == false)
            {
                transform.Find("BuyViewSeeds").gameObject.SetActive(false);
                transform.Find("SellViewSeeds").gameObject.SetActive(true);
            }

            if (transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable == false)
            {
                transform.Find("BuyViewPlants").gameObject.SetActive(false);
                transform.Find("SellViewPlants").gameObject.SetActive(true);
            }

            if (transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable == false)
            {
                transform.Find("BuyViewPotions").gameObject.SetActive(false);
                transform.Find("SellViewPotions").gameObject.SetActive(true);
            }
        }
        else { DestroySellItems(); }

        if (transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable == true) { OpenSeeds(); }

        GameObject.Find("ShopMenu").SetActive(false);
    }

    public void OpenSell()
    {
        LoadSellData();
        DestroyBuyItems();
        
        transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable = false;
        transform.Find("SellOrBuy").Find("ButtonBuy").GetComponent<Button>().interactable = true;

        if (transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewSeeds").gameObject.SetActive(false);
            transform.Find("SellViewSeeds").gameObject.SetActive(true);
        }

        if (transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewPlants").gameObject.SetActive(false);
            transform.Find("SellViewPlants").gameObject.SetActive(true);
        }

        if (transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewPotions").gameObject.SetActive(false);
            transform.Find("SellViewPotions").gameObject.SetActive(true);
        }

        ResetAllItems();
    }

    public void OpenBuy()
    {
        LoadBuyData();
        DestroySellItems();

        transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable = true;
        transform.Find("SellOrBuy").Find("ButtonBuy").GetComponent<Button>().interactable = false;

        if (transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewSeeds").gameObject.SetActive(true);
            transform.Find("SellViewSeeds").gameObject.SetActive(false);
        }

        if (transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewPlants").gameObject.SetActive(true);
            transform.Find("SellViewPlants").gameObject.SetActive(false);
        }

        if (transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable == false)
        {
            transform.Find("BuyViewPotions").gameObject.SetActive(true);
            transform.Find("SellViewPotions").gameObject.SetActive(false);
        }

        ResetAllItems();
    }

    public void OpenSeeds()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = false;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = true;

        if (transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable == false)
        {
            transform.Find("SellViewSeeds").gameObject.SetActive(true);
            if (transform.Find("SellViewPlants").gameObject.activeSelf) { transform.Find("SellViewPlants").gameObject.SetActive(false); }
            if (transform.Find("SellViewPotions").gameObject.activeSelf) { transform.Find("SellViewPotions").gameObject.SetActive(false); }
        }
        else
        {
            transform.Find("BuyViewSeeds").gameObject.SetActive(true);
            if (transform.Find("BuyViewPlants").gameObject.activeSelf) { transform.Find("BuyViewPlants").gameObject.SetActive(false); }
            if (transform.Find("BuyViewPotions").gameObject.activeSelf) { transform.Find("BuyViewPotions").gameObject.SetActive(false); }
        }
    }

    public void OpenPlants()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = false;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = true;

        if (transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable == false)
        {
            transform.Find("SellViewPlants").gameObject.SetActive(true);
            if (transform.Find("SellViewSeeds").gameObject.activeSelf) { transform.Find("SellViewSeeds").gameObject.SetActive(false); }
            if (transform.Find("SellViewPotions").gameObject.activeSelf) { transform.Find("SellViewPotions").gameObject.SetActive(false); }
        }
        else
        {
            transform.Find("BuyViewPlants").gameObject.SetActive(true);
            if (transform.Find("BuyViewSeeds").gameObject.activeSelf) { transform.Find("BuyViewSeeds").gameObject.SetActive(false); }
            if (transform.Find("BuyViewPotions").gameObject.activeSelf) { transform.Find("BuyViewPotions").gameObject.SetActive(false); }
        }
    }

    public void OpenPotions()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = false;

        if (transform.Find("SellOrBuy").Find("ButtonSell").GetComponent<Button>().interactable == false)
        {
            transform.Find("SellViewPotions").gameObject.SetActive(true);
            if (transform.Find("SellViewPlants").gameObject.activeSelf) { transform.Find("SellViewPlants").gameObject.SetActive(false); }
            if (transform.Find("SellViewSeeds").gameObject.activeSelf) { transform.Find("SellViewSeeds").gameObject.SetActive(false); }
        }
        else
        {
            transform.Find("BuyViewPotions").gameObject.SetActive(true);
            if (transform.Find("BuyViewPlants").gameObject.activeSelf) { transform.Find("BuyViewPlants").gameObject.SetActive(false); }
            if (transform.Find("BuyViewSeeds").gameObject.activeSelf) { transform.Find("BuyViewSeeds").gameObject.SetActive(false); }
        }
    }

    public void ResetAllItems()
    {
        for (int i = 0; i < transform.Find("SellViewSeeds").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            transform.Find("SellViewSeeds").Find("Viewport").Find("ContentSell").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
        for (int i = 0; i < transform.Find("SellViewPlants").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            transform.Find("SellViewPlants").Find("Viewport").Find("ContentSell").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
        for (int i = 0; i < transform.Find("SellViewPotions").Find("Viewport").Find("ContentSell").childCount; i++)
        {
            transform.Find("SellViewPotions").Find("Viewport").Find("ContentSell").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
        for (int i = 0; i < transform.Find("BuyViewSeeds").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            transform.Find("BuyViewSeeds").Find("Viewport").Find("ContentBuy").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
        for (int i = 0; i < transform.Find("BuyViewPlants").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            transform.Find("BuyViewPlants").Find("Viewport").Find("ContentBuy").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
        for (int i = 0; i < transform.Find("BuyViewPotions").Find("Viewport").Find("ContentBuy").childCount; i++)
        {
            transform.Find("BuyViewPotions").Find("Viewport").Find("ContentBuy").GetChild(i).GetComponent<ShopItemController>().ResetItem();
        }
    }
}
