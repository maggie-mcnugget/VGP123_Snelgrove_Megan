using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("Pickup Settings")]
    public GameObject pickupPrefab; 

    [Header("Spawn Points")]
    public Transform[] spawnPoints; 

    void Start()
    {
        SpawnPickups();
    }

    void SpawnPickups()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject pickup = Instantiate(pickupPrefab, spawnPoint.position, Quaternion.identity);

            
            SimplePickup sp = pickup.GetComponent<SimplePickup>();

        
            int randomType = Random.Range(0, 2); // 0 or 1

            sp.pickupType = (SimplePickup.PickupType)randomType;
        }
    }
}