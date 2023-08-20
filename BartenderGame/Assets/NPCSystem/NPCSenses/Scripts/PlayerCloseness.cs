using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseness : CheckSomethingIsClose
{
    [HideInInspector]
    public Transform player;

    void Update()
    {
        if (interestingTargets?.Length > 0)
        {
            player = interestingTargets[0]?.transform;
        }
        else
            player = null;
    }
}
