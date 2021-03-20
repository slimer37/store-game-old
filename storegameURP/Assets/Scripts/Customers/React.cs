using UnityEngine;

public class React : MonoBehaviour
{
    [SerializeField] float projectileVelocityTolerance;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.velocity.sqrMagnitude > projectileVelocityTolerance)
        {
            SendMessage("DecreaseMood");
        }
    }
}
