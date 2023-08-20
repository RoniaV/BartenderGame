using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TableClientManager : MonoBehaviour
{
    public event Action OnClientsArrived;

    public bool HasClients { get { return actualGroup != null; } }
    public NavMeshCharacterGroup ClientGroup { get { return actualGroup; } }

    [SerializeField] int minClients = 1;
    [SerializeField] int maxClient = 4;
    [SerializeField] Transform[] tableSeats;
    [SerializeField] Transform exitPoint;

    private NavMeshCharacterGroup actualGroup;


    public void GetClients()
    {
        actualGroup = ClientManager.CM.GetGroup(maxClient);

        actualGroup.SetDestination(tableSeats);

        actualGroup.OnCharactersArrive += ClientsArrived;
    }

    public void ClientsLeave()
    {
        //actualGroup.SetDestination(exitPoint);
    }

    private void ClientsArrived()
    {
        if (OnClientsArrived != null)
            OnClientsArrived();
    }
}
