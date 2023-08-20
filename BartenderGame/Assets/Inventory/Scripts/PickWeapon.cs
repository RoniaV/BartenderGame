using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickWeapon : MonoBehaviour
{
    [SerializeField] WeaponBase weapon;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject?.GetComponent<WeaponInventory>() != null)
        {
            Debug.Log("Picked weapon " + weapon.gameObject.name);
            coll.GetComponent<WeaponInventory>().AddWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
