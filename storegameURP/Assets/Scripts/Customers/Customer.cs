using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Vector3 holdPos;
    [SerializeField] private int maxWanderAmount;
    [SerializeField] private int maxWanderRange;
    [SerializeField] private Animator anim;

    private NavMeshAgent agent;
    private Register reg;

    public Vector3 EndPos { get; set; }
    public Product Wanted { get; set; }
    public bool ReachedRegister { get; private set; } = false;

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

        int index = reg.OnCustomerQueue(this);
        yield return MoveTo(reg.QueuePositions[index]);

        if (index == 0)
        {ReachedRegister = true;}
    }

    public void OnReady() => StartCoroutine(Wanted.FadeAndMove(transform.TransformPoint(holdPos), reg.DropPosition, false));

    public void OnQueueMoved(int index)
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo(reg.QueuePositions[index]));
        if (index == 0)
        { ReachedRegister = true; }
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
        CustomerSpawner.OnCustomerDestroyed();
        Destroy(gameObject);
    }

    IEnumerator MoveTo(Vector3 position)
    {
        agent.destination = position;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => agent.hasPath && Vector3.Distance(transform.position, Wanted.transform.position) <= agent.stoppingDistance);
    }

    void Update() => anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
}
