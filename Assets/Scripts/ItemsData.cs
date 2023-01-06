using RedScarf.EasyCSV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public static ItemsData Instance;

    public TextAsset Table;

    public static List<ItemFromCSV> GetList = new List<ItemFromCSV>();

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        CsvHelper.Init(';');
        var table = CsvHelper.Create(Table.name, Table.text);

        for (int i = 1; i < table.RowCount; i++)
        {
            GetList.Add(new ItemFromCSV(
                table.Read(i, 0),
                table.Read(i, 1),
                table.Read(i, 2),
                int.Parse(table.Read(i, 3)),
                int.Parse(table.Read(i, 4)),
                int.Parse(table.Read(i, 5)),
                table.Read(i, 6),
                table.Read(i, 7)));

            ParamsManager.GetInventory.Add(new InventoryItem(table.Read(i, 1), 0, false));
        }
        ParamsManager.GetInventory[0].Avaliable = true;

        for (int i = 1; i <= 20; i++)
        {
            ParamsManager.GetPlantBeds.Add(new PlantBed(i, 0, "Null", new System.DateTime()));
        }
    }

    public ItemFromCSV ItemDataByName(string name)
    {
        return GetList.Find(x => x.Name == name);
    }

    public ItemFromCSV SeedNameByPlantName(string name)
    {
        return GetList.Find(x => x.GrowPlant == name);
    }

    private void LoadSaveData()
    {

    }
}

public class ItemFromCSV
{
    private string type;
    private string name;
    private string icon;
    private int sellPrice;
    private int buyPrice;
    private int growTime;
    private string growPlant;
    private string opens;

    public ItemFromCSV(string type, string name, string icon, int sellPrice, int buyPrice, int growTime, string growPlant, string opens)
    {
        this.type = type;
        this.name = name;
        this.icon = icon;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
        this.growTime = growTime;
        this.growPlant = growPlant;
        this.opens = opens;
    }

    public string Type { get => type; }
    public string Name { get => name; }
    public string Icon { get => icon; }
    public int SellPrice { get => sellPrice; }
    public int BuyPrice { get => buyPrice; }
    public int GrowTime { get => growTime; }
    public string GrowPlant { get => growPlant; }
    public string Opens { get => opens; }
}
