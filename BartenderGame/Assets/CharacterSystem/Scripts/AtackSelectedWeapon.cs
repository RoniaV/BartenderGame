using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackSelectedWeapon : MonoBehaviour
{
    [SerializeField] WeaponInventory inventory;

    public void Atack()
    {
        inventory?.selectedWeapon?.Atack();
    }

    public void ReleaseAtack()
    {
        inventory?.selectedWeapon?.ReleaseAtack();
    }

    public void SecondaryAtack()
    {
        inventory?.selectedWeapon?.SecondaryAtack();
    }

    public void ReleaseSecondaryAtack()
    {
        inventory?.selectedWeapon?.ReleaseSecondaryAtack();
    }
}
