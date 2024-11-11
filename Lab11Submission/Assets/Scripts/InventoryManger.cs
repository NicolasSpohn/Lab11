using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private string inventoryItemName;
    [SerializeField] private int inventoryItemID;

    private List<string> inventoryNames = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };

    private List<InventoryItem> inventoryList = new List<InventoryItem>();

    private void Start()
    {
        InitializeInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LinearSearchByName(inventoryList, inventoryItemName.ToLower());
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            int result = BinarySearchByID(inventoryList, inventoryItemID);
            if (result > -1)
            {
                Debug.Log($"Target ID, {inventoryItemID}, found with name of {inventoryList[result].itemName} and value of {inventoryList[result].value}");
            }
            else
            {
                Debug.Log("Nothing found.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            QuickSortByValue(inventoryList, 0, inventoryList.Count - 1);

            for (int i = 0; i < inventoryList.Count - 1; i++)
            {
                Debug.Log("ID: " + inventoryList[i].id + "\nValue: " + inventoryList[i].value + "\nName: " + inventoryList[i].itemName);
            }

        }
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventoryNames.Count; i++)
        {
            int randomID = Random.Range(0, 50);
            int randomValue = Random.Range(0, 100);

            for (int j = 0; j < inventoryList.Count; j++)
            {
                if (inventoryList[j].id == randomID)
                {
                    randomID = Random.Range(0, 73);
                    j = 0;
                }

                else if (inventoryList[j].value == randomValue)
                {
                    randomValue = Random.Range(0, 100);
                    j = 0;
                }
            }

            InventoryItem item = new InventoryItem(randomID, inventoryNames[i], randomValue);

            inventoryList.Add(item);
        }
    }

    private void LinearSearchByName(List<InventoryItem> list, string target)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].itemName == target)
            {
                Debug.Log("{target} found in the inventory with an ID of {list[i].ID} and a value of {list[i].value}.");
                return;
            }
        }

        Debug.Log("{target} not found.");
    }

    static int BinarySearchByID(List<InventoryItem> list, int target)
    {
        list.Sort((x, y) => x.id.CompareTo(y.id));

        int l = 0;
        int r = list.Count - 1;

        while (l <= r)
        {
            int mid = l + (r - l) / 2;

            if (list[mid].id == target)
            {
                return mid;
            }
            else if (list[mid].id < target)
            {
                l = mid + 1;
            }
            else
            {
                r = mid - 1;
            }
        }

        return -1;
    }

    public void QuickSortByValue(List<InventoryItem> list, int first, int last)
    {
        if (first < last)
        {
            int pivot = divide(list, first, last);

            QuickSortByValue(list, first, pivot - 1);
            QuickSortByValue(list, pivot + 1, last);

        }
    }

    public int divide(List<InventoryItem> list, int first, int last)
    {
        InventoryItem pivotItem = list[last];
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            if (list[element].value < pivotItem.value)
            {
                smaller++;

                InventoryItem temporary = list[smaller];
                list[smaller] = list[element];
                list[element] = temporary;
            }
        }

        InventoryItem temporaryNext = list[smaller + 1];
        list[smaller + 1] = list[last];
        list[last] = temporaryNext;

        return smaller + 1;
    }
}
