using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public string Icon;
    public string Name;
    public int Count;

    private void Start()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(Icon);
        transform.Find("Name").GetComponent<Text>().text = Name;
        transform.Find("CountField").GetComponent<Text>().text = Count.ToString();
    }
}
