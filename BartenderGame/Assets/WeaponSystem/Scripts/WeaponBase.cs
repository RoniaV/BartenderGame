using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponBase : MonoBehaviour
{
    public Action OnAtack = delegate { };

    public abstract void Atack();
    public abstract void ReleaseAtack();

    public virtual void SecondaryAtack() { }
    public virtual void ReleaseSecondaryAtack() { }
}
