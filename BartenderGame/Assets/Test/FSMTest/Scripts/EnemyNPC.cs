using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using System;

public class EnemyNPC : MonoBehaviour
{
    public enum StateTypes
    {
        IDLE = 0,
        CHASE,
        ATTACK,
        DAMAGE,
        DIE
    }

    #region NPC Data and Parameters
    public FSM mFsm;
    public int mMaxNumDamages = 5;
    private int mDamageCount = 0;
    #endregion

    void Start()
    {
        mFsm = new FSM();
        //mFsm.Add((int)StateTypes.IDLE, new NPCState(mFsm, (int)StateTypes.IDLE, this));
        //mFsm.Add((int)StateTypes.CHASE, new NPCState(mFsm, (int)StateTypes.CHASE, this));
        //mFsm.Add((int)StateTypes.ATTACK, new NPCState(mFsm, (int)StateTypes.ATTACK, this));
        //mFsm.Add((int)StateTypes.DAMAGE, new NPCState(mFsm, (int)StateTypes.DAMAGE, this));
        //mFsm.Add((int)StateTypes.DIE, new NPCState(mFsm, (int)StateTypes.DIE, this));
        Init_IdleState();
        Init_AttackState();
        Init_DieState();
        Init_DamageState();
        Init_ChaseState();
        mFsm.SetCurrentState(mFsm.GetState((int)StateTypes.IDLE));
    }

    void Update()
    {
        mFsm.Update();
    }

    void FixedUpdate()
    {
        mFsm.FixedUpdate();
    }


    #region Setup and initialize the various states.
    void Init_IdleState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.IDLE);

        // Add a text message to the delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - IDLE");
        };

        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - IDLE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - IDLE");
            if (Input.GetKeyDown("c"))
            {
                SetState(StateTypes.CHASE);
            }
            else if (Input.GetKeyDown("d"))
            {
                SetState(StateTypes.DAMAGE);
            }
            else if (Input.GetKeyDown("a"))
            {
                SetState(StateTypes.ATTACK);
            }
        };
    }

    void Init_AttackState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.ATTACK);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - ATTACK");
        };

        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - ATTACK");
        };

        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - ATTACK");
            if (Input.GetKeyDown("c"))
            {
                SetState(StateTypes.CHASE);
            }
            else if (Input.GetKeyDown("d"))
            {
                SetState(StateTypes.DAMAGE);
            }
        };
    }

    void Init_DieState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.DIE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - DIE");
        };

        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - DIE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - DIE");
        };
    }

    void Init_DamageState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.DAMAGE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            mDamageCount++;
            Debug.Log("OnEnter - DAMAGE");
        };

        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - DAMAGE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - DAMAGE");
            if (mDamageCount == mMaxNumDamages)
            {
                SetState(StateTypes.DIE);
                return;
            }
            if (Input.GetKeyDown("i"))
            {
                SetState(StateTypes.IDLE);
            }
            else if (Input.GetKeyDown("c"))
            {
                SetState(StateTypes.CHASE);
            }
            else if (Input.GetKeyDown("a"))
            {
                SetState(StateTypes.ATTACK);
            }
        };
    }

    void Init_ChaseState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.CHASE);
        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - CHASE");
        };
        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - CHASE");
        };
        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - CHASE");
            if (Input.GetKeyDown("i"))
            {
                SetState(StateTypes.IDLE);
            }
            else if (Input.GetKeyDown("d"))
            {
                SetState(StateTypes.DAMAGE);
            }
            else if (Input.GetKeyDown("a"))
            {
                SetState(StateTypes.ATTACK);
            }
        };
    }
    #endregion


    private void SetState(StateTypes type)
    {
        mFsm.SetCurrentState(mFsm.GetState((int)type));

        if (type == StateTypes.DIE)
        {
            StartCoroutine(Coroutine_Die(1.0f));
        }
    }

    IEnumerator Coroutine_Die(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("NPC died. Removing gameobject");
        Destroy(gameObject);
    }
}