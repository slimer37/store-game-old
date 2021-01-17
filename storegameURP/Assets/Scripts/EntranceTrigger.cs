using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EntranceTrigger : MonoBehaviour
{
    [SerializeField] private bool playerTriggers;
    [SerializeField] private bool autoClose;
    [SerializeField] private Openable entrance;

    private bool queueClose = false;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Customer") || playerTriggers && other.CompareTag("Player"))
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
        if (other.CompareTag("Customer") || playerTriggers && other.CompareTag("Player"))
        {
            entrance.SetInteractable(!playerTriggers);

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
