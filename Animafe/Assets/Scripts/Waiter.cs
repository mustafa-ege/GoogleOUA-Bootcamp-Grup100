//using UnityEngine;

//public class Waiter : MonoBehaviour
//{
//    private Food heldFood;
//    public Transform holdPosition; // Assign this in the inspector
//    public float pickupRange = 2.0f; // Maximum range to pick up food
//    public LayerMask foodLayer; // Assign the Food layer in the inspector
//    private Collider[] nearbyFoods;

//    void Update()
//    {
//        if (heldFood == null && Input.GetMouseButtonDown(0)) // Left mouse button click
//        {
//            TryPickupFood();
//        }
//        else if (heldFood != null && Input.GetMouseButtonDown(0)) // Click to deliver
//        {
//            DeliverFood();
//        }
//    }

//    void TryPickupFood()
//    {
//        nearbyFoods = Physics.OverlapSphere(transform.position, pickupRange, foodLayer);
//        if (nearbyFoods.Length > 0)
//        {
//            foreach (var collider in nearbyFoods)
//            {
//                Food food = collider.GetComponent<Food>();
//                if (food != null && heldFood == null)
//                {
//                    PickupFood(food);
//                    break;
//                }
//            }
//        }
//    }

//    public void PickupFood(Food food)
//    {
//        if (heldFood == null)
//        {
//            heldFood = food;
//            food.PickUp(holdPosition);
//            Debug.Log("Picked up: " + food.foodName);
//        }
//    }

//    void DeliverFood()
//    {
//        if (heldFood != null)
//        {
//            heldFood.DeliverToCustomer();
//            heldFood = null;
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        Food food = other.GetComponent<Food>();
//        if (food != null && heldFood == null)
//        {
//            PickupFood(food);
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    private Food heldFood;
    public Transform holdPosition; // Assign this in the inspector
    public float pickupRange = 2.0f; // Maximum range to pick up food
    public LayerMask foodLayer; // Assign the Food layer in the inspector
    public LayerMask customerLayer; // Assign the Customer layer in the inspector
    private Collider[] nearbyFoods;
    private List<Customer> nearbyCustomers = new List<Customer>();

    void Update()
    {
        if (heldFood == null && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            TryPickupFood();
        }
        else if (heldFood != null && Input.GetMouseButtonDown(0)) // Click to deliver
        {
            TryDeliverFood();
        }
    }

    void TryPickupFood()
    {
        nearbyFoods = Physics.OverlapSphere(transform.position, pickupRange, foodLayer);
        if (nearbyFoods.Length > 0)
        {
            foreach (var collider in nearbyFoods)
            {
                Food food = collider.GetComponent<Food>();
                if (food != null && heldFood == null)
                {
                    PickupFood(food);
                    break;
                }
            }
        }
    }

    public void PickupFood(Food food)
    {
        if (heldFood == null)
        {
            heldFood = food;
            food.PickUp(holdPosition);
            Debug.Log("Picked up: " + food.foodName);
        }
    }

    void TryDeliverFood()
    {
        foreach (var customer in nearbyCustomers)
        {
            if (customer.IsPlayerNearby())
            {
                DeliverFood(customer);
                break;
            }
        }
    }

    void DeliverFood(Customer customer)
    {
        if (heldFood != null)
        {
            if (customer.GetCurrentOrder() == heldFood.foodName)
            {
                customer.ReceiveFood(heldFood.foodName);
                heldFood.DeliverToCustomer();
                heldFood = null;
            }
            else
            {
                Debug.Log("Wrong customer!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Food food = other.GetComponent<Food>();
        if (food != null && heldFood == null)
        {
            PickupFood(food);
        }

        Customer customer = other.GetComponent<Customer>();
        if (customer != null)
        {
            nearbyCustomers.Add(customer);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Customer customer = other.GetComponent<Customer>();
        if (customer != null)
        {
            nearbyCustomers.Remove(customer);
        }
    }
}
