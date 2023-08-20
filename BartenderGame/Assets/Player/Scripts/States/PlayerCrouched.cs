using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAim), typeof(PlayerCrouch))]
public class PlayerCrouched : State
{
    [SerializeField] WeaponInventory inventory;

    private MyPlayerInput input;

    private PlayerMovement pMovement;
    private PlayerAim pAim;
    private PlayerCrouch pCrouched;
    private AtackSelectedWeapon atackSelectedWeapon;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<MyPlayerInput>();

        pMovement = GetComponent<PlayerMovement>();
        pAim = GetComponent<PlayerAim>();
        pCrouched = GetComponent<PlayerCrouch>();
        atackSelectedWeapon = GetComponent<AtackSelectedWeapon>();
    }

    protected override void OnEnable()
    {
        pCrouched.ChangeCrouchState();
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        pMovement.SetMovementDirection(input.GetMovementInput());
        pAim.RotateCharacter(input.GetAimInput());

        if(input.GetCrouchInput())
        {
            pCrouched.ChangeCrouchState();
        }

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

        fSM.SetBoolParameter("Crouched", pCrouched.Crouched);

        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        pMovement.SetMovementDirection(Vector3.zero);
        pAim.RotateCharacter(Vector2.zero);
        atackSelectedWeapon?.ReleaseAtack();
    }
}
