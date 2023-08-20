using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshCharacter))]
public class Client : MonoBehaviour
{
    public event System.Action<Order> OnNewOrder;

    public NavMeshCharacter NavMeshCharacter { get { return navMeshCharacter; } }
    public Order ActualOrder { get { return actualOrder; } }

    NavMeshCharacter navMeshCharacter;

    private Seat actualSeat;
    private Order actualOrder;

    void Awake()
    {
        navMeshCharacter = GetComponent<NavMeshCharacter>();
    }

    void Update()
    {
        actualOrder?.UpdateCounter();
    }

    public void SetSeat(Seat seat)
    {
        if (actualSeat != seat)
        {
            actualSeat = seat;
            navMeshCharacter.SetNewTarget(seat.ClientSeat);

            seat.ContainerSeat.OnConsumablePutIn += UpdateOrder;
        }
    }

    public void SetRandomOrder(ConsumableType[] availableTypes, float time)
    {
        ConsumableType[] orderConsumables = new ConsumableType[1];
        orderConsumables[0] = availableTypes[Random.Range(0, availableTypes.Length)];

        actualOrder = new Order(orderConsumables, time);

        OnNewOrder(actualOrder);
    }

    private void UpdateOrder(ConsumableObject obj)
    {
        if (actualOrder != null)
            actualOrder.UpdateOrder(obj.Type);
    }
}
