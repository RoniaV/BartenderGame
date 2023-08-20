using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingPlayer : Sight
{
    [HideInInspector]
    public GameObject player;

    void Update()
    {
        if (interestingTargets?.Length > 0)
        {
            player = interestingTargets[0]?.gameObject;
        }
        else
            player = null;
    }
}
