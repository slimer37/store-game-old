using System.Collections;
using UnityEngine;

public class DriveBy : MonoBehaviour
{
    [SerializeField] Vector3[] nodes;
    [SerializeField] float startDelay;
    [Header("Duration")]
    [SerializeField] float minDuration;
    [SerializeField] float maxDuration;
    [Header("Interval")]
    [SerializeField] float minInterval;
    [SerializeField] float maxInterval;

    [Header("Appearance")]
    [SerializeField] bool isCustomer;

    FeatureRandomizer[] randomizers;
    Animator anim;
    GameObject[] children;

    void Awake()
    {
        children = new GameObject[transform.childCount];
        var i = 0;
        foreach (var child in transform)
        {
            children[i] = ((Transform)child).gameObject;
            i++;
        }

        if (isCustomer)
        { TryGetComponent(out anim); }

        randomizers = GetComponentsInChildren<FeatureRandomizer>();
    }

    IEnumerator Start()
    {
        SetChildrenActive(false);
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            foreach (var randomizer in randomizers)
            { randomizer.Randomize(); }
            if (isCustomer)
            { anim.SetFloat("Speed", 1); }
            yield return DriveRoute(nodes);
        }
    }

    IEnumerator DriveRoute(Vector3[] nodes)
    {
        SetChildrenActive(true);

        float duration = Random.Range(minDuration, maxDuration);
        transform.position = new Vector3(nodes[0].x, transform.position.y, nodes[0].z);
        transform.LookAt(new Vector3(nodes[1].x, transform.position.y, nodes[1].z));

        float totalDist = 0;
        for (int i = 1; i < nodes.Length; i++)
        { totalDist += Vector3.Distance(nodes[i - 1], nodes[i]); }

        for (int i = 1; i < nodes.Length - 1; i++)
        { yield return DriveNodes(nodes[i - 1], nodes[i], nodes[i + 1], i == 1 ? 1 : -1); }
        yield return DriveNodes(nodes[nodes.Length - 2], nodes[nodes.Length - 1], nodes[nodes.Length - 1], 0);

        SetChildrenActive(false);
        yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

        IEnumerator DriveNodes(Vector3 node1, Vector3 node2, Vector3 face, int size = -1)
        {
            var partialDist = Vector3.Distance(node1, node2);
            var adjustedDuration = duration * (partialDist / totalDist);

            var destination = new Vector3(node2.x, transform.position.y, node2.z);
            var direction = face - transform.position;

            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            yield return Tweens.LerpValue(adjustedDuration, t =>
            {
                transform.position = Vector3.Lerp(startPos, destination, t);
                transform.rotation = Quaternion.Lerp(startRot, Quaternion.LookRotation(direction), t);

                if (size != -1)
                { transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * size, t); }
            });
        }
    }

    void SetChildrenActive(bool value)
    {
        foreach (var child in children)
        { child.SetActive(value); }
    }
}
