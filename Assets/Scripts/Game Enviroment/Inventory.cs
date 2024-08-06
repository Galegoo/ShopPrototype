using UnityEngine;
using TMPro;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static int money;
    public int startMoney;
    public GameObject[] inventoryObjs;
    public GameObject[] inventoryItensPrefabs;
    public int inventoryMaxCapacity = 12;
    int inventoryActualAmount = 2;


    void Start()
    {
        money = startMoney;
    }

    public bool AddItemToInventorry(GameObject item)
    {
        if (inventoryActualAmount <= inventoryMaxCapacity)
        {
            inventoryActualAmount++;
            ArrayManagement.AddElementToArray(ref inventoryObjs);
            inventoryObjs[inventoryObjs.Length - 1] = item;
            return true;
        }
        else
            return false;

    }

    public void RemoveItemFromInventory(int index)
    {
        ArrayManagement.RemoveElementFromArray(ref inventoryObjs, index);
        Destroy(NPCUIController.SelectedIcon);
        inventoryActualAmount--;
    }

    // Métodos de ordenação
    public void SortByName()
    {
        inventoryObjs = inventoryObjs.OrderBy(item => item.GetComponent<ItemIcons>().GetName()).ToArray();
    }

    public void SortByPrice()
    {
        inventoryObjs = inventoryObjs.OrderByDescending(item => item.GetComponent<ItemIcons>().GetPrice()).ToArray();
    }
}

