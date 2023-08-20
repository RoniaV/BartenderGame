using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : FirearmBase
{
    void Update()
    {
        if (shooting)
            DebugShot();
    }

    public override void Atack()
    {
        for(int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Shot();
        }

        OnAtack();
    }

    public override void ReleaseAtack()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].ReleaseShot();
        }
    }

    protected override void DebugShot()
    {
        Atack();
        shooting = false;
    }
}
