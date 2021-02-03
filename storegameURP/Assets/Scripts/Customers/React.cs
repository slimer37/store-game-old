using UnityEngine;

public class React : MonoBehaviour
{
    [SerializeField] private float projectileVelocityTolerance;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.sqrMagnitude > projectileVelocityTolerance)
        {
            SendMessage("DecreaseMood");
        }
    }
}
