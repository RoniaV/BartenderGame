using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotNoticer : MonoBehaviour
{
    [SerializeField] UFSM[] NPCsFSM;
    [SerializeField] WeaponInventory inventory;

    private WeaponBase lastWeapon;
    private bool noticed;

    void OnEnable()
    {
        if (inventory != null)
            inventory.OnWeaponSelected += SubscribeToWeapon;
    }

    void OnDisable()
    {
        if (inventory != null)
            inventory.OnWeaponSelected -= SubscribeToWeapon;
    }

    private void SubscribeToWeapon(WeaponBase weapon)
    {
        weapon.OnAtack += NoticeShot;

        if (lastWeapon != null && lastWeapon != weapon)
        {
            lastWeapon.OnAtack -= NoticeShot;
        }

        lastWeapon = weapon;
    }

    private void NoticeShot()
    {
        if (!noticed)
        {
            for (int i = 0; i < NPCsFSM.Length; i++)
            {
                Debug.Log("Shot noticed");
                NPCsFSM[i].SetTriggerParameter("Shot");
            }

            noticed = true;
        }
    }
}
