using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ICrouchInput))]
public class PlayerCrouch : CharacterCrouch
{
    protected override void Awake()
    {
        base.Awake();
        //input = GetComponent<ICrouchInput>();
    }
}
