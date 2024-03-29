using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] Vector3 holdPos;
    [SerializeField] float stopMargin;
    [SerializeField] Animator anim;

    NavMeshAgent agent;
    Register reg;

    public Vector3 EndPos { get; set; }
    public Product Wanted { get; set; }
    public bool ReachedRegister { get; private set; } = false;

    void Awake() => agent = GetComponent<NavMeshAgent>();

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() =>
        {
            // Reselect product if player picks up the target.
            if (Wanted.IsHeld)
            {
                Wanted.Marked = false;
                if (!(Wanted = Product.GetRandomProduct()))
                {
                    End();
                    return true;
                }
            }

            Vector3 position = new Vector3(Wanted.transform.position.x, transform.position.y, Wanted.transform.position.z);
            if (agent.destination != position)
            { agent.destination = position; }
            return agent.hasPath && Vector3.Distance(transform.position, position) <= agent.stoppingDistance + stopMargin;
        });
        yield return Wanted.FadeAndMove(Wanted.transform.position, transform.TransformPoint(holdPos), true);

        reg = Register.GetClosestRegister(transform.position);
        yield return MoveInQueue(reg.OnCustomerQueue(this));
    }

    public void OnReady() => StartCoroutine(Wanted.FadeAndMove(transform.TransformPoint(holdPos), reg.DropPosition, false));
    public void OnQueueMoved(int index) => StartCoroutine(MoveInQueue(index));
    public void End()
    {
        StopAllCoroutines();
        StartCoroutine(Leave());
    }

    IEnumerator Leave()
    {
        if (Wanted)
        { Wanted.gameObject.SetActive(false); }
        yield return MoveTo(EndPos);
        CustomerSpawner.OnCustomerDestroyed();
        Destroy(gameObject);
    }

    IEnumerator MoveTo(Vector3 position)
    {
        position.y = transform.position.y;
        agent.destination = position;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => agent.hasPath &&
            Vector3.Distance(transform.position, position) <= agent.stoppingDistance + stopMargin);
    }

    IEnumerator MoveInQueue(int index)
    {
        yield return MoveTo(reg.QueuePositions[index]);
        if (index == 0)
        { ReachedRegister = true; }
    }

    void Update() => anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
}
