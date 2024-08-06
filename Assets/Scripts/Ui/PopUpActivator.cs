using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpActivator : MonoBehaviour
{
    [SerializeField] GameObject inventoryPopUp;
    [SerializeField] Button InventoryButton;
    [SerializeField] Sprite[] inventoryButtonImages;
    int controler;

    [SerializeField] AudioSource inventorySound;

    [SerializeField] InventoryUI inventory;
    // Start is called before the first frame update
    void Start()
    {
        //inventoryPopUp.SetActive(false);
        controler = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !NPCUIController.IsNpcOn())
        {
            switch (controler)
            {
                case 0:
                    OpenInventory();
                    PlayerController.changeLimitMovmentStatus(true);             
                    break;

                case 1:
                    CloseInventory();
                    PlayerController.changeLimitMovmentStatus(false);

                    break;
            }

        }
    }

    public void OpenInventory()
    {
        if (!NPCUIController.IsNpcOn())
        {
            if (inventorySound != null)
            {
                inventorySound.Play();
            }
            if (controler < 1)
            {
                controler++;
                InventoryButton.image.sprite = inventoryButtonImages[controler];
                inventoryPopUp.SetActive(true);
                PlayerController.changeLimitMovmentStatus(true);
                //FindObjectOfType<Inventory>().inventoryOn();
                inventory.inventoryOn();
            }
            else
                CloseInventory();
        }    
    }
    public void CloseInventory()
    {
        if(inventorySound != null)
        {
            inventorySound.Play();
        }     
        inventoryPopUp.SetActive(false);
        controler = 0;
        InventoryButton.image.sprite = inventoryButtonImages[controler];
        PlayerController.changeLimitMovmentStatus(false);
        //FindObjectOfType<Inventory>().CleanInventoryHud();
        inventory.CleanInventoryHud();
        EquipmentIcons.gameTag = null;
    }
}
