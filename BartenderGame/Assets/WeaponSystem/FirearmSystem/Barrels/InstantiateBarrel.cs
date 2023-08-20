using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBarrel : BarrelBase
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float shotForce;
    [SerializeField] bool addInertia;
    [Header("Object Pool Settings")]
    [SerializeField] Transform poolParent;

    private ObjectPool<Bullet> bulletPool;
    private CharacterController character;

    void Start()
    {
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, poolParent);
        poolParent.parent = null;
    }

    protected override void DoShot()
    {
        Bullet shotedBullet = bulletPool.GetObject();

        shotedBullet.transform.position = shotPoint.position;
        shotedBullet.transform.rotation = shotPoint.rotation;

        if (addInertia)
        {
            SetCharacter();

            shotedBullet.LaunchBullet(shotPoint.forward, shotForce, character.velocity);
        }
        else
            shotedBullet.LaunchBullet(shotPoint.forward, shotForce);

    }

    private void SetCharacter()
    {
        if (character == null)
            character = transform.root.GetComponent<CharacterController>();
    }
}
