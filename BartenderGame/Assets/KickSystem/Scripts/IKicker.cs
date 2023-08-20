using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKicker 
{
    void DoKick(IKickable kickable, Vector3 kickPoint);
}
