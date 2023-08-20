using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrderState
{
    Completed,
    Failed,
    Canceled
}

public class Order
{
    public delegate void OrderStateDelegate(OrderState state);
    public event OrderStateDelegate OnOrderStateChange;

    public bool IsCompleted { get; private set; }
    public Counter Counter { get { return orderCounter; } }
    public ConsumableType[] Objects { get { return objectsToServe.ToArray(); } }

    private List<ConsumableType> objectsToServe = new List<ConsumableType>();
    private Counter orderCounter;

    public Order(ConsumableType[] objects, float t)
    {
        foreach(ConsumableType obj in objects)
        {
            objectsToServe.Add(obj);
        }

        orderCounter = new Counter(t);

        orderCounter.StartCounter();
    }

    public void UpdateOrder(ConsumableType objectServed)
    {
        if (!orderCounter.Finished && objectServed != null)
        {
            foreach (ConsumableType obj in objectsToServe)
            {
                if (obj.name == objectServed.name)
                {
                    objectsToServe.Remove(objectServed);
                    break;
                }
            }

            if (objectsToServe.Count == 0)
            {
                OnOrderStateChange?.Invoke(OrderState.Completed);
                IsCompleted = true;
            }
        }
    }

    public void UpdateCounter()
    {
        if (orderCounter.Finished)
            OnOrderStateChange?.Invoke(OrderState.Failed);
    }

    public void CancelOrder()
    {
        IsCompleted = false;
        orderCounter.ResetCounter();
        objectsToServe.Clear();

        OnOrderStateChange?.Invoke(OrderState.Canceled);
    }
}
