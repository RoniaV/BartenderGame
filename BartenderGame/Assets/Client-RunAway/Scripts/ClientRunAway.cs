using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SeeingPlayer), typeof(PlayerCloseness))]
public class ClientRunAway : UFSM, IDamageable
{
    [SerializeField] Transform door;
    [SerializeField] float maxDistancePlayerDoor = 5;
    [SerializeField] CheckSomethingIsClose checkDoor;

    private SeeingPlayer sight;
    private PlayerCloseness playerCloseness;

    void Awake()
    {
        sight = GetComponent<SeeingPlayer>();
        playerCloseness = GetComponent<PlayerCloseness>();
    }

    void Update()
    {
        SetBoolParameter("PlayerCloseToDoor", 
            (sight.player != null &&
            Vector3.Distance(sight.player.transform.position, door.position) <= maxDistancePlayerDoor));

        SetBoolParameter("CloseToDoor", 
            checkDoor.interestingTargets?.Length > 0 &&
            checkDoor.interestingTargets[0] != null);

        SetBoolParameter("CloseToPlayer",
            playerCloseness.player != null);
    }

    public void ReceiveDamage()
    {
        SetTriggerParameter("Death");
    }
}
