using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerInput))]
public class PlayerMovement : CharacterFloorMovement
{
    protected override void Awake()
    {
        base.Awake();
        //input = GetComponent<MyPlayerInput>();
    }
}
