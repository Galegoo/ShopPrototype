using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private Inventory inventory;
    private PlayerStatus status;
    [SerializeField] AudioSource collectSound;
    
    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable Item"))
        {
            CollectibleItem collectible = collision.GetComponent<CollectibleItem>();
            if (collectible != null)
            {
                CollectItem(collectible);
            }
        }
    }

    private void CollectItem(CollectibleItem collectible)
    {
        // Desabilitar o Collider do item para evitar múltiplas colisões
        collectible.GetComponent<Collider2D>().enabled = false;

        if (collectible.item.itemType == UsableItemType.Coin)
        {
            Inventory.money += collectible.item.amount;
        }
        else if (collectible.item.itemType == UsableItemType.DecreaseHealth)
        {
            status.AplyDamage(collectible.item.amount);
        }
        else
        {
            if (!inventory.AddItemToInventorry(DealWithItemType(collectible.item.itemType)))
                return;            
        }

        if (collectSound != null)
        {
            collectSound.Play();
        }

        Destroy(collectible.gameObject);
    }

    public GameObject DealWithItemType(UsableItemType itemType)
    {
        switch (itemType)
        {
            case UsableItemType.RegenerateHealth:
                return inventory.inventoryItensPrefabs[0];
            case UsableItemType.IncreaseResistance:
                return inventory.inventoryItensPrefabs[1];
            case UsableItemType.IncreaseAttack:
                return inventory.inventoryItensPrefabs[2];
            case UsableItemType.IncreaseSpeed:
                return inventory.inventoryItensPrefabs[3]; 
            default:
                return null;
        }
    }
}