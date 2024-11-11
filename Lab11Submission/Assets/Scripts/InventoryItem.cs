using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int id;
    public string itemName;
    public int value;

    public InventoryItem(int id, string itemName, int value)
    {
        this.id = id;
        this.itemName = itemName;
        this.value = value;
    }
}