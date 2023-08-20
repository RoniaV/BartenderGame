using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public abstract class NPCBase : MonoBehaviour
{
    //protected Dictionary<string, int> StateTypes;


    #region NPC Data and Parameters
    protected FSM mFsm;
    #endregion

    protected virtual void Start()
    {
        mFsm = new FSM();
    }

    protected virtual void Update()
    {
        mFsm.Update();
    }

    protected virtual void FixedUpdate()
    {
        mFsm.FixedUpdate();
    }


    #region Setup and initialize the various states.
    #endregion


    protected virtual void SetState(int type)
    {
        mFsm.SetCurrentState(mFsm.GetState(type));
    }

    protected virtual IEnumerator Coroutine_Die(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("NPC died. Removing gameobject");
        Destroy(gameObject);
    }
}
