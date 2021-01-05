using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] endPositions;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField, Range(0, 60)] private int customersPerMinute;
    
    IEnumerator Start()
    {
        while (true)
        {
            // If there is at least one product available, take a random chance based on customer density.
            if (Product.ProductsAvailable.Count >= 1 && Random.Range(0, 100) <= (customersPerMinute / 60) * 100)
            {
                Customer customer = Instantiate(customerPrefab).GetComponent<Customer>();
                customer.Wanted = Product.GetRandomProduct();
                customer.EndPos = endPositions[Random.Range(0, endPositions.Length)].position;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
