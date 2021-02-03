using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Vector3 ignitionPoint;
    [SerializeField] private bool igniteOnStart;

    [Header("Growth / Decay")]
    [SerializeField] private float growthRate;
    [SerializeField] private float maxGrowth;
    [SerializeField] private float decayRate;

    [Header("Spread")]
    [SerializeField] private float spreadRadius;
    [SerializeField] private float minSpreadTime;
    [SerializeField] private float maxSpreadTime;

    private bool started = false;
    private Collider[] detected = new Collider[5];

    public static List<Transform> Fires { get; private set; } = new List<Transform>();
    public static Transform DecayingFire { get; set; }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => igniteOnStart || started);
        StartFireAt(ignitionPoint);
        started = true;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpreadTime, maxSpreadTime));

            foreach (var fire in Fires)
            {
                if (Physics.OverlapSphereNonAlloc(fire.position, spreadRadius, detected) > 0)
                {
                    foreach (var col in detected)
                    {
                        if (col.gameObject.isStatic)
                        { StartFireAt(detected[0].ClosestPoint(fire.position)); }
                    }
                }
            }
        }

        void StartFireAt(Vector3 position) => Fires.Add(Instantiate(firePrefab, position, Quaternion.identity).transform);
    }

    public void Ignite() => started = true;

    void FixedUpdate()
    {
        if (!started) return;

        foreach (var fire in Fires)
        {
            if (fire == DecayingFire)
            {
                if (fire.localScale.sqrMagnitude > 0)
                { fire.localScale -= Vector3.one * decayRate * Time.fixedDeltaTime; }
                else
                {
                    Fires.Remove(fire);
                    Destroy(fire);
                }
            }
            else
            {
                if (fire.localScale.sqrMagnitude / 3 < maxGrowth * maxGrowth)
                { fire.localScale += Vector3.one * growthRate * Time.fixedDeltaTime; }
            }
        }
    }
}
