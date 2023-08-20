using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarrelBase : MonoBehaviour
{
    [SerializeField] protected Transform shotPoint;
    [SerializeField] protected float cooldown = 1f;
    [SerializeField] protected bool continuousShooting;

    protected bool canBeShoot = true;
    protected float tCooldown;

    protected virtual void Update()
    {
        Debug.DrawRay(shotPoint.position, shotPoint.forward, Color.blue);
        if (tCooldown > 0 && tCooldown <= cooldown)
            tCooldown -= Time.deltaTime;
    }

    public void Shot()
    {
        if (tCooldown <= 0 && canBeShoot)
        {
            tCooldown = cooldown;
            canBeShoot = continuousShooting;

            DoShot();
        }
    }

    protected abstract void DoShot();

    public void ReleaseShot()
    {
        tCooldown = 0;
        canBeShoot = true;
    }
}
