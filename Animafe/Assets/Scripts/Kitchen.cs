//using UnityEngine;
//using System.Collections.Generic;

//public class Kitchen : MonoBehaviour
//{
//    [System.Serializable]
//    public struct FoodItem
//    {
//        public string name;
//        public float cookingTime;
//        public GameObject prefab; // Add a reference to the food prefab
//    }

//    public FoodItem[] foodItems;
//    private Dictionary<string, float> cookingTimes = new Dictionary<string, float>();
//    private Dictionary<string, (float, Customer)> activeOrders = new Dictionary<string, (float, Customer)>();
//    private Dictionary<string, GameObject> foodPrefabs = new Dictionary<string, GameObject>();

//    public Transform foodReadyPosition; // Position where food will appear when ready

//    void Awake()
//    {
//        foreach (var item in foodItems)
//        {
//            cookingTimes[item.name] = item.cookingTime;
//            foodPrefabs[item.name] = item.prefab;
//        }
//    }

//    public void ReceiveOrder(string order, Customer customer)
//    {
//        if (cookingTimes.ContainsKey(order))
//        {
//            float readyTime = Time.time + cookingTimes[order];
//            activeOrders.Add(order, (readyTime, customer));
//            Debug.Log("Order received: " + order + ". Will be ready at " + readyTime);
//        }
//        else
//        {
//            Debug.LogError("Unknown order received: " + order);
//        }
//    }

//    void Update()
//    {
//        List<string> readyOrders = new List<string>();

//        foreach (var order in activeOrders)
//        {
//            if (Time.time >= order.Value.Item1)
//            {
//                readyOrders.Add(order.Key);
//            }
//        }

//        foreach (var order in readyOrders)
//        {
//            var customer = activeOrders[order].Item2;
//            activeOrders.Remove(order);
//            PlaceFoodForPickup(order, customer);
//        }
//    }

//    void PlaceFoodForPickup(string food, Customer customer)
//    {
//        Debug.Log("Food ready: " + food);

//        if (foodPrefabs.ContainsKey(food))
//        {
//            GameObject foodObject = Instantiate(foodPrefabs[food], foodReadyPosition.position, Quaternion.identity);
//            foodObject.GetComponent<Food>().Initialize(food, customer); // Assuming Food script exists on food prefab
//        }
//        else
//        {
//            Debug.LogError("No prefab found for food: " + food);
//        }
//    }
//}
using UnityEngine;
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
    private Queue<(string, float, Customer)> orderQueue = new Queue<(string, float, Customer)>();

    public Transform foodReadyPosition; // Position where food will appear when ready

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
            float readyTime = Time.time + cookingTimes[order];
            orderQueue.Enqueue((order, readyTime, customer));
            Debug.Log("Order received: " + order + ". Will be ready at " + readyTime);
        }
        else
        {
            Debug.LogError("Unknown order received: " + order);
        }
    }

    void Update()
    {
        if (orderQueue.Count > 0)
        {
            var nextOrder = orderQueue.Peek();
            if (Time.time >= nextOrder.Item2)
            {
                var (food, readyTime, customer) = orderQueue.Dequeue();
                PlaceFoodForPickup(food, customer);
            }
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
