using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Items/Equipment")]
public class Equipment : Item
{
    public EquipmentType equipmentType;
    public int healthBonus = 0;
    public int resistanceBonus = 0;
    public int attackBonus = 0;
    public int speedBonus = 0;
}