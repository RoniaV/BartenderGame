using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGroup : NavMeshCharacterGroup
{
    public bool AvailableForNewOrder { get; private set; }

    private Client[] clients;
    private Table actualTable;

    private int ordersCompleted = 0;
    private bool startOrdering = false;

    void OnEnable()
    {
        AvailableForNewOrder = false;
    }

    public void SetClients(Client[] clients)
    {
        Debug.Log("Set clients for: " + gameObject.name);
        this.clients = clients;

        NavMeshCharacter[] characters = new NavMeshCharacter[clients.Length];

        for(int i = 0; i < clients.Length; i++)
        {
            characters[i] = clients[i].NavMeshCharacter;
        }

        SetCharacters(characters);
    }

    public void AssignTable(Table table)
    {
        Debug.Log(table.gameObject.name + " assigned to " + gameObject.name);

        actualTable = table;
        actualTable.SetClients(this);

        for(int i = 0; i < clients.Length; i++)
        {
            clients[i].SetSeat(actualTable.Seats[i]);
        }

        OnCharactersArrive += ArrivedToTable;
    }

    public void StartOrdering()
    {
        startOrdering = true;
    }

    private void CallWaiter()
    {
        AvailableForNewOrder = false;

        actualTable.WaiterCaller.CallWaiter(OrderManager.OM.GetOrderTime());

        actualTable.WaiterCaller.OnCallStateChange += CheckCallState;
    }

    private void SetNewOrder()
    {
        AvailableForNewOrder = false;

        for (int i = 0; i < clients.Length;i++) 
        {
            clients[i].SetRandomOrder(actualTable.AvailableConsumableTypes, OrderManager.OM.GetOrderTime());

            clients[i].ActualOrder.OnOrderStateChange += CheckOrderState;
        }
    }

    private void CheckCallState(CallState state)
    {
        switch (state)
        {
            case CallState.Completed:
                Debug.Log("Waiter arrived!");
                SetNewOrder();
                break;

            case CallState.Failed:
                Debug.Log("Waiter failed!");
                Failed();
                break;

            case CallState.Canceled:
                break;
        }
    }

    private void CheckOrderState(OrderState state)
    {
        switch(state)
        {
            case OrderState.Completed:
                ordersCompleted++;

                if (ordersCompleted == clients.Length)
                    OrderCompleted();
                break;

            case OrderState.Failed:
                Failed();
                break;

            case OrderState.Canceled:
                break;
        }
    }

    private void ArrivedToTable()
    {
        Debug.Log("Arrived to table: " + actualTable.gameObject.name);

        OnCharactersArrive -= ArrivedToTable;

        if(startOrdering)
            CallWaiter();
    }

    private void OrderCompleted()
    {
        Debug.Log("Order completed!");

        AvailableForNewOrder = true;
    }

    private void Failed()
    {
        Debug.Log("Order failed!");

        AvailableForNewOrder = true;
    }

    private void Canceled()
    {

    }
}
