using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public Transform ClientSeat { get { return clientSeat; } }
    public ConsumableContainer ContainerSeat { get { return container; } }

    [SerializeField] Transform clientSeat;
    [SerializeField] ConsumableContainer container;
}
