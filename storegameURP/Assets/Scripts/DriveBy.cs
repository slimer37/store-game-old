using System.Collections;
using UnityEngine;

public class DriveBy : MonoBehaviour
{
    [SerializeField] private Vector3 point1;
    [SerializeField] private Vector3 point2;
    [SerializeField] private float startDelay;
    [Header("Duration")]
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    [Header("Interval")]
    [SerializeField] private float minInterval;
    [SerializeField] private float maxInterval;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            yield return LerpTo(point2, 1);
            yield return LerpTo(point1, -1);
        }
    }

    IEnumerator LerpTo(Vector3 point, int turnMultiplier)
    {
        SetChildrenActive(true);
        yield return Tweens.LerpLocation(transform, point, Random.Range(minDuration, maxDuration));
        SetChildrenActive(false);
        yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        transform.eulerAngles = transform.eulerAngles + Vector3.up * 180 * turnMultiplier;

        void SetChildrenActive(bool value)
        {
            for (int i = 0; i < transform.childCount; i++)
            { transform.GetChild(i).gameObject.SetActive(value); }
        }
    }
}
