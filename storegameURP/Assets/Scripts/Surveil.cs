using System.Collections;
using UnityEngine;

public class Surveil : MonoBehaviour
{
    [SerializeField] private float turnInterval;
    [SerializeField] private float turnDegrees;
    [SerializeField] private float turnTime;

    IEnumerator Start()
    {
        Vector3 original = transform.localEulerAngles;
        Vector3 spin = Vector3.up * turnDegrees;

        while (true)
        {
            yield return Turn(original + spin);
            yield return Turn(original - spin);
        }

        IEnumerator Turn(Vector3 endEuler)
        {
            yield return Tweens.LerpRotationEuler(transform, endEuler, turnTime, true);
            yield return new WaitForSeconds(turnInterval);
        }
    }
}
