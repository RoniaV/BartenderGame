using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckSomethingIsClose : MonoBehaviour
{
    public Collider[] interestingTargets;

    [SerializeField] float radius = 5f;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] LayerMask interestingLayers = Physics.DefaultRaycastLayers;
    [SerializeField] bool checkThroughWalls = false;
    [SerializeField] LayerMask occludingLayers = Physics.DefaultRaycastLayers;
    [SerializeField] bool manualCheck = false;
    [SerializeField] float refreshFrequency = 10;   // Times per second

    private IEnumerator coroutine;

    protected virtual void Awake()
    {
        coroutine = CheckUpdate();
    }

    void OnEnable()
    {
        if (!manualCheck)
            StartCoroutine(coroutine);
    }

    void OnDisable()
    {
        if (!manualCheck)
            StopCoroutine(coroutine);
    }

    public void CheckCloseness()
    {
        Collider[] collidersInRange =
            Physics.OverlapSphere(
            transform.position + offset,
            radius,
            interestingLayers
            );

        List<Collider> interestingTargetsList = new List<Collider>();
        foreach (Collider c in collidersInRange)
        {
            if (
                !checkThroughWalls &&
                !Physics.Linecast(transform.position + offset, c.transform.position, occludingLayers)
                )
            {
                interestingTargetsList.Add(c);
            }
            else if (checkThroughWalls)
                interestingTargetsList.Add(c);
        }

        interestingTargets = interestingTargetsList.ToArray();
    }

    public void SetManualCheck(bool manual)
    {
        if(manual != manualCheck)
        {
            if (manual)
            {
                Debug.Log("Set manual check");
                StopCoroutine(coroutine);
            }
            else
            {
                Debug.Log("Set automatic check");
                StartCoroutine(coroutine);
            }
        }

        manualCheck = manual;
    }

    private IEnumerator CheckUpdate()
    {
        while (true)
        {
            CheckCloseness();

            yield return new WaitForSeconds(1f / refreshFrequency);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enabled)
            Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
