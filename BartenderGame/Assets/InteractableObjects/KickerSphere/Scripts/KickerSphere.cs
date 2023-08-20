using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerSphere : MonoBehaviour, IKicker
{
    public void DoKick(IKickable kickable, Vector3 kickPoint)
    {
        Vector3 direction = (kickPoint - transform.position).normalized;

        kickable.ReceiveKick(direction);
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Object collisioned: " + coll.gameObject.name);
        if(coll.gameObject?.GetComponent<IKickable>() != null)
        {
            Debug.Log("Kickable collisioned " + coll.gameObject.name);
            DoKick(coll.gameObject.GetComponent<IKickable>(), coll.ClosestPoint(transform.position));
        }
    }
}
