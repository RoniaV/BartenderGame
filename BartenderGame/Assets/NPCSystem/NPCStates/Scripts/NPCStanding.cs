using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshCharacter), typeof(SeeingPlayer))]
public class NPCStanding : State
{
    private NavMeshCharacter navMeshCharacter;
    private SeeingPlayer sight;

    protected override void Awake()
    {
        base.Awake();
        navMeshCharacter = GetComponent<NavMeshCharacter>();
        sight = GetComponent<SeeingPlayer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log(name + " enter Standing State");
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

        navMeshCharacter.SetAimTarget(sight.player != null ? sight.player.transform : null);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Debug.Log(name + " exit Standing State");
        navMeshCharacter.SetAimTarget(null);
    }
}
