using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Kitchen : MonoBehaviour
{
    [System.Serializable]
    public struct FoodItem
    {
        public string name;
        public float cookingTime;
        public GameObject prefab; // Add a reference to the food prefab
    }

    public FoodItem[] foodItems;
    private Dictionary<string, float> cookingTimes = new Dictionary<string, float>();
    private Dictionary<string, GameObject> foodPrefabs = new Dictionary<string, GameObject>();
    private Queue<(string, Customer)> orderQueue = new Queue<(string, Customer)>();

    public Transform foodReadyPosition; // Position where food will appear when ready
    private bool isCooking = false;

    void Awake()
    {
        foreach (var item in foodItems)
        {
            cookingTimes[item.name] = item.cookingTime;
            foodPrefabs[item.name] = item.prefab;
        }
    }

    public void ReceiveOrder(string order, Customer customer)
    {
        if (cookingTimes.ContainsKey(order))
        {
            orderQueue.Enqueue((order, customer));
            Debug.Log("Order received: " + order);
            if (!isCooking)
            {
                StartCoroutine(ProcessOrders());
            }
        }
        else
        {
            Debug.LogError("Unknown order received: " + order);
        }
    }

    private IEnumerator ProcessOrders()
    {
        isCooking = true;

        while (orderQueue.Count > 0)
        {
            var (food, customer) = orderQueue.Dequeue();
            yield return StartCoroutine(CookFood(food, customer));
        }

        isCooking = false;
    }

    private IEnumerator CookFood(string food, Customer customer)
    {
        if (cookingTimes.ContainsKey(food))
        {
            float cookingTime = cookingTimes[food];
            Debug.Log("Cooking food: " + food);
            yield return new WaitForSeconds(cookingTime);
            PlaceFoodForPickup(food, customer);
        }
        else
        {
            Debug.LogError("No cooking time found for food: " + food);
        }
    }

    void PlaceFoodForPickup(string food, Customer customer)
    {
        Debug.Log("Food ready: " + food);

        if (foodPrefabs.ContainsKey(food))
        {
            GameObject foodObject = Instantiate(foodPrefabs[food], foodReadyPosition.position, Quaternion.identity);
            foodObject.GetComponent<Food>().Initialize(food, customer); // Assuming Food script exists on food prefab
        }
        else
        {
            Debug.LogError("No prefab found for food: " + food);
        }
    }
}
