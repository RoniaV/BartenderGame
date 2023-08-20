using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ConsumableObject : MonoBehaviour, ITakeable
{
    public ConsumableType Type { get { return type; } }
    public bool Empty { get { return empty; } }

    [SerializeField] ConsumableType type;

    private bool empty;

    private Collider collider;
    private Rigidbody rb;

    void Awake()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        Reset();
    }

    public ConsumableObject TakeObject()
    {
        return this;
    }

    public void Picked(Transform pickPoint)
    {
        transform.position = pickPoint.position;
        transform.rotation = pickPoint.rotation;

        transform.parent = pickPoint;
        collider.enabled = false;
        rb.isKinematic = true;
    }

    public void Dropped(Transform dropPoint)
    {
        transform.position = dropPoint.position;
        transform.rotation = dropPoint.rotation;

        Reset();
    }

    public void ConsumeObject()
    {
        empty = true;
    }

    public void RefillObject()
    {
        empty = false;
    }

    private void Reset()
    {
        transform.parent = null;
        collider.enabled = true;
        rb.isKinematic = false;
    }
}