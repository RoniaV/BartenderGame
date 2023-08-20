using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : State
{
    private MyPlayerInput input;

    private PlayerDash pDash;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<MyPlayerInput>();
        pDash = GetComponent<PlayerDash>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        pDash.Dash(input.GetMovementInput());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

        fSM.SetBoolParameter("Dashing", pDash.isDashing);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        pDash?.StopDash();
    }
}
