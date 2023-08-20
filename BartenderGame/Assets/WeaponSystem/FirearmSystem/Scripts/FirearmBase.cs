using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FirearmBase : WeaponBase
{
    [SerializeField] protected BarrelBase[] barrels;
    [SerializeField] protected int maxAmmo = 10;
    [Header("Debug")]
    [SerializeField] protected bool shooting;

    protected abstract void DebugShot();

    protected int actualAmmo;
}
