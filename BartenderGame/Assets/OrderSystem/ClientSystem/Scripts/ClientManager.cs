using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public static ClientManager CM;

    [SerializeField] Transform poolParent;
    [SerializeField] Client clientPrefab;
    [SerializeField] ClientGroup groupPrefab;
    [SerializeField] Transform spawnPoint;

    private ObjectPool<Client> clientPool;
    private ObjectPool<ClientGroup> groupPool;


    void Awake()
    {
        if (CM == null)
            CM = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        clientPool = new ObjectPool<Client>(clientPrefab, poolParent);
        groupPool = new ObjectPool<ClientGroup>(groupPrefab, poolParent);
    }

    public ClientGroup GetGroup(int maxSize)
    {
        ClientGroup group = groupPool.GetObject();

        Client[] clients = new Client[Random.Range(1, maxSize + 1)];

        for(int i = 0; i < clients.Length; i++)
        {
            clients[i] = clientPool.GetObject();
            clients[i].transform.position = spawnPoint.position;
        }

        group.SetClients(clients);

        return group;
    }
}
