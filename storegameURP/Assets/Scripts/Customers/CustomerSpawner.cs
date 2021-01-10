using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform[] endPositions;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private float startDelay;
    [SerializeField, Range(1, 60)] private int customersPerMinute;

    private static int customers;

    void Awake() => customers = 0;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Level.Current.StoreOpen);
        yield return new WaitForSeconds(DayScreen.Duration + startDelay);

        while (Level.Current.StoreOpen)
        {
            if (customers >= Level.Current.Capacity)
            {
                yield return new WaitForSeconds(1.0f);
                continue;
            }

            // If there is at least one product available, take a random chance based on customer density.
            if (Product.ProductsAvailable.Count >= 1 && Random.Range(0, 101) <= (float)customersPerMinute / 60 * 100)
            {
                Vector3 spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
                Customer customer = Instantiate(customerPrefab, spawnPoint, Quaternion.identity).GetComponent<Customer>();
                customer.Wanted = Product.GetRandomProduct();
                customer.EndPos = endPositions[Random.Range(0, endPositions.Length)].position;
                customers++;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public static void OnCustomerDestroyed() => customers--;
}
