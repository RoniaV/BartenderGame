using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBox : MonoBehaviour, IDamageable
{
    public void ReceiveDamage()
    {
        Destroy(gameObject);
    }
}
