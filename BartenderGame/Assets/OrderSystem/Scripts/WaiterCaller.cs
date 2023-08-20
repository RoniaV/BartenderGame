using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CallState
{
    Completed,
    Failed,
    Canceled
}

[RequireComponent(typeof(PlayerCloseness))]
public class WaiterCaller : MonoBehaviour
{
    public event Action OnWaiterCalled;

    public delegate void OrderStateDelegate(CallState state);
    public event OrderStateDelegate OnCallStateChange;

    public bool Calling { get { return calling; } }
    public Counter Counter { get { return counter; } }

    [SerializeField] LayerMask playerLayer;
    [Range(0, 1)]
    [SerializeField] float maxDotProduct = 0.5f;

    private PlayerCloseness playerCloseness;

    private bool calling = false;
    private Counter counter;

    void Awake()
    {
        counter = new Counter();
        playerCloseness = GetComponent<PlayerCloseness>();
    }

    void Update()
    {
        if (calling)
        {
            if (counter.Finished)
                CallFailed();
            else if (playerCloseness.player != null && CheckPlayerDir())
                WaiterAttended();
        }
    }

    public void CallWaiter(float waitTime)
    {
        calling = true;
        counter.SetTimeToComplete(waitTime);
        counter.StartCounter();

        if (OnWaiterCalled != null)
            OnWaiterCalled();
    }

    public void CancelCall()
    {
        EndCall();

        if (OnCallStateChange != null)
            OnCallStateChange(CallState.Canceled);
    }

    private bool CheckPlayerDir()
    {
        Vector3 dirFromPlayer = (transform.position - playerCloseness.player.transform.position).normalized;

        if (Vector3.Dot(dirFromPlayer, playerCloseness.player.transform.forward) >= maxDotProduct)
            return true;
        else
            return false;
    }

    private void WaiterAttended()
    {
        EndCall();

        if (OnCallStateChange != null)
            OnCallStateChange(CallState.Completed);
    }

    private void CallFailed()
    {
        EndCall();

        if (OnCallStateChange != null)
            OnCallStateChange(CallState.Canceled);
    }

    private void EndCall()
    {
        calling = false;
        counter.ResetCounter();
    }
}
