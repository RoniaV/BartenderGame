using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerInput))]
public class PlayerJump : CharacterJump
{
    protected override void Awake()
    {
        base.Awake();
        //input = GetComponent<MyPlayerInput>();
    }
}
