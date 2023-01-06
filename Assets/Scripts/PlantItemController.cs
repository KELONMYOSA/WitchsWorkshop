using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItemController : MonoBehaviour
{
    public string Icon;
    public string Name;
    public string SeedName;
    public int Time;
    private int ID;

    private void Start()
    {
        TimeSpan growTime = TimeSpan.FromSeconds(ItemsData.Instance.ItemDataByName(SeedName).GrowTime);
        string growTimeString = growTime.Seconds.ToString() + "s";
        if (growTime.Minutes > 0) { growTimeString = growTime.Minutes.ToString() + "m " + growTimeString; }
        if (growTime.Hours > 0) { growTimeString = growTime.Hours.ToString() + "h " + growTimeString; }
        if (growTime.Days > 0) { growTimeString = growTime.Days.ToString() + "d " + growTimeString; }

        transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(Icon);
        transform.Find("Name").GetComponent<Text>().text = Name;
        transform.Find("Time").GetComponent<Text>().text = growTimeString;
        ID = PlantsMenuManager.BedId;
    }

    public void SelectClick()
    {
        ParamsManager.Instance.InventoryItemByName(SeedName).Count = ParamsManager.Instance.InventoryItemByName(SeedName).Count - 1;
        ParamsManager.Instance.PlantBedById(ID).Status = 1;
        ParamsManager.Instance.PlantBedById(ID).CurrentPlant = Name;
        ParamsManager.Instance.PlantBedById(ID).PlantingTime = DateTime.Now;
        PlantsMenuManager.Instance.ClosePlantsMenu();
    }
}
