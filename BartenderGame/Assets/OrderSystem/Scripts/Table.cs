using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool Available { get { return IsAvailable(); } }
    public bool HasClients { get { return actualClients != null; } }
    public Seat[] Seats { get { return seats; } }
    public ConsumableType[] AvailableConsumableTypes { get { return availableConsumableTypes;   } }
    public WaiterCaller WaiterCaller { get { return waiterCaller;    } }

    [SerializeField] Seat[] seats;
    [SerializeField] ConsumableType[] availableConsumableTypes;
    [SerializeField] WaiterCaller waiterCaller;

    private ClientGroup actualClients;

    public void SetClients(ClientGroup clients)
    {
        actualClients = clients;
    }

    private bool IsAvailable()
    {
        return !HasClients || actualClients.AvailableForNewOrder;
    }

    //Old code 
    //
    //public delegate void OrderDelegate(Order order);
    //public event OrderDelegate OnNewOrder;
    //
    //public bool isOrdering { get; private set; }
    //public float OrderCounter { get { return counter.ActualTime; } }
    //public Order ActualOrder { get { return actualOrder; } }
    //
    //[SerializeField] ConsumableContainer objectContainer;
    //[SerializeField] ConsumableType[] availableTypes;
    //
    //WaiterCaller waiterCaller;
    //TableClientManager clientManager;
    //
    //private Order actualOrder;
    //private Counter counter;
    //
    //void Awake()
    //{
    //    waiterCaller = GetComponent<WaiterCaller>();
    //    clientManager = GetComponent<TableClientManager>();
    //    counter = new Counter();
    //}
    //
    //void OnEnable()
    //{
    //    //objectContainer.OnObjectDroped += UpdateOrder;
    //    waiterCaller.OnWaiterAttends += SetNewOrder;
    //    waiterCaller.OnCallFailed += OrderFailed;
    //}
    //
    //void Update()
    //{
    //    if(actualOrder != null && counter.Finished)
    //    {
    //        OrderFailed();
    //    }
    //}
    //
    //void OnDisable()
    //{
    //    //objectContainer.OnObjectDroped -= UpdateOrder;
    //    waiterCaller.OnWaiterAttends -= SetNewOrder;
    //    waiterCaller.OnCallFailed -= OrderFailed;
    //}
    //
    //public void StartOrdering(float orderTime)
    //{
    //    isOrdering = true;
    //    counter.SetTimeToComplete(orderTime);
    //
    //    Debug.Log("Set new order for " + gameObject.name + " with time: " + counter.TimeToComplete);
    //
    //    if (!clientManager.HasClients)
    //    {
    //        clientManager.GetClients();
    //        clientManager.OnClientsArrived += CallWaiter;
    //    }
    //    else
    //        CallWaiter();
    //}
    //
    //private void CallWaiter()
    //{
    //    waiterCaller.CallWaiter(counter.TimeToComplete);
    //}
    //
    //private void SetNewOrder()
    //{
    //    actualOrder = CreateRandomOrder(counter.TimeToComplete);
    //    counter.StartCounter();
    //
    //    if (OnNewOrder != null)
    //        OnNewOrder(actualOrder);
    //}
    //
    //private void UpdateOrder(ConsumableType objType)
    //{
    //    if (isOrdering)
    //    {
    //        actualOrder.UpdateOrder(objType);
    //
    //        if (actualOrder.isCompleted)
    //            OrderCompleted();
    //    }
    //}
    //
    //private void OrderCompleted()
    //{
    //    Debug.Log(gameObject.name + " order completed");
    //
    //    OrderFinished();
    //}
    //
    //private void OrderFailed()
    //{
    //    Debug.Log(gameObject.name + " order failed");
    //
    //    OrderFinished();
    //}
}
