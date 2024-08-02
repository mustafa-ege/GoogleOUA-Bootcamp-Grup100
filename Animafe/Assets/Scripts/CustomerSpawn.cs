using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI; // NavMesh için gerekli

public class CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs; // List of customer prefabs to spawn
    public List<Transform> spawnPointSet; // List of spawn point transforms
    public List<Transform> tablePoints; // Masalarýn transform listesi
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
        if (customerPrefabs.Count == 0 || spawnPointSet.Count == 0 || tablePoints.Count == 0)
        {
            Debug.LogError("Customer prefabs, spawn points, or table points not set.");
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

        // Müþteriyi masaya yönlendirin
        MoveCustomerToTable(newCustomer);

        Debug.Log("Customer spawned: " + customerPrefab.name + " at " + spawnPoint.position);
    }

    void MoveCustomerToTable(GameObject customer)
    {
        // Müþteri için bir hedef masa seçin
        int randomTableIndex = Random.Range(0, tablePoints.Count);
        Transform targetTable = tablePoints[randomTableIndex];

        // Müþterinin NavMeshAgent'ýný alýn ve hedefi ayarlayýn
        NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(targetTable.position);
        }
        else
        {
            Debug.LogError("Customer does not have a NavMeshAgent component.");
        }
    }

    void CleanupCustomers()
    {
        // Remove null references from the list (e.g., customers that have been destroyed)
        spawnedCustomers.RemoveAll(customer => customer == null);
    }
}


