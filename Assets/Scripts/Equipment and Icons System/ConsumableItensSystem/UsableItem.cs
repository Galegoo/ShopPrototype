using UnityEngine;

public abstract class UsableItem : Item
{
    public UsableItemType itemType;
    public int amount;

    public abstract void ApplyEffect(PlayerStatus playerStats);
}