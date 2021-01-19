using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EntranceTrigger : MonoBehaviour
{
    [SerializeField] private bool nonRestrictive;
    [SerializeField] private bool autoClose;
    [SerializeField] private Openable entrance;

    private bool queueClose = false;

    void OnTriggerStay(Collider other)
    {
        if (nonRestrictive && (other.attachedRigidbody || other.CompareTag("Player")) || other.CompareTag("Customer"))
        {
            entrance.SetInteractable(false);

            if (!entrance.Open)
            {
                entrance.Interact();
                queueClose = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (nonRestrictive && (other.attachedRigidbody || other.CompareTag("Player")) || other.CompareTag("Customer"))
        {
            entrance.SetInteractable(!nonRestrictive);

            if (autoClose)
            { queueClose = true; }
        }
    }

    void Update()
    {
        if (entrance.Open && queueClose)
        { entrance.Interact(); }
    }
}
