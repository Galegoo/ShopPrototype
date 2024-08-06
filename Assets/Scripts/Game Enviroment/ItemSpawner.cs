using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; 
    public Transform spawnPoint; 
    public float spawnInterval = 2f; 
    private GameObject currentItem; 

    private float spawnTimer;
    private bool playerInSpawnArea;

    private void Start()
    {
        spawnPoint = transform;
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && !playerInSpawnArea && currentItem == null)
        {
            SpawnItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnItem()
    {
        currentItem = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSpawnArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSpawnArea = false;
        }
    }
}
