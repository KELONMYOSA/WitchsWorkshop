using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ParamsManager : MonoBehaviour
{
    public static ParamsManager Instance;

    public int Money;
    public static List<InventoryItem> GetInventory = new List<InventoryItem>();
    public static List<PlantBed> GetPlantBeds = new List<PlantBed>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Money = 100;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Loading") { GameObject.Find("CurrentMoneyText").GetComponent<Text>().text = Money.ToString(); }
    }

    public InventoryItem InventoryItemByName(string name)
    {
        return GetInventory.Find(x => x.Name == name);
    }

    public PlantBed PlantBedById(int id)
    {
        return GetPlantBeds.Find(x => x.Id == id);
    }
}

public class InventoryItem
{
    private string name;
    private int count;
    private bool avaliable;

    public InventoryItem(string name, int count, bool avaliable)
    {
        this.name = name;
        this.count = count;
        this.avaliable = avaliable;
    }

    public string Name { get => name; }
    public int Count { get => count; set => count = value; }
    public bool Avaliable { get => avaliable; set => avaliable = value; }
}

public class PlantBed
{
    private int id;
    private int status;
    private string currentPlant;
    private DateTime plantingTime;

    public PlantBed(int id, int status, string currentPlant, DateTime plantingTime)
    {
        this.id = id;
        this.status = status;
        this.currentPlant = currentPlant;
        this.plantingTime = plantingTime;
    }

    public int Id { get => id; }
    public int Status { get => status; set => status = value; }
    public string CurrentPlant { get => currentPlant; set => currentPlant = value; }
    public DateTime PlantingTime { get => plantingTime; set => plantingTime = value; }
}
