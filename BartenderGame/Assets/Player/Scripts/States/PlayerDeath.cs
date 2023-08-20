using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : State
{
    protected override void OnEnable()
    {
        StartCoroutine(Coroutine_Die(2));
        base.OnEnable();
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
    }

    private IEnumerator Coroutine_Die(float duration)
    {
        Debug.Log("NPC died. Removing gameobject");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
