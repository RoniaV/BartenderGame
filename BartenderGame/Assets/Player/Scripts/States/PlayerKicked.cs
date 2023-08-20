using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterKickable), typeof(MyPlayerInput))]
public class PlayerKicked : State, IKickable
{
    private MyPlayerInput input;

    private CharacterKickable characterKickable;

    private Vector3 kickDirection;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<MyPlayerInput>();
        characterKickable = GetComponent<CharacterKickable>();
    }

    protected override void OnEnable()             
    {
        base.OnEnable();
        characterKickable.Kick(kickDirection);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
        fSM.SetBoolParameter("Kicked", characterKickable.isBeingKicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        kickDirection = Vector3.zero;
    }

    public void ReceiveKick(Vector3 direction)
    {
        kickDirection = direction;
        fSM.SetBoolParameter("Kicked", true);
    }
}
