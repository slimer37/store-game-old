using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Light scannerLight;

    public System.Action<Product> onScan;

    void OnTriggerEnter(Collider other)
    {
        Product scannedProduct;
        if (scannedProduct = other.GetComponent<Product>())
        { onScan?.Invoke(scannedProduct); }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Product>())
        { scannerLight.enabled = true; }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Product>())
        { scannerLight.enabled = false; }
    }
}
