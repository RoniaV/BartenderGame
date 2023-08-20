using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AvailableGroupFinder))]
public class OrderManager : MonoBehaviour
{
    public static OrderManager OM;

    public event Action OnOrderFailed;

    [SerializeField] int minComands = 1;
    [Tooltip("Time to add a new order")]
    [SerializeField] float timeToNewOrder = 20;
    [Tooltip("Time for the first order to be completed")]
    [SerializeField] float baseOrderTime = 10;
    [Range(1, 2)]
    [SerializeField] float log = 1.05f;
#if UNITY_EDITOR
    [Header("Debug Settings")]
    [SerializeField] bool startOrdering = false;
#endif

    AvailableGroupFinder groupFinder;

    private ClientGroup[] clientGroups;
    private int actualOrders;
    private IEnumerator orderCoroutine;

    void Awake()
    {
        groupFinder = GetComponent<AvailableGroupFinder>();
        orderCoroutine = AddOrderCoroutine();

        if (OM == null)
            OM = this;
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        //foreach(ClientGroup group in clientGroups)
        //{
            //table.OnOrderCompleted += OrderCompleted;
            //table.OnOrderFailed += OrderFailed;
        //}
    }

    void Update()
    {
#if UNITY_EDITOR
        if (startOrdering)
        {
            StartOrdering();
            startOrdering = false;
        }
#endif
    }

    void OnDisable()
    {
        //foreach (ClientGroup group in clientGroups)
        //{
            //table.OnOrderCompleted -= OrderCompleted;
            //table.OnOrderFailed -= OrderFailed;
        //}
    }

    public void StartOrdering()
    {
        Debug.Log("Start ordering!");

        for(int i = 0; i < minComands; i++)
        {
            SetNewOrder();
        }

        //StartCoroutine(orderCoroutine);
    }

    public float GetOrderTime()
    {
        float orderTime = Mathf.Log(actualOrders, log) + baseOrderTime;

        return orderTime;
    }

    public void StopAllOrders()
    {
        Debug.Log("Stop ordering!");

        //for (int i = 0; i < clientGroups.Length; i++)
        //{
        //    //tables[i].CancelActualOrder();
        //}

        StopCoroutine(orderCoroutine);
    }

    private void OrderCompleted(Order order)
    {
        Debug.Log("Order completed, setting new order");

        actualOrders--;
        SetNewOrder();
    }

    private void OrderFailed(Order order)
    {
        Debug.Log("Order failed, setting new order");

        actualOrders--;
        SetNewOrder();

        OnOrderFailed?.Invoke();
    }

    private IEnumerator AddOrderCoroutine()
    {
        while(actualOrders < clientGroups.Length)
        {
            yield return new WaitForSeconds(timeToNewOrder);

            Debug.Log("Added new order!");
            SetNewOrder();
        }

        yield return null;
    }

    private void SetNewOrder()
    {
        ClientGroup availableGroup = groupFinder.GetAvailableGroup();

        actualOrders++;
        if (availableGroup != null) 
        {
            Debug.Log("New order set for: " + availableGroup.gameObject.name);
            availableGroup.StartOrdering();
        }
    }
}
