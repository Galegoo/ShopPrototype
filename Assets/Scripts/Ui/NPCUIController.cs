using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NPCUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject baloonsCanvas;
    [SerializeField] GameObject pressEtoInteractCanvas;
    [SerializeField] GameObject arrowBuy;
    [SerializeField] GameObject arrowSell;
    [SerializeField] GameObject arrowExit;
    [SerializeField] GameObject SellInventory;
    [SerializeField] GameObject BuyInventory;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text warningText;

    [Header("Inventory Settings")]
    [SerializeField] GameObject[] positions;
    [SerializeField] GameObject[] positionsbuy;
    [SerializeField] GameObject[] allEquipmenttoBuy;
    [SerializeField] GameObject objectssell;
    [SerializeField] GameObject objectsbuy;
    [SerializeField] int index;

    [Header("Audio Settings")]
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource SellSound;
    [SerializeField] AudioSource BuySound;

    [SerializeField] Inventory inventoryRef;

    static bool isNPCActive;
    public static bool selling;
    public static bool buying;
    public static GameObject SelectedIcon;
    public static Equipment selectedEquipment;
    public static ConsumableIcons selectedUsableItem;

    [Header("PopUpInitialization")]
    [SerializeField] GameObject select;
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text helthText;
    [SerializeField] TMP_Text resText;
    [SerializeField] TMP_Text atkText;
    [SerializeField] TMP_Text velText;

    int contador;
    [SerializeField] int timesBuyWasOpen;
    [SerializeField] int timesNpcWasOpen;
    [SerializeField] int npcInteractionController;

    void Start()
    {
        InitializeUI();
        UpdateMoneyText();
    }

    void Update()
    {
        HandleNPCInteraction();
        HandleUIInteraction();      
    }

    void InitializeUI()
    {
        pressEtoInteractCanvas.SetActive(false);
        index = 1;
        selling = false;
        buying = false;
        timesNpcWasOpen = 0;
    }

    void HandleNPCInteraction()
    {
        isNPCActive = baloonsCanvas.activeSelf;

        if (PlayerController.GetNpcWasTouched())
        {
            if (!baloonsCanvas.activeSelf)
            {
                pressEtoInteractCanvas.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !baloonsCanvas.activeSelf && timesNpcWasOpen == 0)
            {
                timesNpcWasOpen++;
                OpenNpcInteraction();
            }
        }
        else
        {
            pressEtoInteractCanvas.SetActive(false);
            baloonsCanvas.SetActive(false);
        }
    }

    void HandleUIInteraction()
    {
        if (baloonsCanvas.activeSelf)
        {
            HandleArrowNavigation();

            if (!BuyInventory.activeSelf && !SellInventory.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                npcInteractionController++;
                if (npcInteractionController > 1)
                    HandleInventorySelection();
            }
        }
    }

    void HandleArrowNavigation()
    {
        switch (index)
        {
            case 1:
                SetActiveArrow(arrowBuy);
                break;
            case 2:
                SetActiveArrow(arrowSell);
                break;
            case 3:
                SetActiveArrow(arrowExit);
                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && index > 1)
        {
            index--;
            PlayClickSound();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && index < 3)
        {
            index++;
            PlayClickSound();
        }
    }

    void SetActiveArrow(GameObject activeArrow)
    {
        arrowBuy.SetActive(activeArrow == arrowBuy);
        arrowSell.SetActive(activeArrow == arrowSell);
        arrowExit.SetActive(activeArrow == arrowExit);
    }

    void HandleInventorySelection()
    {
        switch (index)
        {
            case 1:
                OpenNpc(BuyInventory);
                buying = true;
                if (timesBuyWasOpen < 1)
                {
                    contador = 0;
                    DrawBuyInventory();
                    timesBuyWasOpen++;
                }
                break;
            case 2:
                OpenNpc(SellInventory);
                selling = true;
                contador = 0;
                DrawSellInventory();
                break;
            case 3:
                CloseNpcInteraction();
                break;
        }
        PlayClickSound();
    }

    void UpdateMoneyText()
    {
        moneyText.text = Inventory.money.ToString();
    }

    void OpenNpcInteraction()
    {
        PlayerController.changeLimitMovmentStatus(true);
        pressEtoInteractCanvas.SetActive(false);
        baloonsCanvas.SetActive(true);
        arrowSell.SetActive(false);
        arrowExit.SetActive(false);
    }

    void CloseNpcInteraction()
    {
        PlayerController.changeLimitMovmentStatus(false);
        pressEtoInteractCanvas.SetActive(false);
        baloonsCanvas.SetActive(false);
        index = 1;
        npcInteractionController = 0;
        StartCoroutine(WaitToOpenNpcAgain());
    }

    void DrawBuyInventory()
    {
        for (int i = 0; i < allEquipmenttoBuy.Length; i++)
        {
            GameObject inventoryGO = Instantiate(allEquipmenttoBuy[i], objectsbuy.transform);
            inventoryGO?.GetComponent<ItemIcons>().Initialize(select, priceText, descriptionText, nameText, warningText.gameObject, helthText, resText, atkText, velText);
            AddToInventoryBuy(inventoryGO);
        }
    }

    void DrawSellInventory()
    {
        for (int i = 0; i < inventoryRef.inventoryObjs.Length; i++)
        {
            GameObject inventoryGO = Instantiate(inventoryRef.inventoryObjs[i], objectssell.transform);
            inventoryGO?.GetComponent<ItemIcons>().Initialize(select, priceText, descriptionText, nameText, warningText.gameObject, helthText, resText, atkText, velText);
            inventoryGO.transform.position = positions[contador].transform.position;
            contador++;
        }
    }

    void AddToInventoryBuy(GameObject obj)
    {
        obj.transform.position = positionsbuy[contador].transform.position;
        allEquipmenttoBuy[contador] = obj;
        contador++;
    }

    void CleanInventory(GameObject _objects)
    {
        foreach (Transform child in _objects.transform)
        {
            if (child != _objects.transform.GetChild(0)) // Assuming first child is not to be destroyed
            {
                Destroy(child.gameObject);
            }
        }
    }

    void CleanInventorySpecific(GameObject _objects)
    {
        foreach (Transform child in _objects.transform)
        {
            if (child.gameObject.name == SelectedIcon.name)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public static bool IsNpcOn()
    {
        return isNPCActive;
    }

    public void OpenNpc(GameObject popup)
    {
        UpdateMoneyText();
        popup.transform.parent.gameObject.SetActive(true);
        popup.SetActive(true);
        PlayerController.changeLimitMovmentStatus(true);        
    }

    public void CloseSellOrBuy()
    {
        PlayClickSound();

        CleanInventory(objectssell);

        if (priceText != null)
            priceText.text = "";
        if (descriptionText != null)
            this.descriptionText.text = "";
        if (nameText != null)
            this.nameText.text = "";
        if (warningText != null)
            this.warningText.text = "";
        if (helthText != null)
            this.helthText.text = "";
        if (resText != null)
            this.resText.text = "";
        if (atkText != null)
            this.atkText.text = "";
        if (velText != null)
            this.velText.text = "";

        SellInventory.SetActive(false);
        BuyInventory.SetActive(false);
        SellInventory.transform.parent.gameObject.SetActive(false);
        selling = false;
        buying = false;
        SelectedIcon = null;
        timesBuyWasOpen = 0;
        timesNpcWasOpen = 0;
    }

    public void Buy()
    {
        if (SelectedIcon != null)
        {
                
                if (SelectedIcon.GetComponent<ItemIcons>().GetPrice() <= Inventory.money)
                {
                    if (inventoryRef.AddItemToInventorry(SelectedIcon))
                    {
                        Inventory.money -= SelectedIcon.GetComponent<ItemIcons>().GetPrice();
                        UpdateMoneyText();
                        if (BuySound != null)
                            BuySound.Play();
                    }
                    else
                    {
                        warningText.gameObject.SetActive(true);
                        warningText.text = "Inventory is full";
                    }
                }
                else
                {
                    warningText.gameObject.SetActive(true);
                    warningText.text = "Insufficient funds";
                }
        }
    }

    public void Sell()
    {
        if (SelectedIcon != null)
        {
            if (!CheckIfItemToBeSoldIsEquipped())
            {
                CleanInventorySpecific(objectssell);

                    for (int i = 0; i < inventoryRef.inventoryObjs.Length; i++)
                    {
                        if (inventoryRef.inventoryObjs[i].gameObject.name + "(Clone)" == SelectedIcon.name)
                        {
                            Destroy(objectssell.transform.GetChild(i)); // Remove o item da interface
                            inventoryRef.RemoveItemFromInventory(i);
                            Inventory.money += SelectedIcon.GetComponent<ItemIcons>().GetPrice() / 2;
                            UpdateMoneyText();
                            if (SellSound != null)
                                SellSound.Play();
                            SelectedIcon = null;
                        }
                    }
            }
        }
    }
    // Checks if the item to be sold is equipped

    bool CheckIfItemToBeSoldIsEquipped()
    {
        Equip equipRef = FindObjectOfType<Equip>();
        int check = 0;

        foreach (var equippedItem in equipRef.EquippedItems.Values)
        {
            if (equippedItem != null)
            {
                if (SelectedIcon.gameObject.tag == equippedItem.gameObject.tag &&
                    SelectedIcon.GetComponent<Image>().color == equippedItem.GetComponent<SpriteRenderer>().color)
                {
                    check++;
                }
            }
        }

        if (check > 0)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "You  can't  sell  clothes  you  are  currently  wearing ";
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator WaitToOpenNpcAgain()
    {
        yield return new WaitForSeconds(2f);
        timesNpcWasOpen = 0;
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }

    public void PlaySellSound()
    {
        SellSound.Play();
    }

    public void PlayBuySound()
    {
        BuySound.Play();
    }
}
