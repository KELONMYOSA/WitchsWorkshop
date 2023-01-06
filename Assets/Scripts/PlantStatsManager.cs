using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStatsManager : MonoBehaviour
{
    public static int BedId;

    public void ClickReturn()
    {
        GameObject.Find("Background").transform.Find("PlantStats").gameObject.SetActive(false);
        if (ParamsManager.Instance.PlantBedById(BedId).Status == 2)
        {
            GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").gameObject.SetActive(true);
            GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("ButtonReady").gameObject.SetActive(false);
        }
    }

    public static void ShowReadyButton()
    {
        GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").gameObject.SetActive(false);
        GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("ButtonReady").gameObject.SetActive(true);
    }

    public void ClickReady()
    {
        transform.parent.Find("PlantBeds").Find("Bed_" + BedId).GetComponent<PlantBedController>().CollectPlant();
        ClickReturn();
        GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("Progress").gameObject.SetActive(true);
        GameObject.Find("Background").transform.Find("PlantStats").Find("BackgroundStats").Find("ButtonReady").gameObject.SetActive(false);
    }
}
