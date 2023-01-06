using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuManager : MonoBehaviour
{
    public static InventoryMenuManager Instance;

    public GameObject InventoryItemPrefab;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        for (int i = 0; i < ItemsData.GetList.Count; i++)
        {
            if (ParamsManager.GetInventory[i].Count > 0)
            {
                InventoryItemPrefab.GetComponent<InventoryItemController>().Name = ParamsManager.GetInventory[i].Name;
                InventoryItemPrefab.GetComponent<InventoryItemController>().Count = ParamsManager.GetInventory[i].Count;
                InventoryItemPrefab.GetComponent<InventoryItemController>().Icon = ItemsData.GetList[i].Icon;

                if (ItemsData.GetList[i].Type == "Seed") { Instantiate(InventoryItemPrefab, transform.Find("InventoryViewSeeds").Find("Viewport").Find("Content")); }
                if (ItemsData.GetList[i].Type == "Plant") { Instantiate(InventoryItemPrefab, transform.Find("InventoryViewPlants").Find("Viewport").Find("Content")); }
                if (ItemsData.GetList[i].Type == "Potion") { Instantiate(InventoryItemPrefab, transform.Find("InventoryViewPotions").Find("Viewport").Find("Content")); }
            }
        }
    }

    private void DestroyInventoryItems()
    {
        List<GameObject> itemsToRemove = new List<GameObject>();

        for (int i = 0; i < transform.Find("InventoryViewSeeds").Find("Viewport").Find("Content").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("InventoryViewSeeds").Find("Viewport").Find("Content").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("InventoryViewPlants").Find("Viewport").Find("Content").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("InventoryViewPlants").Find("Viewport").Find("Content").GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.Find("InventoryViewPotions").Find("Viewport").Find("Content").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("InventoryViewPotions").Find("Viewport").Find("Content").GetChild(i).gameObject);
        }

        foreach (GameObject item in itemsToRemove)
        {
            Destroy(item);
        }
    }

    public void CloseInventory()
    {
        if (transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable == true) { OpenSeeds(); }

        DestroyInventoryItems();

        GameObject.Find("Inventory").SetActive(false);
    }

    public void OpenSeeds()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = false;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = true;

        transform.Find("InventoryViewSeeds").gameObject.SetActive(true);
        if (transform.Find("InventoryViewPlants").gameObject.activeSelf) { transform.Find("InventoryViewPlants").gameObject.SetActive(false); }
        if (transform.Find("InventoryViewPotions").gameObject.activeSelf) { transform.Find("InventoryViewPotions").gameObject.SetActive(false); }
    }

    public void OpenPlants()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = false;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = true;

        transform.Find("InventoryViewPlants").gameObject.SetActive(true);
        if (transform.Find("InventoryViewSeeds").gameObject.activeSelf) { transform.Find("InventoryViewSeeds").gameObject.SetActive(false); }
        if (transform.Find("InventoryViewPotions").gameObject.activeSelf) { transform.Find("InventoryViewPotions").gameObject.SetActive(false); }
    }

    public void OpenPotions()
    {
        transform.Find("TypeSelect").Find("ButtonSeeds").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPlants").GetComponent<Button>().interactable = true;
        transform.Find("TypeSelect").Find("ButtonPotions").GetComponent<Button>().interactable = false;

        transform.Find("InventoryViewPotions").gameObject.SetActive(true);
        if (transform.Find("InventoryViewPlants").gameObject.activeSelf) { transform.Find("InventoryViewPlants").gameObject.SetActive(false); }
        if (transform.Find("InventoryViewSeeds").gameObject.activeSelf) { transform.Find("InventoryViewSeeds").gameObject.SetActive(false); }
    }
}
