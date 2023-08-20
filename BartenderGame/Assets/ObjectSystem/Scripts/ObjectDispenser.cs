using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDispenser : MonoBehaviour, ITakeable
{
    [SerializeField] ConsumableObject objectPrefab;

    private ObjectPool<ConsumableObject> consumablesPool;

    void Awake()
    {
        consumablesPool = new ObjectPool<ConsumableObject>(objectPrefab);
    }

    public ConsumableObject TakeObject()
    {
        return consumablesPool.GetObject();
    }
}
