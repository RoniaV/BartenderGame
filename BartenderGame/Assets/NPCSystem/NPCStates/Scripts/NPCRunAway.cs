using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshCharacter), typeof(PlayerCloseness))]
public class NPCRunAway : State
{
    [SerializeField] float minStepLength = 2;
    [SerializeField] int raysToCheck = 16;
    [SerializeField] LayerMask obstacleLayers;
    [SerializeField] float minPlayerDistance = 4;
    [SerializeField] Transform door;

    private NavMeshCharacter navMeshCharacter;
    private PlayerCloseness playerCloseness;

    protected override void Awake()
    {
        base.Awake();
        navMeshCharacter = GetComponent<NavMeshCharacter>();
        playerCloseness = GetComponent<PlayerCloseness>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log(name + " enter RunAway State");
        SetTargetPoint();

        navMeshCharacter.OnEndOfPathReached += CheckIfPlayerIsClose;
        //playerCloseness.SetManualCheck(true);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Debug.Log(name + " exit RunAway State");

        navMeshCharacter?.SetNewTarget(null);
        navMeshCharacter.OnEndOfPathReached -= CheckIfPlayerIsClose;
        //playerCloseness.SetManualCheck(false);
    }

    private void CheckIfPlayerIsClose()
    {
        playerCloseness.CheckCloseness();

        if (playerCloseness.player != null)
            SetTargetPoint();
    }

    private void SetTargetPoint()
    {
        Debug.Log("Set target point");
        Vector3 targetPoint = Vector3.zero;

        Vector3 dir = GetDirection();

        if (dir != Vector3.zero)
        {
            Debug.Log("Direction: " + dir);
            targetPoint = transform.position + dir * minStepLength;
        }

        navMeshCharacter.SetNewTarget(targetPoint, true);
    }

    private Vector3 GetDirection()
    {
        Debug.Log("Get direction");
        Vector3[] availableDirs = GetAvailableDirections();
        Vector3 desiredDir = GetDesiredDirection();

        return GetOptimalDirection(availableDirs, desiredDir);
    }

    private Vector3 GetOptimalDirection(Vector3[] availableDirections, Vector3 desiredDirection)
    {
        Vector3 optimalDir = new Vector3();

        //Get all the dot products of the available directions and the desired direction
        float[] dotProducts = new float[availableDirections.Length];

        for (int i = 0; i < availableDirections.Length; i++)
        {
            dotProducts[i] = Vector3.Dot(desiredDirection, availableDirections[i]);
        }

        //Get the highest dot product from the array
        float highestDotProduct = dotProducts[0];
        optimalDir = availableDirections[0];

        for (int e = 0; e < dotProducts.Length; e++)
        {
            if (dotProducts[e] > highestDotProduct)
            {
                highestDotProduct = dotProducts[e];
                optimalDir = availableDirections[e];
            }
        }

        Debug.Log("Highest Dot Product: " + highestDotProduct);
        Debug.Log("Optimal direction: " + optimalDir);
        return optimalDir;
    }

    private Vector3[] GetAvailableDirections()
    {
        List<Vector3> availableDirections = new List<Vector3>();

        for (int i = 0; i < raysToCheck; i++)
        {
            Vector3 rayDir = (Quaternion.AngleAxis((360 / raysToCheck) * i, Vector3.up) * Vector3.forward).normalized;

            if (!Physics.Raycast(transform.position, rayDir, minStepLength, obstacleLayers))
            {
                Debug.DrawRay(transform.position, rayDir * minStepLength, Color.green, 2f);
                availableDirections.Add(rayDir);
            }
            else
                Debug.DrawRay(transform.position, rayDir * minStepLength, Color.red, 2f);
        }

        return availableDirections.ToArray();
    }

    private Vector3 GetDesiredDirection()
    {
        Vector3 desiredDir = new Vector3();

        if (Vector3.Distance(playerCloseness.player.position, transform.position) < minPlayerDistance)
            desiredDir = transform.position - playerCloseness.player.position;
        else
        {
            Vector3 dirToDoor = (door.position - transform.position).normalized;
            Vector3 dirFromPlayer = (transform.position - playerCloseness.player.position).normalized;

            desiredDir = dirToDoor + dirFromPlayer;
        }

        desiredDir.y = 0;
        desiredDir = desiredDir.normalized;

        Debug.Log("Desired direction: " + desiredDir);
        return desiredDir;
    }


    //private Vector3 GetDirection()
    //{
    //    Vector3 desiredDir = transform.position - playerCloseness.player.position;
    //    desiredDir.y = 0;
    //    desiredDir = desiredDir.normalized;

    //    return GetAvailableDirection(desiredDir);
    //}

    //private Vector3 GetAvailableDirection(Vector3 desiredDirection)
    //{
    //    Vector3 rightDirection = (Quaternion.AngleAxis(checkAngle, Vector3.up) * desiredDirection).normalized;
    //    Vector3 leftDirection = (Quaternion.AngleAxis(-checkAngle, Vector3.up) * desiredDirection).normalized;

    //    if (!Physics.Raycast(transform.position, desiredDirection, minStepLength, wallLayer))
    //    {
    //        Debug.DrawRay(transform.position, desiredDirection * minStepLength, Color.green, 2);
    //        return desiredDirection * minStepLength;
    //    }
    //    else if (!Physics.Raycast(transform.position, rightDirection, minStepLength, wallLayer))
    //    {
    //        Debug.DrawRay(transform.position, rightDirection * minStepLength, Color.green, 2);
    //        return rightDirection * minStepLength;
    //    }
    //    else if (!Physics.Raycast(transform.position, leftDirection, minStepLength, wallLayer))
    //    {
    //        Debug.DrawRay(transform.position, leftDirection * minStepLength, Color.green, 2);
    //        return leftDirection * minStepLength;
    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, desiredDirection * minStepLength, Color.red, 2);
    //        return Vector3.zero;
    //    }
    //}
}
