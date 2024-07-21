//using Unity.VisualScripting;
//using UnityEngine;

//public class Waiter : MonoBehaviour
//{
//    private Food heldFood;
//    public Transform holdPosition; // Assign this in the inspector

//    public void PickupFood(Food food)
//    {
//        if (heldFood == null)
//        {
//            heldFood = food;
//            food.PickUp(holdPosition);
//            Debug.Log("Picked up: " + food.foodName);
//        }
//    }

//    void Update()
//    {
//        if (heldFood != null && Input.GetMouseButtonDown(0)) // Click to deliver
//        {
//            DeliverFood();
//        }
//        if (Input.GetMouseButtonDown(0)) // Right click to drop food
//        {

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
//}


using UnityEngine;

public class Waiter : MonoBehaviour
{
    private Food heldFood;
    public Transform holdPosition; // Assign this in the inspector
    public Camera playerCamera; // Assign your player's camera in the inspector
    public float pickupRange = 2.0f; // Maximum range to pick up food
    public LayerMask foodLayer; // Assign the Food layer in the inspector

    void Update()
    {
        if (heldFood == null && Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            TryPickupFood();
        }
        else if (heldFood != null && Input.GetMouseButtonDown(0)) // Click to deliver
        {
            DeliverFood();
        }
    }

    void TryPickupFood()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, foodLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            Food food = hit.collider.GetComponent<Food>();
            if (food != null && heldFood == null)
            {
                PickupFood(food);
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

    void DeliverFood()
    {
        if (heldFood != null)
        {
            heldFood.DeliverToCustomer();
            heldFood = null;
        }
    }
}
