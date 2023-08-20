using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshCharacter))]
public class NPCIdle : State
{
    [SerializeField] Transform player;

    private NavMeshCharacter navMeshCharacter;

    protected override void Awake()
    {
        base.Awake();
        navMeshCharacter = GetComponent<NavMeshCharacter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log(name + " enter Idle State");
        navMeshCharacter.SetAimTarget(player);
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

        Debug.Log(name + " exit Idle State");
        navMeshCharacter.SetAimTarget(null);
    }
}
