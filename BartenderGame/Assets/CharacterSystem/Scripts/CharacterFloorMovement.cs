using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterGravitable))]
public class CharacterFloorMovement : CharacterMovement
{
    [Tooltip("The maximum slope degree a character can walk")]
    [SerializeField] [Range(0, 90)] float maxSlope = 30f;

    protected CharacterGravitable characterGravitable;

    protected override void Awake()
    {
        base.Awake();
        characterGravitable = GetComponent<CharacterGravitable>();
    }

    protected override void FixedUpdate()
    {
        movementDirection = ProjectMovementOnGround(movementDirection);

        Debug.DrawLine(transform.position, transform.position + (movementDirection * 2), Color.blue);

        base.FixedUpdate();
    }

    protected Vector3 ProjectMovementOnGround(Vector3 movement)
    {
        RaycastHit groundHit = characterGravitable.GetGroundHit();

        if (GroundWalkable(groundHit))
            return Vector3.ProjectOnPlane(movement, groundHit.normal).normalized;

        else
            return movement;
    }

    protected bool GroundWalkable(RaycastHit groundHit)
    {
        float slopeT = Mathf.InverseLerp(0, 90, maxSlope);

        if (groundHit.collider != null)
        {
            Vector3 localNormal = groundHit.normal.normalized;
            bool v = localNormal.y >= 1 - slopeT;
            //Debug.Log("GroundWalkable: " + v + ". Normal X: " + localNormal.x);
            Debug.DrawRay(groundHit.transform.position, localNormal, Color.red);
            return v;
        }
        else
        {
            //Debug.Log("Not grounded");
            return false;
        }
    }
}
