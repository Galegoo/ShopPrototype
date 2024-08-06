using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public int baseHealth = 500;
    public int baseResistance = 20;
    public int baseAttack = 30;
    public int baseSpeed = 15;

    private int currentHealth;
    private int currentResistance;
    private int currentAttack;
    private int currentSpeed;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text resistanceText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text speedText;

    private void Start()
    {
        ResetToBaseStatus();
        UpdateStatusUI();
    }

    public void ApplyEquipmentBonus(Equipment equipment)
    {
        currentHealth += equipment.healthBonus;
        currentResistance += equipment.resistanceBonus;
        currentAttack += equipment.attackBonus;
        currentSpeed += equipment.speedBonus;
        UpdateStatusUI();
    }

    public void RemoveEquipmentBonus(Equipment equipment)
    {
        currentHealth -= equipment.healthBonus;
        currentResistance -= equipment.resistanceBonus;
        currentAttack -= equipment.attackBonus;
        currentSpeed -= equipment.speedBonus;
        UpdateStatusUI();
    }

    public void RegenerateHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, baseHealth);
        UpdateStatusUI();
    }
    public void AplyDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        UpdateStatusUI();
    }

    public void IncreaseMaxHealth(int amount)
    {
        baseHealth += amount;
        currentHealth += amount;
        UpdateStatusUI();
    }

    public void IncreaseResistance(int amount)
    {
        baseResistance += amount;
        currentResistance += amount;
        UpdateStatusUI();
    }

    public void IncreaseAttack(int amount)
    {
        baseAttack += amount;
        currentAttack += amount;
        UpdateStatusUI();
    }

    public void IncreaseSpeed(int amount)
    {
        baseSpeed += amount;
        currentSpeed += amount;
        UpdateStatusUI();
    }

    private void UpdateStatusUI()
    {
        healthText.text = "HP: " + currentHealth;
        resistanceText.text = "RES: " + currentResistance;
        attackText.text = "ATK: " + currentAttack;
        speedText.text = "VEL: " + currentSpeed;
    }

    private void ResetToBaseStatus()
    {
        currentHealth = baseHealth;
        currentResistance = baseResistance;
        currentAttack = baseAttack;
        currentSpeed = baseSpeed;
    }
}