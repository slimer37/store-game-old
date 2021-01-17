using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EntranceTrigger : MonoBehaviour
{
    public System.Action<bool> OnCustomerTrigger;

    [SerializeField] private bool playerTriggers;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Customer") || playerTriggers && other.CompareTag("Player"))
        { OnCustomerTrigger?.Invoke(true); }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer") || playerTriggers && other.CompareTag("Player"))
        { OnCustomerTrigger?.Invoke(false); }
    }
}
