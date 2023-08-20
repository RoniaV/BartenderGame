using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerDash : CharacterDash
{
    private PlayerMovement movement;

    protected override void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        movement.enabled = !isDashing;
    }
}
