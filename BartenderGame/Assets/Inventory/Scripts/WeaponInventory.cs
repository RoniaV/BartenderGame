using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IChangeWeaponInput))]
public class WeaponInventory : MonoBehaviour
{
    public delegate void WeaponDelegate(WeaponBase weapon);
    public event WeaponDelegate OnWeaponSelected;

    public WeaponBase selectedWeapon { get; private set; }

    [SerializeField] List<WeaponBase> weapons = new List<WeaponBase>();
    [SerializeField] Transform weaponPoint;
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] bool changeWeapon;
    [SerializeField] bool up;
#endif

    private int wIndex = 0;

    void Start()
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != null)
            {
                PositionWeapon(weapons[i].transform);
                weapons[i].gameObject.SetActive(false);
            }
            else
                weapons.RemoveAt(i);
        }

        SelectWeapon();
    }

    void Update()
    {
#if UNITY_EDITOR
        if(changeWeapon)
        {
            changeWeapon = false;
            ChangeWeapon(up);
        }
#endif
    }

    public void ChangeWeapon(bool up)
    {
        wIndex += up ? 1 : -1;

        if (wIndex >= weapons.Count)
            wIndex = 0;
        else if (wIndex < 0)
            wIndex = weapons.Count - 1;

        SelectWeapon();
    }

    public void AddWeapon(WeaponBase weapon)
    {
        weapons.Add(weapon);

        SelectWeapon();
    }

    public void RemoveWeapon(WeaponBase weapon)
    {
        weapons.Remove(weapon);
        Destroy(weapon.gameObject);

        SelectWeapon();
    }

    private void SelectWeapon()
    {
        CleanWeapons();

        if (weapons.Count > 0)
        {
            if (wIndex >= weapons.Count)
                wIndex = weapons.Count - 1;

            SetWeapon(weapons[wIndex]);
        }
        else
            SetWeapon(null);
    }

    private void SetWeapon(WeaponBase weapon)
    {
        if (selectedWeapon != weapon)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].gameObject.SetActive(false);
            }

            selectedWeapon = weapon;

            if (selectedWeapon != null)
            {
                PositionWeapon(selectedWeapon.transform);
                selectedWeapon.gameObject.SetActive(true);
            }

            if (OnWeaponSelected != null)
                OnWeaponSelected(selectedWeapon);
        }
    }

    private void CleanWeapons()
    {
        for(int i = 0; i < weapons.Count;)
        {
            if (weapons[i] == null)
                weapons.RemoveAt(i);
            else
                i++;
        }
    }

    private void PositionWeapon(Transform w)
    {
        w.position = weaponPoint.position;
        w.rotation = weaponPoint.rotation;
        w.parent = weaponPoint;
    }
}
