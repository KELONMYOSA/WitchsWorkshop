using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void OpenHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void OpenGarden()
    {
        SceneManager.LoadScene("Garden");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OpenCauldron()
    {
        SceneManager.LoadScene("Cauldron");
    }

    public void OpenInventory()
    {
        if (InventoryMenuManager.Instance != null && !GameObject.Find("Background").transform.Find("Inventory").gameObject.activeSelf) { InventoryMenuManager.Instance.LoadData(); }

        if (!GameObject.Find("Background").transform.Find("Inventory").gameObject.activeSelf) { GameObject.Find("Background").transform.Find("Inventory").gameObject.SetActive(true); }
        else { InventoryMenuManager.Instance.CloseInventory(); }
    }

    public void OpenShopMenu()
    {
        if (ShopMenuManager.Instance != null) { ShopMenuManager.Instance.LoadSellData(); }
        if (!GameObject.Find("Background").transform.Find("ShopMenu").gameObject.activeSelf) { GameObject.Find("Background").transform.Find("ShopMenu").gameObject.SetActive(true); }
    }
}
