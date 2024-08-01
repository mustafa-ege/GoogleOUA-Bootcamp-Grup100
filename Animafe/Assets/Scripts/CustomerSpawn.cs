using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs; // List of customer prefabs to spawn
    public List<Transform> spawnPointSet; // List of spawn point transforms
    public float spawnInterval = 5.0f; // Time between spawns
    public int maxCustomers = 10; // Maximum number of customers allowed in the scene

    private List<GameObject> spawnedCustomers = new List<GameObject>(); // Track spawned customers
    private float spawnTimer = 0f; // Timer for spawning

    void Start()
    {
        // Initialize spawn timer
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // Check if we can spawn more customers
        if (spawnedCustomers.Count < maxCustomers)
        {
            // Decrement the spawn timer
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnCustomer();
                spawnTimer = spawnInterval; // Reset timer after spawning
            }
        }

        // Optional: Clean up customers that have left
        CleanupCustomers();
    }

    void SpawnCustomer()
    {
        if (customerPrefabs.Count == 0 || spawnPointSet.Count == 0)
        {
            Debug.LogError("Customer prefabs or spawn points not set.");
            return;
        }

        // Choose a random customer prefab
        int randomCustomerIndex = Random.Range(0, customerPrefabs.Count);
        GameObject customerPrefab = customerPrefabs[randomCustomerIndex];

        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPointSet.Count);
        Transform spawnPoint = spawnPointSet[randomIndex];

        // Instantiate a new customer at the chosen spawn point
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedCustomers.Add(newCustomer);

        Debug.Log("Customer spawned: " + customerPrefab.name + " at " + spawnPoint.position);
    }

    void CleanupCustomers()
    {
        // Remove null references from the list (e.g., customers that have been destroyed)
        spawnedCustomers.RemoveAll(customer => customer == null);
    }
}


