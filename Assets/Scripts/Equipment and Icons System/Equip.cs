using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public enum EquipmentType
{
    Pants,
    Armature,
    Boots,
    Helmet,
    Weapon
}

public class Equip : MonoBehaviour
{
    [SerializeField] GameObject pants;
    [SerializeField] GameObject armature;
    [SerializeField] GameObject boots;
    [SerializeField] GameObject helmet;
    [SerializeField] GameObject weapon;

    [SerializeField] GameObject legsEquip;
    [SerializeField] GameObject topEquip;
    [SerializeField] GameObject bootsEquip;
    [SerializeField] GameObject helmetEquip;
    [SerializeField] GameObject weaponEquip;

    private Dictionary<EquipmentType, (GameObject, GameObject)> equipmentMapping;
    private Dictionary<EquipmentType, GameObject> equippedItems = new Dictionary<EquipmentType, GameObject>();
    public IReadOnlyDictionary<EquipmentType, GameObject> EquippedItems => equippedItems;
    private PlayerController _playerController;
    [SerializeField] AudioSource equipSound;
    [SerializeField] AudioSource useSound;
    [SerializeField] AudioSource unequipSound;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] Inventory inventory;

    public List<Equipment> EquipedEquipments;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        playerStatus = GetComponent<PlayerStatus>();
        InitializeEquipmentMapping();
    }

    private void InitializeEquipmentMapping()
    {
        equipmentMapping = new Dictionary<EquipmentType, (GameObject, GameObject)>
        {
            { EquipmentType.Pants, (pants, legsEquip) },
            { EquipmentType.Armature, (armature, topEquip) },
            { EquipmentType.Boots, (boots, bootsEquip) },
            { EquipmentType.Helmet, (helmet, helmetEquip) },
            { EquipmentType.Weapon, (weapon, weaponEquip) }
        };
    }

    public void EquipMethod()
    {
        if (EquipmentIcons.gameTag != null)
        {
            EquipmentType equipmentType;
            if (Enum.TryParse(EquipmentIcons.gameTag, true, out equipmentType))
            {
                EquipItem(equipmentType);
            }
            else
            {
                ConsumeItem(NPCUIController.SelectedIcon.GetComponent<ConsumableIcons>().consumableItem);            
            }
        }
    }

    private void ConsumeItem(UsableItem item)
    {
        if (useSound != null)
            useSound.Play();

        item.ApplyEffect(playerStatus);

        Debug.Log("Consume Item");
        int index = 0;
        foreach(var _object in inventory.inventoryObjs)
        {
            if(_object.name + "(Clone)" == NPCUIController.SelectedIcon.name)
            {
                inventory.RemoveItemFromInventory(index);
                return;
            }
            index++;
        }
        
    }

    private void EquipItem(EquipmentType equipmentType)
    {
       
        if (CheckIfItIsAlreadyEquipped(equipmentType) && EquipedEquipments.Contains(NPCUIController.selectedEquipment))
        {
            UnequipItem(equipmentType);
            return;
        }
        else if (CheckIfItIsAlreadyEquipped(equipmentType))
        { 
            foreach (var equipment in EquipedEquipments)
            {
                if (equipment.equipmentType == NPCUIController.selectedEquipment.equipmentType)
                {
                    if (unequipSound != null)
                        unequipSound.Play();
                    UnequipItem(equipmentType, equipment);
                    EquipedEquipments.Remove(equipment);
                    break;
                }
            }
        }
        if (equipSound != null)
            equipSound.Play();
        var (item, itemEquip) = equipmentMapping[equipmentType];
        item.SetActive(true);
        itemEquip.SetActive(true);
        if(equipmentType != EquipmentType.Weapon)
        {
            item.GetComponent<SpriteRenderer>().color = EquipmentIcons.colorstorage;
            itemEquip.GetComponent<Image>().color = EquipmentIcons.colorstorage;
            UpdateAnimator(equipmentType);
        }
        else
        {
            item.GetComponent<SpriteRenderer>().sprite = NPCUIController.selectedEquipment.icon;
            itemEquip.GetComponent<Image>().sprite = NPCUIController.selectedEquipment.icon;
        }
  
        playerStatus.ApplyEquipmentBonus(NPCUIController.selectedEquipment);      
        EquipedEquipments.Add(NPCUIController.selectedEquipment);
        equippedItems[equipmentType] = item;
    }

    private void UnequipItem(EquipmentType equipmentType, Equipment oldEquip = null)
    {
        var (item, itemEquip) = equipmentMapping[equipmentType];
        foreach (var equipment in equippedItems.Values)
        {
            if (equipment.name.Contains(item.name))
            {
                equipment?.SetActive(false);
                break;
            }
                
        }
        foreach (var equipment in equipmentMapping.Values)
        {
            if(equipment.Item2.name.Contains(itemEquip.name))
            {
                equipment.Item2.SetActive(false);
                break;
            }
        }

        if (oldEquip)
        {
            playerStatus.RemoveEquipmentBonus(oldEquip);
            EquipedEquipments.Remove(oldEquip);
        }
        else
        {
            EquipedEquipments.Remove(NPCUIController.selectedEquipment);
            playerStatus.RemoveEquipmentBonus(NPCUIController.selectedEquipment);
        }

        equippedItems.Remove(equipmentType);
    }

    private void UpdateAnimator(EquipmentType equipmentType)
    {
        int animIndex = equipmentType switch
        {
            EquipmentType.Armature => 0,
            EquipmentType.Pants => 1,
            EquipmentType.Boots => 2,
            EquipmentType.Helmet => 3,
            _ => -1
        };

        if (animIndex != -1)
        {
            if (PlayerController.CheckInputX() != 0)
            {
                _playerController.equipmentAnimatorController[animIndex].SetFloat("moveX", PlayerController.CheckInputX());
            }
            else if(PlayerController.CheckInputY() != 0)
            {
                _playerController.equipmentAnimatorController[animIndex].SetFloat("moveY", PlayerController.CheckInputY());
            }
        }
    }

    private bool CheckIfItIsAlreadyEquipped(EquipmentType equipmentType)
    {
        return equippedItems.ContainsKey(equipmentType);
    }
}
