using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableGroupFinder : MonoBehaviour
{
    public ClientGroup GetAvailableGroup()
    {
        ClientGroup availableGroup = null;

        ClientGroup[] clientGroups = FindObjectsOfType<ClientGroup>();
        ClientGroup[] availableGroups = GetAvailableGroups(clientGroups);

        if (availableGroups.Length > 0)
        {
            availableGroup = availableGroups[ Random.Range(0, availableGroups.Length) ];
        }
        else
        {
            //Ver si hay alguna mesa que no tenga clientes
            Table[] tables = FindObjectsOfType<Table>();
            Table[] availableTables = GetAvailableTables(tables);

            if (availableTables.Length > 0)
            {
                Table availableTable = availableTables[ Random.Range(0, availableTables.Length) ];

                availableGroup = ClientManager.CM.GetGroup(availableTable.Seats.Length);

                availableGroup.AssignTable(availableTable);
            }
            else
                Debug.Log("No more groups available");
        }

        return availableGroup;
    }

    private static Table[] GetAvailableTables(Table[] tables)
    {
        List<Table> availableTables = new List<Table>();

        for (int i = 0; i < tables.Length; i++)
        {
            if (!tables[i].HasClients)
                availableTables.Add(tables[i]);
        }

        return availableTables.ToArray();
    }

    private ClientGroup[] GetAvailableGroups(ClientGroup[] groups)
    {
        List<ClientGroup> availableGroups = new List<ClientGroup>();

        for (int i = 0; i < groups.Length; i++)
        {
            if (groups[i].AvailableForNewOrder)
                availableGroups.Add(groups[i]);
        }

        return availableGroups.ToArray();
    }
}
