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
            Quaternion startRot = transform.localRotation;
            Quaternion endRot = Quaternion.Euler(endEuler);
            for (float t = 0; t < 1; t += Time.deltaTime / turnTime)
            {
                transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
                yield return null;
            }
            transform.localRotation = endRot;
            yield return new WaitForSeconds(turnInterval);
        }
    }
}
