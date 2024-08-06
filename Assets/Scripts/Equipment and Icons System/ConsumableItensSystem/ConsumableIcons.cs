using UnityEngine;
using TMPro;

public class ConsumableIcons : ItemIcons
{
    public UsableItemConcrete consumableItem;

    protected override void Start()
    {
        base.Start();
        image.sprite = consumableItem?.icon;
    }

    protected override string GetDescription() => consumableItem.description;
    public override string GetName() => consumableItem.itemName;

    public override int GetPrice() => consumableItem.price;

    protected override void Highlight()
    {
        base.Highlight();
        if(EquipButton != null)
        {
            EquipButton.gameObject.SetActive(true);
            EquipButton.GetComponentInChildren<TMP_Text>().text = "Use";
        }


    }
    protected override void UpdateUI()
    {
        if (NPCUIController.selling)
        {
            priceText.text = "Price: " + consumableItem.price / 2;
        }
        else if (NPCUIController.buying)
        {
            priceText.text = "Price: " + consumableItem.price;
        }

        descriptionText.text = GetDescription();
        nameText.text = GetName();

        UpdateStatusTexts();
    }

    private void UpdateStatusTexts()
    {
        switch (consumableItem.itemType)
        {
            case UsableItemType.RegenerateHealth:
                SetActiveStatus(healthText, "Restore HP: " + consumableItem.amount);
                SetInactiveStatus(resText, atkText, velText);
                break;
            case UsableItemType.IncreaseMaxHealth:
                SetActiveStatus(healthText, "Max HP Increase: " + consumableItem.amount);
                SetInactiveStatus(resText, atkText, velText);
                break;
            case UsableItemType.IncreaseSpeed:
                SetActiveStatus(velText, "VEL Boost: " + consumableItem.amount);
                SetInactiveStatus(healthText, resText, atkText);
                break;
            case UsableItemType.IncreaseResistance:
                SetActiveStatus(resText, "RES Boost: " + consumableItem.amount);
                SetInactiveStatus(healthText, atkText, velText);
                break;
            case UsableItemType.IncreaseAttack:
                SetActiveStatus(atkText, "ATK Boost: " + consumableItem.amount);
                SetInactiveStatus(healthText, resText, velText);
                break;
            default:
                SetInactiveStatus(healthText, resText, atkText, velText);
                break;
        }
    }

    private void SetActiveStatus(TMP_Text text, string value)
    {
        text.gameObject.SetActive(true);
        text.text = value;
    }

    private void SetInactiveStatus(params TMP_Text[] texts)
    {
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }
}