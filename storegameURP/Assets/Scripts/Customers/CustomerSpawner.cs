using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] endPositions;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private float startDelay;
    [SerializeField, Range(0, 60)] private int customersPerMinute;
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => Level.Current.StoreOpen);
        yield return new WaitForSeconds(startDelay);

        while (Level.Current.StoreOpen)
        {
            // If there is at least one product available, take a random chance based on customer density.
            if (Product.ProductsAvailable.Count >= 1 && Random.Range(0, 100) <= (customersPerMinute / 60) * 100)
            {
                Customer customer = Instantiate(customerPrefab, transform.position, Quaternion.identity).GetComponent<Customer>();
                customer.Wanted = Product.GetRandomProduct();
                customer.EndPos = endPositions[Random.Range(0, endPositions.Length)].position;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
