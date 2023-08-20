using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    void DoDamage(IDamageable target);
}
