using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KillNoticer : MonoBehaviour
{
    public bool Noticed { get { return noticed; } }

    [SerializeField] UFSM[] nPCs;
    [SerializeField] NPCDeath[] npcDeaths;
    [SerializeField] LayerMask playerLayer;

    private bool noticed = false;
    private bool playerInside = false;

    void OnEnable()
    {
        for(int i = 0; i < npcDeaths.Length; i++)
        {
            npcDeaths[i].OnNPCDeath += NPCDied;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < npcDeaths.Length; i++)
        {
            npcDeaths[i].OnNPCDeath -= NPCDied;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInside = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInside = false;
        }
    }

    private void NPCDied()
    {
        if(!noticed && playerInside)
        {
            Debug.Log("Kill noticed");

            for(int i = 0; i < nPCs.Length; i++)
            {
                nPCs[i].SetTriggerParameter("Kill");
            }

            noticed = true;
        }
    }
}
