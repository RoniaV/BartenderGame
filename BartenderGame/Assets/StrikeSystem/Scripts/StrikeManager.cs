using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeManager : MonoBehaviour
{
    [SerializeField] int maxStrikes = 3;
    [SerializeField] OrderManager orderManager;
    [SerializeField] WeaponInventory inventory;
    [SerializeField] Plate playerPlate;

    private int strikeCounter = 0;

    void OnEnable()
    {
        orderManager.OnOrderFailed += Strike;
    }

    void OnDisable()
    {
        orderManager.OnOrderFailed -= Strike;
    }

    private void Strike()
    {
        Debug.Log("Strike!");
        strikeCounter++;

        if (strikeCounter >= maxStrikes)
            EndGame();
    }

    private void EndGame()
    {
        Debug.Log("Reach maximum strikes");
        orderManager.StopAllOrders();
        inventory.RemoveWeapon(playerPlate);
    }
}
