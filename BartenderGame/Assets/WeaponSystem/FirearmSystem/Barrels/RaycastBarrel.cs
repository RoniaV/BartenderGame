using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBarrel : BarrelBase, IDamageDealer
{
    [SerializeField] float maxDistance = 100f;
    [Header("Debug")]
    [SerializeField] bool drawRay;


    protected override void DoShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(shotPoint.position, shotPoint.forward, out hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);

            IDamageable damageable = hit.collider?.GetComponent<IDamageable>();

            if (damageable != null)
                DoDamage(damageable);
        }
    }

    public void DoDamage(IDamageable target)
    {
        target.ReceiveDamage();
    }
}
