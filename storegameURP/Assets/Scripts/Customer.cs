using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Vector3 holdPos;
    [SerializeField] private float velocityStopThresh;
    [SerializeField] private float distanceStopThresh;
    [SerializeField] private int maxWanderAmount;
    [SerializeField] private int maxWanderRange;

    private NavMeshAgent agent;
    private Register reg;

    public Vector3 EndPos { get; set; }
    public Product Wanted { get; set; }

    void Awake() => agent = GetComponent<NavMeshAgent>();

    IEnumerator Start()
    {
        for (var i = 0; i < Random.Range(0, maxWanderAmount); i++)
        {
            var wanderPos = Random.onUnitSphere * Random.Range(0, maxWanderRange);
            yield return MoveTo(wanderPos);
        }

        yield return MoveTo(Wanted.transform.position);
        yield return Wanted.FadeAndMove(Wanted.transform.position, transform.TransformPoint(holdPos), true);

        reg = Register.GetClosestRegister(transform.position);

        yield return MoveTo(reg.OnCustomerQueue(this));
    }

    public void OnReady() => StartCoroutine(Wanted.FadeAndMove(transform.TransformPoint(holdPos), reg.DropPosition, false));

    public void OnQueueMoved(Vector3 nextSpot)
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo(nextSpot));
    }

    public void End()
    {
        StopAllCoroutines();
        StartCoroutine(Leave());
    }

    IEnumerator Leave()
    {
        Wanted.gameObject.SetActive(false);
        yield return MoveTo(EndPos);
        Destroy(gameObject);
    }

    IEnumerator MoveTo(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => agent.remainingDistance < distanceStopThresh && agent.velocity.magnitude <= velocityStopThresh);
    }
}
