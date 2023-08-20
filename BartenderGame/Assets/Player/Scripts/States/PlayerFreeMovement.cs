using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeMovement : State
{
    [SerializeField] WeaponInventory inventory;

    private MyPlayerInput input;

    private PlayerMovement pMovement;
    private PlayerAim pAim;
    private PlayerJump pJump;
    private AtackSelectedWeapon atackSelectedWeapon;

    protected override void Awake()
    {
        input = GetComponent<MyPlayerInput>();

        pMovement = GetComponent<PlayerMovement>();
        pAim = GetComponent<PlayerAim>();
        pJump = GetComponent<PlayerJump>();
        atackSelectedWeapon = GetComponent<AtackSelectedWeapon>();

        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

        pMovement.SetMovementDirection(input.GetMovementInput());
        pAim.RotateCharacter(input.GetAimInput());

        if (input.GetJumpInput())
            pJump.Jump();

        if (input.GetUtilInput())
            fSM.SetBoolParameter("Dashing", true);

        if (input.GetCrouchInput())
            fSM.SetBoolParameter("Crouched", true);

        if (input.GetAtackInput())
            atackSelectedWeapon.Atack();
        else if (input.GetAtackReleaseInput())
            atackSelectedWeapon.ReleaseAtack();

        if (input.GetSecondaryAtackInput())
            atackSelectedWeapon.SecondaryAtack();
        else if (input.GetSecondaryAtackReleaseInput())
            atackSelectedWeapon.ReleaseSecondaryAtack();

        if (input.GetChangeWeaponInput())
            inventory.ChangeWeapon(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        pMovement.SetMovementDirection(Vector3.zero);
        pAim.RotateCharacter(Vector2.zero);
        atackSelectedWeapon.ReleaseAtack();
    }
}
