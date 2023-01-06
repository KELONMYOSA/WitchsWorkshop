using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantBedController : MonoBehaviour
{
    private int ID;
    private TimeSpan RemainingTime = new TimeSpan();
    private DateTime ReadyTime = new DateTime();
    private TimeSpan GrowTime = new TimeSpan();

    private void Start()
    {
        ID = int.Parse(transform.name.Substring(transform.name.IndexOf("_") + 1));
    }

    private void Update()
    {
        UpdateRemainingTime();
        UpdateButtonCondition();
    }

    public void Click()
    {
        if (ParamsManager.Instance.PlantBedById(ID).Status == 0)
        {
            PlantsMenuManager.BedId = ID;
            if (PlantsMenuManager.Instance != null) { PlantsMenuManager.Instance.LoadData(); }
            if (!GameObject.Find("Background").transform.Find("PlantsMenu").gameObject.activeSelf) { GameObject.Find("Background").transform.Find("PlantsMenu").gameObject.SetActive(true); }
        }
        if (ParamsManager.Instance.PlantBedById(ID).Status == 1)
        {
            PlantStatsManager.BedId = ID;
            if (GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("ButtonReady").gameObject.activeSelf)
            {
                GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").gameObject.SetActive(true);
                GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("ButtonReady").gameObject.SetActive(false);
            }
            GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Name").GetComponent<Text>().text = ParamsManager.Instance.PlantBedById(ID).CurrentPlant;
            GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(ItemsData.Instance.ItemDataByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Icon);
            GameObject.Find("Background").transform.Find("PlantStats").gameObject.SetActive(true);
        }
        if (ParamsManager.Instance.PlantBedById(ID).Status == 2)
        {
            CollectPlant();
        }
    }

    private void UpdateRemainingTime()
    {
        if (ParamsManager.Instance.PlantBedById(ID).Status == 1)
        {
            GrowTime = TimeSpan.FromSeconds(ItemsData.Instance.ItemDataByName(ItemsData.Instance.SeedNameByPlantName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Name).GrowTime);
            ReadyTime = ParamsManager.Instance.PlantBedById(ID).PlantingTime + GrowTime;

            RemainingTime = ReadyTime - DateTime.Now;

            if (PlantStatsManager.BedId == ID)
            {
                string stringTimer = RemainingTime.Seconds.ToString() + "s";
                if (RemainingTime.Minutes > 0) { stringTimer = RemainingTime.Minutes.ToString() + "m " + stringTimer; }
                if (RemainingTime.Hours > 0) { stringTimer = RemainingTime.Hours.ToString() + "h " + stringTimer; }
                if (RemainingTime.Days > 0) { stringTimer = RemainingTime.Days.ToString() + "d " + stringTimer; }

                GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").Find("Fill Area").Find("Time").GetComponent<Text>().text = stringTimer;
                GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").GetComponent<Slider>().value = ((float)GrowTime.TotalSeconds - (float)RemainingTime.TotalSeconds) / (float)GrowTime.TotalSeconds;
                
                if (RemainingTime.TotalSeconds < 0)
                {
                    PlantStatsManager.ShowReadyButton();
                }
            }

            if (RemainingTime.TotalSeconds < 0)
            { 
                ParamsManager.Instance.PlantBedById(ID).Status = 2;
                RemainingTime = new TimeSpan();
            }
        }
    }

    private void UpdateButtonCondition()
    {
        if (ParamsManager.Instance.PlantBedById(ID).Status == 0)
        {
            GameObject.Find("Bed_" + ID).GetComponent<Button>().image.color = Color.white;
        }
        if (ParamsManager.Instance.PlantBedById(ID).Status == 1)
        {
            if (RemainingTime.TotalSeconds > GrowTime.TotalSeconds / 2)
            {
                GameObject.Find("Bed_" + ID).GetComponent<Button>().image.color = Color.yellow;
            }
            else
            {
                GameObject.Find("Bed_" + ID).GetComponent<Button>().image.color = Color.blue;
            }
        }
        if (ParamsManager.Instance.PlantBedById(ID).Status == 2)
        {
            GameObject.Find("Bed_" + ID).GetComponent<Button>().image.color = Color.green;
        }
    }

    public void CollectPlant()
    {
        ParamsManager.Instance.PlantBedById(ID).Status = 0;
        ParamsManager.Instance.InventoryItemByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Count = ParamsManager.Instance.InventoryItemByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Count + 1;
        ParamsManager.Instance.PlantBedById(ID).PlantingTime = new DateTime();
        if (ParamsManager.Instance.InventoryItemByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Avaliable == false) { ParamsManager.Instance.InventoryItemByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Avaliable = true; }
        if (ItemsData.Instance.ItemDataByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Opens != "Null") { ParamsManager.Instance.InventoryItemByName(ItemsData.Instance.ItemDataByName(ParamsManager.Instance.PlantBedById(ID).CurrentPlant).Opens).Avaliable = true; }
        ParamsManager.Instance.PlantBedById(ID).CurrentPlant = "Null";

        ReadyTime = new DateTime();
        GrowTime = new TimeSpan();
    }
}
