//using UnityEngine;

//public class Food : MonoBehaviour
//{
//    public string foodName;
//    private Customer customer;
//    private Transform holdPosition;
//    private bool isPlayerNearby = false;

//    public void Initialize(string name, Customer customer)
//    {
//        this.foodName = name;
//        this.customer = customer;
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            isPlayerNearby = true;
//            Debug.Log("Player is near the food.");
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            isPlayerNearby = false;
//            Debug.Log("Player left the food.");
//        }
//    }

//    void Update()
//    {
//        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Return))
//        {
//            Waiter waiter = FindObjectOfType<Waiter>();
//            if (waiter != null)
//            {
//                waiter.PickupFood(this);
//            }
//        }
//    }

//    public void PickUp(Transform holdPos)
//    {
//        holdPosition = holdPos;
//        transform.SetParent(holdPosition);
//        transform.localPosition = Vector3.zero;
//        transform.localRotation = Quaternion.identity;
//        GetComponent<Collider>().enabled = false; // Disable collider to prevent further interactions
//    }

//    public void DeliverToCustomer()
//    {
//        if (customer != null)
//        {
//            customer.ReceiveFood(foodName);
//            Destroy(gameObject); // Remove food from the scene after delivery
//        }
//    }
//}


using UnityEngine;

public class Food : MonoBehaviour
{
    public string foodName;
    private Customer customer;
    private Transform holdPosition;
    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Initialize(string name, Customer customer)
    {
        this.foodName = name;
        this.customer = customer;
    }

    public void PickUp(Transform holdPos)
    {
        holdPosition = holdPos;
        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics
        }
        if (col != null)
        {
            col.enabled = false; // Disable collider
        }
    }

    public void DeliverToCustomer()
    {
        if (customer != null)
        {
            customer.ReceiveFood(foodName);
            Destroy(gameObject); // Remove food from the scene after delivery
        }
    }
}
