using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshCharacter), typeof(SeeingPlayer))]
public class NPCRunToDoor : State
{
    [SerializeField] Transform exitDoor = null;

    private NavMeshCharacter navMeshCharacter;
    private SeeingPlayer sight;

    private GameObject lastPlayerValue;

    protected override void Awake()
    {
        base.Awake();
        navMeshCharacter = GetComponent<NavMeshCharacter>();
        sight = GetComponent<SeeingPlayer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log(name + " enter RunToDoor State");
        navMeshCharacter.SetNewTarget(exitDoor, true);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

        if (sight.player != null && lastPlayerValue != sight.player)
            navMeshCharacter.SetNewTarget(exitDoor, true);

        lastPlayerValue = sight.player;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Debug.Log(name + " exit RunToDoor State");
        navMeshCharacter?.SetNewTarget(null);
    }
}
