using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerInput))]
public class PlayerAim : CharacterAim
{
    void Awake()
    {
        //input = GetComponent<MyPlayerInput>();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
