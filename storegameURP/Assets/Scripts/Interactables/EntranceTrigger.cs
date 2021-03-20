using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EntranceTrigger : MonoBehaviour
{
    [SerializeField] Openable entrance;
    [SerializeField] bool nonRestrictive;
    [SerializeField] bool playerCanInteract = true;
    [SerializeField] bool autoClose;
    [SerializeField, Min(0.1f)] float closeTime = 0.25f;

    float timeSinceDetection = 0;

    void OnTriggerStay(Collider other)
    {
        if (nonRestrictive && (other.attachedRigidbody || other.CompareTag("Player")) || other.CompareTag("Customer"))
        {
            entrance.SetInteractable(false);
            timeSinceDetection = 0;

            if (!entrance.Open)
            { entrance.Interact(); }
        }
    }

    void Update()
    {
        if (autoClose && entrance.Open && timeSinceDetection > closeTime)
        {
            if (playerCanInteract)
            { entrance.SetInteractable(true); }

            entrance.Interact();
        }
        else
        { timeSinceDetection += Time.deltaTime; }
    }
}
