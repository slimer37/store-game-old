using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Register : Interactable
{
    [Header("Text")]
    [SerializeField] private TextMeshPro screenMainText;
    [SerializeField] private string pendingText;
    [SerializeField] private TextMeshPro screenSideText;
    [SerializeField] private ParticleSystem particles;

    [Header("Other")]
    [SerializeField] private Transform itemDrop;

    private static List<Register> allRegisters = new List<Register>();

    private List<Customer> queue = new List<Customer>();

    private Customer currentCustomer = null;
    private List<Product> receipt = new List<Product>();
    private Scanner scanner;

    private bool CustomerPending => queue.Count > 0 && !currentCustomer && queue[0].ReachedRegister;

    protected override CursorIcon.Icon HoverIcon => CustomerPending ? CursorIcon.Icon.Access : CursorIcon.Icon.None;
    public Vector3 DropPosition => itemDrop.position;
    public Vector3[] QueuePositions { get; private set; }

    public static Register GetClosestRegister(Vector3 origin)
    {
        float closestDist = 0;
        int closestReg = 0;
        for (int i = 0; i < allRegisters.Count; i++)
        {
            var registerDist = Vector3.Distance(origin, allRegisters[i].transform.position);
            if (closestDist == 0 || registerDist < closestDist)
            {
                closestDist = registerDist;
                closestReg = i;
            }
        }
        return allRegisters[closestReg];
    }

    void Awake()
    {
        allRegisters.Add(this);
        if (scanner = GetComponentInChildren<Scanner>())
        { scanner.onScan += EnterItem; }
        QueuePositions = QueuePositioning.GenerateQueue(this, Level.Current.Capacity);
    }

    public int OnCustomerQueue(Customer customer)
    {
        queue.Add(customer);
        return queue.Count - 1;
    }

    void Update()
    {
        if (CustomerPending)
        { screenMainText.text = pendingText; }
    }

    // Process next customer
    public override void Interact()
    {
        if (CustomerPending)
        {
            currentCustomer = queue[0];
            currentCustomer.OnReady();
        }
    }

    void EnterItem(Product scanned)
    {
        screenMainText.text = scanned.Info.DisplayString;

        if (!currentCustomer || currentCustomer.Wanted != scanned) return;

        UpdateReceipt(scanned);
        EndOrder();
    }

    void EndOrder()
    {
        Level.Current.Money += receipt[0].Info.Price;
        receipt.Clear();

        particles.Play();

        currentCustomer.End();
        currentCustomer = null;
        queue.RemoveAt(0);
        for (int i = 0; i < queue.Count; i++)
        { queue[i].OnQueueMoved(i); }
    }

    void UpdateReceipt(Product item)
    {
        receipt.Add(item);

        string text = "";
        float total = 0;
        for (int i = 0; i < receipt.Count; i++)
        {
            if (i > 0) { text += "\n"; }
            text += receipt[i].Info.ReceiptPhrase;
            total += receipt[i].Info.Price;
        }
        text += $"\nTotal: {total:c}";
        screenSideText.text = text;
    }
}
