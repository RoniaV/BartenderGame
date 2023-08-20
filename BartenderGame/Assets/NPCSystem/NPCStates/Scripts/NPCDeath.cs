using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCDeath : State
{
    public event Action OnNPCDeath;

    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log(name + " enter Death State");

        if (OnNPCDeath != null)
            OnNPCDeath();

        StartCoroutine(Coroutine_Die(2));
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Debug.Log(name + " exit Death State");
    }

    private IEnumerator Coroutine_Die(float duration)
    {
        Debug.Log(gameObject.name + " died. Removing gameobject");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
