using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableContainer : MonoBehaviour, IContainer, ITakeable
{
    public delegate void ObjectDelegate(ConsumableObject obj);
    public event ObjectDelegate OnConsumablePutIn;

    [SerializeField] Transform[] dropPoints;

    private int maxObjects;
    private List<ConsumableObject> actualObjects = new List<ConsumableObject>();

    void Start()
    {
        maxObjects = dropPoints.Length;
    }

    public bool IsFull()
    {
        if (actualObjects.Count < maxObjects)
            return false;
        else
            return true;
    }

    public bool PutInObject(ConsumableObject objectToPutIn)
    {
        if ( objectToPutIn != null && !IsFull())
        {
            actualObjects.Add(objectToPutIn);
            objectToPutIn.Picked(dropPoints[actualObjects.Count - 1]);

            if (OnConsumablePutIn != null)
                OnConsumablePutIn(objectToPutIn);

            return true;
        }
        else
            return false;
    }

    // Puede que en un futuro se quiera poner un objeto en una posición concreta, pero por ahora no se usa
    //
    //public bool PutInObject(ConsumableObject objectToPutIn, Transform dropPoint)
    //{
    //    if (objectToPutIn != null && !IsFull())
    //    {
    //        actualObjects.Add(objectToPutIn);
    //        objectToPutIn.Picked(dropPoint);

    //        if (OnConsumablePutIn != null)
    //            OnConsumablePutIn(objectToPutIn);

    //        return true;
    //    }
    //    else
    //        return false;
    //}

    public ConsumableObject TakeObject()
    {
        if (actualObjects.Count == 0) return null;

        ConsumableObject objectToPick = actualObjects[actualObjects.Count - 1];
        actualObjects.RemoveAt(actualObjects.Count - 1);

        return objectToPick;
    }

    public void RemoveAllObjects()
    {
        foreach (ConsumableObject obj in actualObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
