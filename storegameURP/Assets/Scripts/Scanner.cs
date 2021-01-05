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

    void OnTriggerStay(Collider other) => scannerLight.enabled = true;
    void OnTriggerExit(Collider other) => scannerLight.enabled = false;
}
