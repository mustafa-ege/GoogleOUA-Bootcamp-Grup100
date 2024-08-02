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
            PlaceInFrontOfCustomer();
        }
    }

    private void PlaceInFrontOfCustomer()
    {
        if (customer != null)
        {
            // Calculate the position in front of the customer
            Vector3 frontPosition = customer.transform.position + customer.transform.forward ; // Adjust the distance as needed
            frontPosition.y = customer.transform.position.y; // Maintain the same height as the customer

            // Update food position
            transform.SetParent(null); // Unparent the food from the hold position
            transform.position = frontPosition;
            transform.rotation = Quaternion.identity;

            // Re-enable the physics and collider if needed
            if (rb != null)
            {
                rb.isKinematic = false; // Re-enable physics
            }
            if (col != null)
            {
                col.enabled = true; // Re-enable collider
            }
        }
    }
}
