using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform posicoesHolder;
    [SerializeField] Transform[] posicoes;
    int contador;
    [SerializeField] GameObject inventoryUIObjects;
    [SerializeField] AudioSource clickSound;
    [SerializeField] GameObject select;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text itemHelthText;
    [SerializeField] TMP_Text itemResText;
    [SerializeField] TMP_Text itemAtkText;
    [SerializeField] TMP_Text itemVelText;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] Inventory inventory;
    [SerializeField] Button equipButton;

    private void Update()
    {
        moneyText.text = "" + Inventory.money;
    }
    private void Awake()
    {
        posicoes = posicoesHolder.GetComponentsInChildren<Transform>();
        posicoes = posicoes.Where(t => t != posicoesHolder.transform).ToArray();
    }
    public void inventoryOn()
    {
        contador = 0;

        for (int i = 0; i < inventory.inventoryObjs.Length; i++)
        {
            GameObject inventoryGO = Instantiate(inventory.inventoryObjs[i], inventoryUIObjects.transform);
            inventoryGO.GetComponent<ItemIcons>().Initialize(select, null, descriptionText, nameText, null, itemHelthText, itemResText, itemAtkText, itemVelText,equipButton);
            inventoryGO.transform.position = posicoes[contador].position;
            contador++;
        }
    }

    public void CleanInventoryHud()
    {
        nameText.text = "";

        if (descriptionText != null)
            descriptionText.text = "";
        if (nameText != null)
            nameText.text = "";
        if (itemHelthText != null)
            this.itemHelthText.text = "";
        if (itemResText != null)
            this.itemResText.text = "";
        if (itemAtkText != null)
            this.itemAtkText.text = "";
        if (itemVelText != null)
            this.itemVelText.text = "";
        if (equipButton != null)
            equipButton?.gameObject.SetActive(false);


        GameObject[] allEquipment;
        allEquipment = new GameObject[inventoryUIObjects.transform.childCount];

        for (int i = 0; i < inventoryUIObjects.transform.childCount - 1; i++)
        {
            allEquipment[i] = inventoryUIObjects.transform.GetChild(i + 1).gameObject;
        }
        foreach (GameObject go in allEquipment)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }

    }

    // Métodos de ordenação na UI
    public void SortByName()
    {
        inventory.SortByName();
        RefreshInventoryUI();
    }

    public void SortByPrice()
    {
        inventory.SortByPrice();
        RefreshInventoryUI();
    }

    private void RefreshInventoryUI()
    {
        CleanInventoryHud();
        inventoryOn();
    }
}
