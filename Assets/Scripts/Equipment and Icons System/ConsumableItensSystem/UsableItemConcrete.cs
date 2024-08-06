using UnityEngine;

[CreateAssetMenu(fileName = "NewUsableItem", menuName = "Items/UsableItem")]
public class UsableItemConcrete : UsableItem
{
    public override void ApplyEffect(PlayerStatus playerStats)
    {
        switch (itemType)
        {
            case UsableItemType.RegenerateHealth:
                playerStats.RegenerateHealth(amount);
                break;
            case UsableItemType.IncreaseMaxHealth:
                playerStats.IncreaseMaxHealth(amount);
                break;
            case UsableItemType.IncreaseResistance:
                playerStats.IncreaseResistance(amount);
                break;
            case UsableItemType.IncreaseAttack:
                playerStats.IncreaseAttack(amount);
                break;
            case UsableItemType.IncreaseSpeed:
                playerStats.IncreaseSpeed(amount);
                break;
        }
    }
}