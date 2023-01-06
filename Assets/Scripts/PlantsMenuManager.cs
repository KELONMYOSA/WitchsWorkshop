using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsMenuManager : MonoBehaviour
{
    public static PlantsMenuManager Instance;

    public GameObject PlantItemPrefab;
    public static int BedId;

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
            if (ParamsManager.GetInventory[i].Count > 0 && ItemsData.GetList[i].Type == "Seed")
            {
                PlantItemPrefab.GetComponent<PlantItemController>().SeedName = ItemsData.GetList[i].Name;
                PlantItemPrefab.GetComponent<PlantItemController>().Name = ItemsData.GetList[i].GrowPlant;
                PlantItemPrefab.GetComponent<PlantItemController>().Time = ItemsData.GetList[i].GrowTime;
                PlantItemPrefab.GetComponent<PlantItemController>().Icon = ItemsData.GetList[i].Icon;

                Instantiate(PlantItemPrefab, transform.Find("BackgroundPlantsMenu").Find("PlantsView").Find("Viewport").Find("Content"));
            }
        }
    }

    private void DestroyPlantsMenuItems()
    {
        List<GameObject> itemsToRemove = new List<GameObject>();

        for (int i = 0; i < transform.Find("BackgroundPlantsMenu").Find("PlantsView").Find("Viewport").Find("Content").childCount; i++)
        {
            itemsToRemove.Add(transform.Find("BackgroundPlantsMenu").Find("PlantsView").Find("Viewport").Find("Content").GetChild(i).gameObject);
        }

        foreach (GameObject item in itemsToRemove)
        {
            Destroy(item);
        }
    }

    public void ClosePlantsMenu()
    {
        DestroyPlantsMenuItems();

        GameObject.Find("PlantsMenu").SetActive(false);
    }
}
