//using UnityEngine;
//using System.Collections.Generic;

//public class Customer : MonoBehaviour
//{
//    public List<string> foodOptions; // List of possible food items
//    public string currentOrder;
//    public Transform tablePosition; // The position where the customer sits

//    void Start()
//    {
//        MakeOrder();
//    }

//    void MakeOrder()
//    {
//        if (foodOptions == null || foodOptions.Count == 0)
//        {
//            Debug.LogError("No food options assigned to the customer.");
//            return;
//        }

//        int randomIndex = Random.Range(0, foodOptions.Count);
//        currentOrder = foodOptions[randomIndex];
//        Debug.Log("Customer ordered: " + currentOrder);

//        Kitchen kitchen = FindObjectOfType<Kitchen>();
//        if (kitchen == null)
//        {
//            Debug.LogError("No Kitchen found in the scene.");
//            return;
//        }
//        kitchen.ReceiveOrder(currentOrder, this);
//    }

//    public void ReceiveFood(string food)
//    {
//        if (food == currentOrder)
//        {
//            Debug.Log("Customer received their food: " + food);
//            // Add code for customer to consume the food
//        }
//        else
//        {
//            Debug.LogError("Wrong food delivered: " + food);
//        }
//    }
//}
using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public List<string> foodOptions; // List of possible food items
    public string currentOrder;
    private bool isPlayerNearby = false;

    void Start()
    {
        MakeOrder();
    }

    void MakeOrder()
    {
        if (foodOptions == null || foodOptions.Count == 0)
        {
            Debug.LogError("No food options assigned to the customer.");
            return;
        }

        int randomIndex = Random.Range(0, foodOptions.Count);
        currentOrder = foodOptions[randomIndex];
        Debug.Log("Customer ordered: " + currentOrder);

        Kitchen kitchen = FindObjectOfType<Kitchen>();
        if (kitchen == null)
        {
            Debug.LogError("No Kitchen found in the scene.");
            return;
        }
        kitchen.ReceiveOrder(currentOrder, this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the customer.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the customer.");
        }
    }

    public void ReceiveFood(string food)
    {
        if (food == currentOrder)
        {
            Debug.Log("Customer received their food: " + food);
            // Add code for customer to consume the food
        }
        else
        {
            Debug.Log("Wrong food delivered: " + food);
        }
    }

    public bool IsPlayerNearby()
    {
        return isPlayerNearby;
    }

    public string GetCurrentOrder()
    {
        return currentOrder;
    }
}
