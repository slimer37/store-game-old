using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EntranceTrigger : MonoBehaviour
{
    public System.Action<bool> OnCustomerTrigger;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Customer"))
        { OnCustomerTrigger?.Invoke(true); }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        { OnCustomerTrigger?.Invoke(false); }
    }
}
