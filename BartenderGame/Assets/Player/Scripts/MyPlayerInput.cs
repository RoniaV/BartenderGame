using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MyPlayerInput : MonoBehaviour
{
    [Header("Input Actions Names")]
    [SerializeField] string moveAcName = "Move";
    [SerializeField] string aimAcName = "Aim";
    [SerializeField] string jumpAcName = "Jump";
    [SerializeField] string atackAcName = "Atack";
    [SerializeField] string runAcName = "Run";
    [SerializeField] string secondaryAtackAcName = "SecondaryAtack";
    [SerializeField] string crouchAcName = "Crouch";
    [SerializeField] string utilAcName = "Util";
    [SerializeField] string changeWeaponAcName = "ChangeWeapon";

    PlayerInput playerInput;

    private InputAction moveAction, aimAction, jumpAction, atackAction, changeWeaponAction, utilAction, crouchAction, secondaryAtackAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions[moveAcName];
        aimAction = playerInput.actions[aimAcName];
        jumpAction = playerInput.actions[jumpAcName];
        atackAction = playerInput.actions[atackAcName];
        changeWeaponAction = playerInput.actions[changeWeaponAcName];
        utilAction = playerInput.actions[utilAcName];
        crouchAction = playerInput.actions[crouchAcName];
        secondaryAtackAction = playerInput.actions[secondaryAtackAcName];
    }

    public Vector3 GetMovementInput()
    {
        Vector2 inputValue = moveAction.ReadValue<Vector2>();

        Vector3 fixedValue = new Vector3(inputValue.x, 0, inputValue.y);
        fixedValue = transform.TransformDirection(fixedValue).normalized;
        return fixedValue;
    }

    public Vector2 GetAimInput()
    {
        return aimAction.ReadValue<Vector2>();
    }

    public bool GetAtackInput()
    {
        return atackAction.IsPressed();
    }

    public bool GetAtackReleaseInput()
    {
        return atackAction.WasReleasedThisFrame();
    }

    public bool GetChangeWeaponInput()
    {
        return changeWeaponAction.WasPerformedThisFrame();
    }

    public bool GetJumpInput()
    {
        return jumpAction.WasPerformedThisFrame();
    }

    public bool GetUtilInput()
    {
        return utilAction.WasPerformedThisFrame();
    }

    public bool GetCrouchInput()
    {
        return crouchAction.WasPerformedThisFrame();
    }

    public bool GetSecondaryAtackInput()
    {
        return secondaryAtackAction.IsPressed();
    }

    public bool GetSecondaryAtackReleaseInput()
    {
        return secondaryAtackAction.WasReleasedThisFrame();
    }
}
