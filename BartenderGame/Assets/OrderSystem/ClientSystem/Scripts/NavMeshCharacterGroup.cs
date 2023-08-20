using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NavMeshCharacterGroup : MonoBehaviour
{
    public event Action OnCharactersArrive;

    public int clientCount { get { return characters.Length; } }

    private NavMeshCharacter[] characters;
    private int arrivals;

#if UNITY_EDITOR
    //[Header("Debug Settings")]
    //[SerializeField] NavMeshCharacter[] characterArray;
    //[SerializeField] Transform[] destinationPoints;
    //[SerializeField] bool setDestination;

    //void Start()
    //{
    //    SetCharacters(characterArray);

    //    OnCharactersArrive += ()=> Debug.Log("All characters arrived");
    //}

    //void Update()
    //{
    //    if(setDestination)
    //    {
    //        SetDestination(destinationPoints);
    //        setDestination = false;
    //    }
    //}
#endif

    public void SetCharacters(NavMeshCharacter[] characters)
    {
        this.characters = characters;

        foreach(NavMeshCharacter character in characters)
        {
            character.OnEndOfPathReached += SetArrivals;
        }
    }

    public void SetDestination(Transform[] destinationPoints)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].SetNewTarget(destinationPoints[i]);
        }
    }

    private void SetArrivals()
    {
        arrivals++;

        if (arrivals >= characters.Length)
        {
            arrivals = 0;

            if (OnCharactersArrive != null)
                OnCharactersArrive();
        }
    }
}
