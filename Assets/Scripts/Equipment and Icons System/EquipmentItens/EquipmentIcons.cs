using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EquipmentIcons : ItemIcons
{
    [SerializeField] Equipment equipmentItem; // Referência ao Scriptable Object
    public Equipment Equipment => equipmentItem;

    public override void Initialize(GameObject select, TMP_Text priceText, TMP_Text descriptionText, TMP_Text nameText, GameObject warningText, TMP_Text helthText, TMP_Text resText, TMP_Text atkText, TMP_Text velText, Button _button)
    {
        base.Initialize(select, priceText, descriptionText, nameText, warningText, helthText, resText, atkText, velText, _button);
        image.sprite = equipmentItem?.icon;
    }
    protected override string GetDescription() => equipmentItem.description;
    public override string GetName() => equipmentItem.itemName;

    public override int GetPrice() => equipmentItem.price;

    protected override void Highlight()
    {
        base.Highlight();
        NPCUIController.selectedEquipment = equipmentItem;
        if(EquipButton != null)
        {
            EquipButton?.gameObject.SetActive(true);
            EquipButton.GetComponentInChildren<TMP_Text>().text = "Equip";
        }

    }
    protected override void UpdateUI()
    {
        if (NPCUIController.selling)
        {
            priceText.text = "Price: " + equipmentItem.price / 2;
        }
        else if (NPCUIController.buying)
        {
            priceText.text = "Price: " + equipmentItem.price;
        }

        UpdateStatusTexts(equipmentItem.healthBonus, healthText, "HP: ");
        UpdateStatusTexts(equipmentItem.resistanceBonus, resText, "RES: ");
        UpdateStatusTexts(equipmentItem.attackBonus, atkText, "ATK: ");
        UpdateStatusTexts(equipmentItem.speedBonus, velText, "VEL: ");
    }

    private void UpdateStatusTexts(int bonus, TMP_Text text, string label)
    {
        if (bonus > 0)
        {
            text.gameObject.SetActive(true);
            text.text = label + bonus;
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }
}

